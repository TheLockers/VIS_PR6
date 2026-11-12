// Класс ошибки AnketaError
class AnketaError : Exception
{
    public AnketaError(string message) : base(message) { }
}

// Класс Person родительский класс
class Person : IComparable<Person>
{
    private string _fullName = string.Empty;
    private string _email = string.Empty;
    private string _phone = string.Empty;

    public string FullName { get => _fullName; private set => _fullName = ValidationFullName(value); }
    public string Email { get => _email; private set => _email = ValidationEmail(value); }
    public string Phone { get => _phone; private set => _phone = ValidationPhone(value); }

    public Person(string fullname, string email, string phone)
    {
        FullName = fullname;
        Email = email;
        Phone = phone;
    }

    private static string ValidationFullName(string value)
    {
        string input = value;

        // проверка на ввод  пустой строки
        if (string.IsNullOrWhiteSpace(value))
            throw new AnketaError("ФИО не может быть пустым.");

        // проверка на ввод неправильного формата ФИО
        string[] parts = input
            .Trim()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length is < 2 or > 3)
            throw new AnketaError("Ожидается 2 формы ФИО: 'Фамилия Имя', 'Фамилия Имя Отчество'.");

        if (parts.Any(p => !p.All(char.IsLetter)))
            throw new AnketaError("ФИО должно содержать только буквы.");

        // валидация ФИО
        static string Normalize(string s) =>
            char.ToUpper(s[0]) + s[1..].ToLower();

        string surname = Normalize(parts[0]);
        string name = Normalize(parts[1]);
        string? patronymic = parts.Length == 3 ? Normalize(parts[2]) : null;

        return patronymic is null
            ? $"{surname} {name}"
            : $"{surname} {name} {patronymic}";
    }
    private static string ValidationEmail(string value)
    {
        string input = value;

        // проверка на ввод пустой строки
        if (string.IsNullOrWhiteSpace(input))
            throw new AnketaError("Почта не может быть пустой.");

        string[] parts = input
            .Trim()
            .Split('@');

        // Проверка на формат почты
        if (parts.Length != 2)
            throw new AnketaError("Ожидается, что email состоит из имени и домена с @ между ними.");

        string local = parts[0];
        string domain = parts[1];

        // проверка на целостность почты
        if (string.IsNullOrWhiteSpace(local) || string.IsNullOrWhiteSpace(domain))
            throw new AnketaError("Имя или домен почты не могут быть пустыми.");

        string[] domainParts = domain.Split('.');

        // проверка домена почты на целостность
        if (domainParts.Length != 2)
            throw new AnketaError("Домен должен состоять из 2-ух частей.");

        // проверка целостность домена
        if (string.IsNullOrWhiteSpace(domainParts[0]) || string.IsNullOrWhiteSpace(domainParts[1]))
            throw new AnketaError("Имя домена или зона домена не могут быть пустыми.");

        // Проверка валидности Имени почты: не должно содержать спец символов
        foreach (char c in local)
        {
            if (!char.IsLetterOrDigit(c))
                throw new AnketaError("Имя не должно содержать спец символов.");
        }

        // Проверка валидности Домена почты: не должно содержать спец символов
        foreach (char c in domain)
        {
            if (!char.IsLetterOrDigit(c) && c != '.')
                throw new AnketaError("Домен не должен содержать спец символов.");
        }

        return local + '@' + domain;
    }
    private static string ValidationPhone(string value)
    {
        string input = value;

        // Проверка целостности введенной строки
        if (string.IsNullOrWhiteSpace(input))
            throw new AnketaError("Телефон не должен быть пустым.");

        char[] numbers = input
            .Where(char.IsDigit)
            .ToArray();

        // Проверка формата телефона
        if (numbers.Length != 11)
            throw new AnketaError("Номер должен состоять из 11 цифр.");

        // Замена первой цифры под ожидаемый формат
        if (numbers[0] == '8')
            numbers[0] = '7';

        // Проверка валидации ожидаемого формата
        if (numbers[0] != '7')
            throw new AnketaError("Номер должен начианться с 7 или 8.");

        return new string(numbers);
    }

    public string MaskedPhone() => $"+{_phone[0]} ({_phone[1..4]}) ***-**-{_phone[9..11]}";
    public string ShortFio()
    {
        string[] parts = _fullName.Split(' ');

        string surName = parts[0];
        string initials = string.Empty;

        foreach (string p in parts.Skip(1))
        {
            initials += $"{char.ToUpper(p[0])}. ";
        }

        return $"{surName} {initials.Trim()}";
    }

    // Методы заглушки для переопределения
    public virtual string Role => "Не указана";
    public virtual string Extra => "-";
    public virtual string TypeCode => "unknown";

    public int CompareTo(Person? other) =>
        other is null ? 1 : string.Compare(FullName, other.FullName, StringComparison.CurrentCulture);
    public override string ToString()
    {
        return $"{FullName,-10} | {Role,-10} | {Email,-10} | {MaskedPhone(),-10}";
    }
}

// Класс Студента наследуемый Person
class Student : Person
{
    public string Group { get; }

    public Student(string fullName, string email, string phone, string group)
        : base(fullName, email, phone)
    {
        // проверка целостности группы
        if (string.IsNullOrWhiteSpace(group))
            throw new AnketaError("Группа не можеты быть пустой.");

        Group = group.Trim();
    }

    // Переопределенные методы
    public override string Role => "Студент";
    public override string Extra => Group;
    public override string TypeCode => "s";
}

// Класс Учителя наследуемый Person
class Teacher : Person
{
    // Поле кафедры
    public string Department { get; }

    // конструктор класса fullName, email, phone инициализируются в родительском классе 
    public Teacher(string fullName, string email, string phone, string department)
        : base(fullName, email, phone)
    {
        // проверка на целостность группы
        if (string.IsNullOrWhiteSpace(department))
            throw new AnketaError("Кафедра не может быть пустой.");

        Department = department.Trim();
    }

    // Переопределенные методы
    public override string Role => "Преподаватель";
    public override string Extra => Department;
    public override string TypeCode => "t";
}