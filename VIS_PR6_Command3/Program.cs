// класс температур
public class Temperature : IComparable<Temperature>
{
    // поле цельсий
    private double _celsius;

    // конструктор
    public Temperature(double celsius)
    {
        TemperatureC = celsius;
    }

    // св-во цельсий
    public double TemperatureC
    {
        // геттер
        get => _celsius;

        // приватный сеттер
        private set
        {
            if (value < -273.15)
                throw new ArgumentOutOfRangeException(nameof(value), "Температура ниже абсолютного нуля невозможна");
            _celsius = value;
        }
    }
    // поле температуры фаренгейта (вычисляемое)
    public double TemperatureF => _celsius * 9 / 5 + 32;

    // __lt__ — через IComparable<T>
    public int CompareTo(Temperature? other)
    {
        if (other is null) return 1;
        return _celsius.CompareTo(other._celsius);
    }

    // вывод класса
    public override string ToString() => $"{TemperatureC:F1} C = {TemperatureF:F1} F";
}

// класс программы
class Program
{
    // главный метод класса
    static void Main()
    {
        // создание объекта температур
        Temperature[] temps = [
            new Temperature(21.8),
            new Temperature(30),
            new Temperature(-31)
            ];

        foreach (Temperature t in temps)
        {
            Console.WriteLine(t);
        }

        Array.Sort(temps);

        Console.WriteLine("\nпо возрастанию:");
        foreach (Temperature t in temps)
        {
            Console.WriteLine(t);
        }

        var warmest = temps.Max();

        Console.WriteLine($"Самый темплый {warmest}");
    }
}