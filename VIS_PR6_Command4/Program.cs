// класс фигур (родитель)
public class Figure
{
    // поле имени
    public string Name { get; }

    // конструктор класса
    public Figure(string name)
    {
        Name = name;
    }

    // виртуальный метод площади
    public virtual double Area() => 0.0;

    // вывод класса
    public override string ToString()
    {
        return $"{Name}: площадь {Area():F2}";
    }
}

// класс Прямоугольник наследуемый Фигуру
class Rect : Figure
{
    // поле шириный
    private double _width;
    // св-во ширины с проверкой
    public double Width
    {
        get => _width;
        private set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Ширина не может быть меньше или равно 0.");
            _width = value;
        }
    }

    // поле длины
    private double _lenght;
    // св-во длины
    public double Lenght
    {
        get => _lenght;
        private set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Длина не может быть меньше или равна 0.");
            _lenght = value;
        }
    }

    //конструктор класса
    public Rect(double width, double lenght) : base("Прямоугольник")
    {
        Width = width;
        Lenght = lenght;
    }

    // переопределенная метод Area()
    public override double Area() => _lenght * _width;
}

// класс Круг наследуемы Фигуру
class Circle : Figure
{
    // поле радиуса
    private double _radius;
    // св-во радиуса
    public double Radius
    {
        get => _radius;
        private set
        {
            if (value <= 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Радиус не может быть меньше или равно 0.");
            _radius = value;
        }
    }

    // конструктор класса
    public Circle(double radius) : base("Круг")
    {
        Radius = radius;
    }

    // переопределенный метод
    public override double Area() => 3.1415 * _radius * _radius;
}

// главный класс программы
class Program
{
    // главная функция программы
    static void Main()
    {
        // создание экземпляров на основе класса фигуры
        Figure[] Array = [
            new Rect(3, 4),
            new Circle(2),
            new Rect(3, 5)
            ];

        // переменная суммы площадей
        double sumArea = 0;

        // счет площадей и вывод прощадей фигур
        foreach(Figure t in Array)
        {
            sumArea += t.Area();
            Console.WriteLine(t);
        }

        // вывод суммы площадей
        Console.WriteLine("\nОбщая площадь: {0:F2}", sumArea);
    }
}