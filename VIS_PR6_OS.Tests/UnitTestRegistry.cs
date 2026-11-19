using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace VIS_PR6_OS.Tests
{
    public class UnitTestRegistry : IClassFixture<RegistryFixture>
    {
        private readonly Registry _registry;

        public UnitTestRegistry(RegistryFixture fixture)
        {
            _registry = fixture.Registry;
        }

        [Fact]
        public void Test_NullRegistry()
        {
            Registry registry = new Registry();

            int count = registry.Count;

            Assert.Equal(0, count);
        }

        [Fact]
        public void Test_ThreeAnketsInDownloadFixture()
        {
            int count = _registry.Count;

            Assert.Equal(3, count);
        }

        [Fact]
        public void Test_ThreeErrorsInDownloadFixture()
        {
            int count = _registry.Errors.Count;

            Assert.Equal(3, count);
        }

        [Fact]
        public void Test_NumberErrors()
        {
            List<int> numbers = _registry.Errors.Select(error => error.Item1).ToList();

            Assert.Equal(new[] { 5, 6, 7 }, numbers);
        }

        [Fact]
        public void Test_NotHaveFile()
        {
            string path = "111.txt";

            try
            {
                Registry registry = new();

                Assert.False(registry.Load(path));
            }
            finally
            {
                File.Delete(path);
            }
        }

        [Fact]
        public void Test_StatsCountRoles()
        {
            var stat = _registry.StatsByRole();

            Assert.Equal(2, stat["Студент"]);
            Assert.Equal(1, stat["Преподаватель"]);
        }

        [Fact]
        public void Test_StatsCountGroups()
        {
            var stat = _registry.StatsByExtra();

            Assert.Equal(2, stat.Count);
            Assert.Equal(2, stat["09с33"]);
        }

        [Fact]
        public void Test_RegistryIteratedForeach()
        {
            int count = 0;

            foreach (var people in _registry)
            {
                count++;
            }

            Assert.Equal(3, count);
        }

        [Fact]
        public void Test_FilterFullName()
        {
            var names = _registry.People.Select(person => person.FullName).ToList();

            Assert.Equal(new[] { "Иванов Иван", "Петров Петр", "Петрова Анна" }, names);
        }

        [Fact]
        public void Test_SaveOk()
        {
            string filename = Path.GetTempFileName();

            try
            {
                _registry.SaveOk(filename);

                string[] lines = File.ReadAllLines(filename);

                Assert.Equal(3, lines.Length);
            }
            finally
            {
                File.Delete(filename);
            }
        }

        [Fact]
        public void Test_SaveErrorsError()
        {
            string filename = Path.GetTempFileName();

            try
            {
                _registry.SaveErrors(filename);

                string content = File.ReadAllText(filename);

                Assert.Contains("Неизвестный тип", content);
            }
            finally
            {
                File.Delete(filename);
            }
        }
    }
}
