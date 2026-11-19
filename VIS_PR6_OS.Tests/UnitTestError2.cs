namespace VIS_PR6_OS.Tests
{
    public class UnitTestError2
    {
        [Fact]
        public void Person_NullEmailFormat_Error()
        {
            Assert.Throws<AnketaError>(() => new Person("петров-водкин кузьма", "iv anov@mail.ru", "89991234567"));
        }
    }
}