using WebApplication.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Data
{
    public static class DbInitializer
    {
        static DateTime RandomDay(DateTime start, Random rand)
        {
            int range = (DateTime.Today - start).Days;
            return start.AddDays(rand.Next(range));
        }

        static DateTime RandomDay(DateTime start, DateTime end, Random rand)
        {
            int range = (end - start).Days;
            return start.AddDays(rand.Next(range));
        }

        static string RandomAddress(string[] towns, string[] streets, Random rand)
        {
            return $"{towns[rand.Next(towns.GetLength(0))]}, ул. " +
                $"{streets[rand.Next(streets.GetLength(0))]}, {rand.Next(1, 100)}";
        }

        static string RandomPhone(Random rand)
        {
            return $"+375 (44) {rand.Next(000, 999).ToString()} " +
                $"{rand.Next(00, 99).ToString()} {rand.Next(00, 99).ToString()}";
        }

        static string PassportNumber(Random rand, string chars)
        {
            return $"{chars[rand.Next(chars.Length)]}{chars[rand.Next(chars.Length)]}" +
                $"{rand.Next(00000000, 9999999)}";
        }

        public static void Initialize(LanguageClassesContext db)
        {
            db.Database.EnsureCreated();
               
            // Проверка занесены ли курсы
            if (db.Courses.Any())
            {
                return;   // База данных инициализирована
            }

            // количество записей 
            const int courseNumber = 100;
            const int positionNumber = 100;
            const int listenerNumber = 100;
            const int purposeNumber = 100;
            const int employeeNumber = 1000;
            const int employeeCoursesNumber = 1000;
            const int listenerCoursesNumber = 1000;
            const int paymentNumber = 1000;

            Random rand = new Random(1);
            Random gen = new Random();
            string randChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

            string[] middleNames = { "Панов","Зайцев","Гордеев","Орехов","Назаров","Тихомиров","Вдовин","Давыдов","Кондратьев",
                "Губанов","Ларионов","Беляев","Леонтьев","Михайлов","Ковалев","Марков","Тихонов","Прохоров","Соловьев","Кузнецов",
                "Винокуров","Власов","Наумов","Матвеев","Афанасьев","Курочкин","Максимов","Романов","Павлов","Осипов","Сазонов",
                "Кузьмин","Колпаков","Тихонов","Данилов","Быков","Константинов","Кошелев","Наумов","Калачев","Касаткин","Семенов",
                "Смирнов","Иванов","Кузнецов","Поляков","Борисов","Орлов","Васильев","Родионов" };
            string[] firstNames = { "Александр", "Борис", "Филипп", "Тимур", "Владислав", "Роман", "Арсений", "Александр", "Георгий",
                "Ярослав", "Юрий", "Александр", "Максим", "Дмитрий", "Давид", "Егор", "Даниил", "Николай", "Арсений", "Юрий", "Егор",
                "Владимир", "Никита", "Леон", "Николай", "Константин", "Михаил", "Дмитрий", "Мирон", "Игорь", "Даниил", "Матвей", "Артём",
                "Денис", "Савва", "Артемий", "Владимир", "Марк", "Адам", "Тимофей", "Виктор", "Артём", "Даниил", "Ярослав", "Александр",
                "Константин", "Николай", "Павел", "Сергей" };
            string[] lastNames = { "Миронович", "Сергеевич", "Юрьевич", "Тимофеевич", "Владимирович", "Никитич", "Владимирович", "Давидович",
                "Михайлович", "Андреевич", "Максимович", "Александрович", "Дмитриевич", "Артёмович", "Тимофеевич", "Матвеевич", "Алексеевич",
                "Александрович", "Тимофеевич", "Николаевич", "Александрович", "Артёмович", "Дмитриевич", "Богданович", "Арсентьевич", "Макарович",
                "Германович", "Степанович", "Константинович", "Максимович", "Александрович", "Егорович", "Андреевич", "Даниилович", "Ярославович",
                "Дмитриевич", "Станиславович", "Иванович", "Сергеевич", "Тимофеевич", "Александрович", "Сергеевич", "Фёдорович", "Михайлович",
                "Фёдорович", "Алексеевич", "Михайлович", "Тимурович", "Тимофеевич", "Глебович" };
            string[] towns = { "Светлогорск", "Речица", "Гомель", "Минск", "Солигорск", "Витебск", "Могилев", "Мозырь", "Барановичи", "Жлобин", "Гродно", "Кобрин",
                "Полоцк", "Туров", "Глубокае", "Пинск", "Василевичи", "Орша", "Несвиж", "Бобруйск", "Слоним", "Борисов"};
            string[] streets = { "Советская", "Кирова", "Пролетарская", "Коммунарова", "Речная", "Высокая", "Озерная", "Текстильная", "Восточная", "Кленовская",
                "Кутузова", "Трудовая", "Крестьянская", "Первомайская", "Береговая", "Горная", "Добрушская", "Тургенева", "Луначарского", "Урицкого", "Гоголя",
                "Маркса", "Полесская", "Ленина", "Луговая", "Столярная", "Брянская", "Встречная", "Тимофеенко", "Бакунина", "Лермонтова", "Чехова", "Чайковского",
                "Ломоносова" };
            
            
            // Courses
            string courseName;
            string program;
            string description;
            int intensity;
            int groupSize;
            int vacanciesNumber;
            int hoursNumber;
            int cost;
            
            for (int i = 1; i < courseNumber; i++)
            {
                courseName = $"НазваниеКурса{i}";
                program = $"Программа{i}";
                intensity = rand.Next(1, 7);
                groupSize = rand.Next(8, 51);
                vacanciesNumber = rand.Next(2, 46);
                description = $"Описание{i}";
                while (vacanciesNumber > groupSize && vacanciesNumber != groupSize)
                {
                    groupSize = rand.Next(8, 51);
                    vacanciesNumber = rand.Next(2, 46);
                }
                hoursNumber = rand.Next(8, 61);
                cost = rand.Next(200, 20001);

                db.Courses.Add(new Course
                {
                    Name = courseName,
                    Program = program,
                    Intensity = intensity,
                    GroupSize = groupSize,
                    VacanciesNumber = vacanciesNumber,
                    HoursNumber = hoursNumber,
                    Cost = cost,
                    Description = description
                });
            }

            db.SaveChanges();


            // Positions
            string positionName;

            for (int i = 1; i < positionNumber; i++)
            {
                positionName = $"Должность{i}";
                db.Positions.Add(new Position { Name = positionName });
            }

            db.SaveChanges();


            // Listeners
            string surname;
            string firstName;
            string patronymic;
            DateTime date;
            string address;
            string phone;
            string passportNumber;

            for (int i = 1; i < listenerNumber; i++)
            {
                surname = middleNames[rand.Next(middleNames.GetLength(0))];
                firstName = firstNames[rand.Next(firstNames.GetLength(0))];
                patronymic = lastNames[rand.Next(lastNames.GetLength(0))];
                date = RandomDay(new DateTime(1990, 1, 1), new DateTime(2006, 1, 1), gen);
                address = RandomAddress(towns, streets, gen);
                phone = RandomPhone(gen);
                passportNumber = PassportNumber(gen, randChars);

                db.Listeners.Add(new Listener 
                { 
                    Surname = surname,
                    FirstName = firstName, 
                    Patronymic = patronymic,
                    Address = address,
                    Birthdate = date,
                    Phone = phone,
                    PassportNumber = passportNumber
                });
            }

            db.SaveChanges();


            // Purposes
            string purposeName;

            for (int i = 1; i < purposeNumber; i++)
            {
                purposeName = $"НазначениеПлатежа{i}";
                db.Purposes.Add(new Purpose { PurposeName = purposeName });
            }

            db.SaveChanges();


            // Employees
            string education;
            int positionId;

            for (int i = 1; i < employeeNumber; i++)
            {
                surname = middleNames[rand.Next(middleNames.GetLength(0))];
                firstName = firstNames[rand.Next(firstNames.GetLength(0))];
                patronymic = lastNames[rand.Next(lastNames.GetLength(0))];
                education = $"Образование{i}";
                positionId = rand.Next(1, positionNumber);

                db.Employees.Add(new Employee
                {
                    Surname = surname,
                    FirstName = firstName,
                    Patronymic = patronymic,
                    Education = education,
                    PositionId = positionId,
                });
            }

            db.SaveChanges();


            // Employees_Courses
            int courseId;
            int employeeId;

            for (int i = 1; i < employeeCoursesNumber; i++)
            {
                courseId = rand.Next(1, courseNumber);
                employeeId = rand.Next(1, employeeNumber);

                db.EmployeesCourses.Add(new EmployeesCourse
                {
                    CourseId = courseId,
                    EmployeeId = employeeId,
                });
            }

            db.SaveChanges();


            // Listeners_Courses
            int listenerId;

            for (int i = 1; i < listenerCoursesNumber; i++)
            {
                courseId = rand.Next(1, courseNumber);
                listenerId = rand.Next(1, listenerNumber);

                db.ListenersCourses.Add(new ListenersCourse
                {
                    CourseId = courseId,
                    ListenerId = listenerId,
                });
            }

            db.SaveChanges();


            // Payments
            double amount;
            int purposeId;
            
            for (int i = 1; i < paymentNumber; i++)
            {
                amount = rand.NextDouble() * (25.0 + 5.0) - 5.0;
                date = RandomDay(new DateTime(2015, 1, 1), gen);
                listenerId = rand.Next(1, listenerNumber);
                purposeId = rand.Next(1, purposeNumber);

                db.Payments.Add(new Payment
                {
                    Amount = amount,
                    Date = date,
                    ListenerId = listenerId,
                    PurposeId = purposeId,
                });
            }

            db.SaveChanges();


        }
    }
}
