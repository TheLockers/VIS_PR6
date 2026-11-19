// класс счетчика 
public class Counter
{
    // хранилище счетчика
    public int Count { get; private set; }

    // конструктор по умолчанию
    public Counter()
    {
        Count = 0;
    }

    // функция увеличение счетчика на n, базово при вызове функции будет увеличивать значение на 1
    public void Add(int n)
    {
        if (n <= 1)
        {
            Count += n;
            Console.WriteLine($"откланено: {n}");
            return;
        }
        Count += n;
        Console.WriteLine(this);
    }

    // сброс счетчика до 0
    public void Reset()
    {
        Count = 0;
        Console.Write("после сброса: ");
        Console.WriteLine(this);
    }

    // вывод объекта класса
    public override string ToString()
    {
        return $"принято: {Count}";
    }
}

// основной класс программы
class Program
{
    // главная функция программы
    static void Main()
    {
        // создание 2 счетчиков по заданию
        Counter counter1 = new Counter();
        Counter counter2 = new Counter();

        // неазвисимая работа счетчиков 
        counter1.Add(7);
        counter2.Add(1);
        counter1.Reset();
        Console.WriteLine(counter2);
    }
}

