// Класс Регистрации анкет и ошибок
class Registry
{
    private readonly List<Person> _people = new List<Person>();
    private readonly List<(int Line, string Raw, string Reason)> _errors = new List<(int Line, string Raw, string Reason)>();

    public void Add(Person people) => _people.Add(people);
    public bool Load(string fileName)
    {
        // проверка наличии файла
        if (!File.Exists(fileName))
            return false;

        string[] lines = File.ReadAllLines(fileName);

        for (int i = 0; i < lines.Length; i++)
        {
            string currentLine = lines[i].Trim();
            int numberLine = i + 1;

            // пропуск пустых строк
            if (string.IsNullOrWhiteSpace(currentLine))
                continue;

            try
            {
                string[] parts = currentLine.Split(';');

                if (parts.Length != 5)
                    throw new AnketaError("Ожидается вводные данные в формате: тип ; ФИО ; почта ; телефон ; кафедра/группа");

                string type     = parts[0].Trim();
                string fio      = parts[1].Trim();
                string email    = parts[2].Trim();
                string phone    = parts[3].Trim();
                string extra    = parts[4].Trim();

                Person person;

                if (type == "s")
                {
                    person = new Student(fio, email, phone, extra);
                }
                else if (type == "t")
                {
                    person = new Teacher(fio, email, phone, extra);
                }
                else
                {
                    throw new AnketaError($"Неизвестный тип: {type}");
                }

                _people.Add(person);
            }
            catch (AnketaError ex)
            {
                _errors.Add((numberLine, currentLine, ex.Message));
            }
        }
        _people.Sort();

        return true;
    }

    public int Count => _people.Count;

    public IReadOnlyList<Person> People => _people;
    public IReadOnlyList<(int, string, string)> Errors => _errors;

    public Dictionary<string, int> StatsByRole()
    {
        return _people.GroupBy(p => p.Role).ToDictionary(g => g.Key, g => g.Count());
    }
    public Dictionary<string, int> StatsByExtra()
    {
        return _people.GroupBy(p => p.Extra).ToDictionary(g => g.Key, g => g.Count());
    }

    public void SaveOk(string path)
    {
        using (StreamWriter writer = new StreamWriter(path))
        {
            foreach (Person person in _people)
            {
                writer.WriteLine($"{person.TypeCode};{person.FullName};{person.Email};{person.Phone};{person.Extra}");
            }
        }
    }
    public void SaveErrors(string path)
    {
        using (StreamWriter writer = new StreamWriter(path))
        {
            foreach ((int Line, string Raw, string Reason) error in _errors)
            {
                writer.WriteLine($"Строка {error.Line}: {error.Raw}");
                writer.WriteLine($"   - {error.Reason}");
                writer.WriteLine("");
            }
        }
    }
}
