using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public enum TimeFrame { Year, TwoYears, Long }
    class Program
    {
        static void Main(string[] args)
        {
            int NOM = 3;
            if (NOM == 1)
            {
                Console.WriteLine("\n\n1");
                Team MyTeam1 = new Team("MIET4", 7);
                Team MyTeam2 = new Team("MIET4", 7);
                Console.WriteLine(MyTeam1.Equals(MyTeam2));
                Console.WriteLine(MyTeam1 == MyTeam2);
                Console.WriteLine(string.Format(" MyTeam1: {0}, MyTeam2: {1} ", MyTeam1.GetHashCode(), MyTeam2.GetHashCode()));

                Console.WriteLine("\n||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\n\n2");

                try
                {
                    MyTeam2.RegistrationNumber = -2;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.WriteLine("\n||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\n\n3");

                ResearchTeam MyTeam3 = new ResearchTeam();

                MyTeam3.AddMembers(new Person[3] { new Person("Semen", "Svechkin", new DateTime(1999, 01, 01)), new Person("Some", "Person", new DateTime(1990, 11, 13)), new Person() });
                MyTeam3.AddPapers(new Paper[2] { new Paper("SP", new Person("Semen", "Svechkin", new DateTime(1999, 01, 01)), new DateTime(2025, 04, 03)), new Paper() });
                Console.WriteLine(MyTeam3);

                Console.WriteLine("\n||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\n\n4");
                Console.WriteLine(MyTeam3.Team);


                Console.WriteLine("\n||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\n\n5");

                ResearchTeam MyTeam4 = (ResearchTeam)MyTeam3.DeepCopy();
                MyTeam3.Organisation = "another";
                MyTeam3.RegistrationNumber = 22;
                Console.WriteLine(MyTeam3.ToString());
                Console.WriteLine("\n");
                Console.WriteLine(MyTeam4.ToString());

                Console.WriteLine("\n||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\n\n6");

                foreach (Person pers in MyTeam3.GetMembersWithoutPublications())
                {
                    Console.WriteLine(pers);
                }

                Console.WriteLine("\n||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\n\n7");

                foreach (Paper pap in MyTeam3.GetRecentPublications(2))
                {
                    Console.WriteLine(pap);
                }

                Console.ReadKey();
            }






            if (NOM == 2)
            {
                var test = new TestCollections<int, string>(5,
                j => new KeyValuePair<int, string>(j, $"Value_{j}")
            );

                test.MeasureSearchTime();
            }



            if (NOM == 3)
            {
              
                // 1. Создание ResearchTeam и тестирование сортировок
              
                var person1 = new Person("Semen", "Smith", new DateTime(1990, 5, 10));
                var person2 = new Person("Simon", "Johnson", new DateTime(1985, 8, 15));

                var researchTeam = new ResearchTeam(
                    theme: "Quantum Computing",
                    organisation: "MIET PIN24",
                    regNumber: 777,
                    duration: TimeFrame.Long
                );

                researchTeam.AddMembers(person1, person2);
                researchTeam.AddPapers(
                    new Paper("NAME1", person1, new DateTime(2023, 4, 20)),
                    new Paper("NAME2", person2, new DateTime(2020, 1, 1)),
                    new Paper("NAME3", person1, new DateTime(2025, 12, 31))
                );

                Console.WriteLine("Исходные данные:");
                Console.WriteLine(researchTeam);

                // Сортировка по дате публикации
                researchTeam.SortPublicationsByDate();
                Console.WriteLine("\nПосле сортировки по дате:");
                Console.WriteLine(researchTeam);

                // Сортировка по названию публикации
                researchTeam.SortPublicationsByTitle();
                Console.WriteLine("\nПосле сортировки по названию:");
                Console.WriteLine(researchTeam);

                // Сортировка по фамилии автора
                researchTeam.SortPublicationsByAuthorSurname();
                Console.WriteLine("\nПосле сортировки по фамилии автора:");
                Console.WriteLine(researchTeam);

              
                // 2. Создание ResearchTeamCollection<string>
              

                Console.WriteLine("\n||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\n\n2");
                var collection = new ResearchTeamCollection<string>(
                    keySelector: rt => $"{rt.Organisation}_{rt.RegistrationNumber}"
                );

                collection.AddResearchTeams(
                    researchTeam,
                    new ResearchTeam("AI", "MIET", 123, TimeFrame.TwoYears),
                    new ResearchTeam("Miet Networks", "MIET DEVs", 456, TimeFrame.Year)
                );

                Console.WriteLine("\n\nКоллекция ResearchTeamCollection:");
                Console.WriteLine(collection);

              
                // 3. Операции с коллекцией
              
                Console.WriteLine("\n||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\n\n3");
                Console.WriteLine("\nПоследняя дата публикации: " + collection.LastPublicationDate);

                Console.WriteLine("\nГруппы по TimeFrame:");
                foreach (var group in collection.GroupByTimeFrame)
                {
                    Console.WriteLine($"\nГруппа {group.Key}:");
                    foreach (var item in group)
                        Console.WriteLine($"Ключ: {item.Key}\n{item.Value.ToShortString()}");
                }

              
                // 4. Тестирование скорости поиска
              
                Console.WriteLine("\n||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||\n\n4");
                Console.Write("\nВведите число элементов для TestCollection: ");
                int n;
                while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
                    Console.WriteLine("Ошибка! Введите целое число > 0:");

                var test = new TestCollections<Team, ResearchTeam>(
                    count: n,
                    generator: j => new KeyValuePair<Team, ResearchTeam>(
                        new Team($"Org_{j+1}", j+1),
                        new ResearchTeam($"Theme_{j+1}", $"Org_{j+1}", j+1, TimeFrame.Year)
                    )
                );

                test.MeasureSearchTime();

                Console.ReadKey();
            }
        }
    }
}
