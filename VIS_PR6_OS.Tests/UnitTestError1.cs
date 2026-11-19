namespace VIS_PR6_OS.Tests
{
    public class UnitTestError1
    {
        [Fact]
        public void Person_DoubleSurname_Normalizes()
        {
            Person person = new Person(
                "петров-водкин кузьма",
                "test@mail.ru",
                "89991234567"
            );

            Assert.Equal("Петров-Водкин Кузьма", person.FullName);
        }
    }
}