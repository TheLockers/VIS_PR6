public class Student
{
    public string FullName { get; set; }
    public string Group { get; set; }

    // конструктор по умолчанию
    public Student()
    {
        FullName = string.Empty;
        Group = string.Empty;
    }

    // конструктор с парамерами
    public Student(string fullName, string group)
    {
        FullName = fullName;
        Group = group;
    }

    // функция вывода ФИО в формате { Фамилия И. О. }
    public string GetShortName()
    {
        if (string.IsNullOrWhiteSpace(FullName)) return $"Ошибка: ФИО не задано";

        var parts = FullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length > 3) return $"Ошибка: Обработка ФИО";

        var surname = Capitalize(parts[0]);
        var initials = string.Concat(parts.Skip(1).Select(p => $"{char.ToUpper(p[0])}. ")).Trim();

        return initials.Length == 0 ? surname : $"{surname} {initials}";
    }

    private static string Capitalize(string str)
        => string.IsNullOrWhiteSpace(str) ? str : char.ToUpper(str[0]) + str[1..].ToLower();

    // вывод параметров класса
    public override string ToString()
    {
        return $"Студент: {FullName}, группа: {Group}";
    }
}

class Program1
{
    static void Main()
    {
        Student student1 = new Student("Полункин Сергей Владимирович", "09C41");
        Student student2 = new Student("Токавер Валерий Евгеньевич", "09C41");

        Console.WriteLine(student1.FullName);
        Console.WriteLine(student1.Group);
        Console.WriteLine(student1.GetShortName());
        Console.WriteLine(student2.GetShortName() + $" | {student2.Group}");
    }
}
