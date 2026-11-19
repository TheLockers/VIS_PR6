// ошибка собственного приготовления
class AgeError : Exception
{
    // поле возраста
    public int AgeOutRange { get; }

    // конструктор
    public AgeError(int age) : base($"- возраст {age} вне границ 0..120")
    {
        AgeOutRange = age;
    }

    // вывод класса
    public override string ToString()
    {
        return $"- возраст {AgeOutRange} вне границ 0..120";
    }
}

// класс Person
class Person
{
    // поле возраста 
    private int _age;
    // св-во возраста
    public int Age
    {
        get => _age;
        // сеттер с проверкой
        private set
        {
            if (0 > value || value > 120)
                // вызов собственной ошибки
                throw new AgeError(value);
            _age = value;
        }
    }

    // поле имени
    public string Name { get; }

    // конструктор класса
    public Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    // вывод класса
    public override string ToString()
    {
        return $"{Name}, {Age} лет";
    }
}

// главный класс программы
class Program
{
    // главный метод класса программы
    static void Main()
    {
        // данные для работы программы
        var input = new (string Name, int Age)[]
        {
            ("Маша", 121),
            ("Игорь", 54),
            ("Алексей", -3),
            ("Сергей", 20)
        };

        // пустой список персон
        var list = new List<Person>();

        // перебор всех данных
        foreach (var (Name, Age) in input)
        {
            // попытка добавить объект класса Person, иначе ошибка 
            try
            {
                list.Add(new Person(Name, Age));
            }
            catch (AgeError ex)
            {
                Console.WriteLine($"Не принято: {Name} {ex}");
            }
        }

        // Вывод обработаных Person
        Console.WriteLine($"\nПринято: {list.Count}");
        foreach (var l in list)
        {
            Console.WriteLine("   {0}", l);
        }
    }
}