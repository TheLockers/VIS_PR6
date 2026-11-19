using System;
using System.Collections.Generic;
using System.Text;

namespace VIS_PR6_OS.Tests
{
    public class RegistryFixture : IDisposable
    {
        public Registry Registry { get; }
        private readonly string _filePath;

        public RegistryFixture()
        {
            _filePath = Path.GetTempFileName();

            string[] lines =
            {
                "s;  иванов   иВАН ;IvAnov@Mail.Ru ; +7 (921) 111-22-33 ; 09с33",
                "t;петров петр ;petrov@voenmeh.ru;89217654321;Кафедра И9",
                "s;петрова анна;petrova@yandex.ru;89217654321;09с33",
                "",
                "x;сидоров;sidorov@mail.ru;+79210000000;09с34",
                "s;кузнецов павел;kuznetsov.mail.ru;+79211234567;09с34",
                "t;смирнова ольга;smirnova@inbox.ru;+7921123;Кафедра И4"
            };

            File.WriteAllLines(_filePath, lines);

            Registry = new Registry();

            Registry.Load(_filePath);
        }

        public void Dispose()
        {
            if (File.Exists(_filePath)) File.Delete(_filePath);
        }
    }
}