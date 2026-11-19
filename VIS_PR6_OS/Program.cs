class Program
{
    static void Main()
    {
        Registry registry = new Registry();
        string inputPath = @"C:\Users\polun\source\repos\VIS_PR6\VIS_PR6_OS\input.txt";

        // 1. Загрузка
        if (!registry.Load(inputPath))
        {
            Console.WriteLine($"Файл не найден: {inputPath}");
            return;
        }

        // 2. Вывод таблицы
        Console.WriteLine("  №  ФИО                         Роль           Группа/кафедра  Телефон");
        Console.WriteLine(new string('-', 85));

        for (int i = 0; i < registry.People.Count; i++)
        {
            Person p = registry.People[i];
            Console.WriteLine($"{i + 1,3}  {p.FullName,-27} {p.Role,-14} {p.Extra,-15} {p.MaskedPhone()}");
        }
        Console.WriteLine(new string('-', 85));
        Console.WriteLine();

        // 3. Вывод ошибок
        if (registry.Errors.Count > 0)
        {
            Console.WriteLine($"Отклонено строк: {registry.Errors.Count}");
            foreach (var err in registry.Errors)
            {
                Console.WriteLine($"  строка {err.Item1}: {err.Item3}");
                Console.WriteLine($"      {err.Item2}");
            }
            Console.WriteLine();
        }

        // 4. Итоговая статистика
        Console.WriteLine($"Принято: {registry.Count} | Отклонено: {registry.Errors.Count}");
        Console.WriteLine();

        Console.WriteLine("По ролям:");
        foreach (var kvp in registry.StatsByRole().OrderBy(x => x.Key))
        {
            Console.WriteLine($"  {kvp.Key,-15} {kvp.Value}");
        }
        Console.WriteLine();

        Console.WriteLine("По группам:");
        foreach (var kvp in registry.StatsByExtra().OrderBy(x => x.Key))
        {
            Console.WriteLine($"  {kvp.Key,-15} {kvp.Value}");
        }

        // 5. Сохранение результатов
        string okPath = @"C:\Users\polun\source\repos\VIS_PR6\VIS_PR6_OS\ok.txt";
        string errorsPath = @"C:\Users\polun\source\repos\VIS_PR6\VIS_PR6_OS\errors.txt";

        registry.SaveOk(okPath);
        registry.SaveErrors(errorsPath);

        Console.WriteLine($"\nДанные успешно сохранены в:\n  {okPath}\n  {errorsPath}");
    }
}