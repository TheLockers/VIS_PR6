namespace VIS_PR6_OS.Tests
{
    public class UnitTestError3
    {
        [Fact]
        public void Person_OrderByName()
        {
            var people = new List<Person>
            {
                new Person("Ёлкин Петр", "elkin@mail.ru", "89991234568"),
                new Person("Яковлев Иван", "yakovlev@mail.ru", "89991234567"),
                new Person("Абрамов Алексей", "abramov@mail.ru", "89991234569"),
                new Person("Ёлкин Петр", "elkin@mail.ru", "89991234568"),
            };

            people.Sort();

            var surnames = people.Select(p => p.FullName.Split(' ')[0]).ToList();

            Assert.Equal(new[] { "Абрамов", "Ёлкин", "Ёлкин", "Яковлев" }, surnames);
        }
    }
}
