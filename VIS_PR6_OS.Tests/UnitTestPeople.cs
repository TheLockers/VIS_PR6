namespace VIS_PR6_OS.Tests
{
    public class UnitTestPeople
    {
        [Theory]
        [InlineData("  иванов   иВАН  ", "Иванов Иван")]
        [InlineData("ИВАНОВ ИВАН", "Иванов Иван")]
        [InlineData("  иВАНОВ   иВАН  ", "Иванов Иван")]
        [InlineData("пЕТРОВ иВАН сЕРГЕЕВИЧ", "Петров Иван Сергеевич")]
        public void Test_Parametrize(string input, string expected)
        {
            Person person = new Person(input, "email@domain.com", "88005553535");

            Assert.Equal(expected, person.FullName);
        }

        [Theory]
        [InlineData("Иванов")]
        [InlineData("    ")]
        [InlineData("")]
        public void Test_FormatAncorrect(string input)
        {
            Assert.Throws<AnketaError>(() => new Person(input, "email@domain.com", "88005553535"));
        }

        [Theory]
        [InlineData("PolunkinSergei@gmail.com", "polunkinsergei@gmail.com")]
        [InlineData("PopovArseniy@gmail.com", "popovarseniy@gmail.com")]
        public void Test_FormatEmail(string input, string expected)
        {
            Person person = new Person("Иванов Иван", input, "88005553535");

            Assert.Equal(expected, person.Email);
        }

        [Theory]
        [InlineData("kuznetsov.mail.ru")]
        [InlineData("ivan@@mail.ru")]
        [InlineData("@mail.ru")]
        [InlineData("ivan@")]
        [InlineData("ivan@mail")]
        [InlineData("ivan@mail.ru.com")]
        [InlineData("ivan!@mail.ru")]
        [InlineData("ivan@mail!.ru")]
        [InlineData("")]
        [InlineData("   ")]
        public void Test_AncorrectEmail(string input)
        {
            Assert.Throws<AnketaError>(() => new Person("Иванов Иван", input, "88005553535"));
        }

        [Theory]
        [InlineData(" +7 (921) 111-22-33 ")]
        [InlineData("89211112233")]
        [InlineData("+7 921 111 22 33")]
        [InlineData("+7-921-111-22-33")]
        public void Test_FormatPhone(string input)
        {
            Person person = new Person("Ивано Иван", "email@domain.com", input);

            Assert.Equal("79211112233", person.Phone);
        }

        [Theory]
        [InlineData("+792100000000")]
        [InlineData("++7921123")]
        public void Test_AncorrectPhone(string input)
        {
            Assert.Throws<AnketaError>(() => new Person("Иванов Иван", "email@domain.com", input));
        }

        [Fact]
        public void Test_MaskedPhone()
        {
            Person person = new Person("Иванов Иван", "email@domain.com", "79211112233");

            Assert.Equal("+7 (921) ***-**-33", person.MaskedPhone());
        }

        [Fact]
        public void Test_ShprtFio()
        {
            Person person = new Person("Иванов Иван", "email@domain.com", "79211112233");

            Assert.Equal("Иванов И.", person.ShortFio());
        }

        [Fact]
        public void Test_DifferentsResultsRole()
        {
            Student student = new Student("Иванов Иван", "email@domain.com", "79211112233", "09c41");
            Teacher teacher = new Teacher("Иванов Иван", "email@domain.com", "79211112233", "Кафедра И4");

            Assert.Equal("Студент", student.Role);
            Assert.Equal("Преподаватель", teacher.Role);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        public void Test_StudentNullArgument(string input)
        {
            Assert.Throws<AnketaError>(() => new Student("Иванов Иван", "email@domain.com", "79211112233", input));
        }

        [Fact]
        public void Test_RegistryStudent()
        {
            string path = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".txt");

            File.WriteAllText(path, "s;Иванов Иван;ivan@mail.ru;89991234567;ИС-21");

            try
            {
                Registry registry = new Registry();

                Assert.True(registry.Load(path));
                Assert.Single(registry.People);

                Assert.IsType<Student>(registry.People[0]);

                Student student = (Student)registry.People[0];

                Assert.Equal("Иванов Иван", student.FullName);
                Assert.Equal("ИС-21", student.Group);
            }
            finally
            {
                File.Delete(path);
            }
        }

        [Fact]
        public void Test_RegistryTeacher()
        {
            string path = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".txt");

            File.WriteAllText(path, "t;Петров Петр;pet@mail.ru;89991234568;Математика");

            try
            {
                Registry registry = new Registry();

                Assert.True(registry.Load(path));
                Assert.Single(registry.People);

                Assert.IsType<Teacher>(registry.People[0]);

                Teacher teacher = (Teacher)registry.People[0];

                Assert.Equal("Петров Петр", teacher.FullName);
                Assert.Equal("Математика", teacher.Department);
            }
            finally
            {
                File.Delete(path);
            }
        }

        [Fact]
        public void Test_EmptyString()
        {
            string path = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".txt");

            File.WriteAllText(path, "\n\n\n\nt;Петров Петр;pet@mail.ru;89991234568;Математика");

            try
            {
                Registry registry = new Registry();

                Assert.True(registry.Load(path));
                Assert.Single(registry.People);
                Assert.Empty(registry.Errors);
            }
            finally
            {
                File.Delete(path);
            }

        }
    }
}
