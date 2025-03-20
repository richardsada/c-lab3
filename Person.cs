using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Person
    {
        public string PersName { get; private set; }
        public string PersonSurname { get; private set; }
        public DateTime PersonBirthday { get; private set; }

        public int IntBirthday
        {
            get { return PersonBirthday.Year * 10000 + PersonBirthday.Month * 100 + PersonBirthday.Day; }
            set { PersonBirthday = new DateTime(value / 10000, (value / 100) % 100, value % 100); }
        }

        public Person(string Name, string Surname, DateTime Birthday)
        {
            PersName = Name;
            PersonSurname = Surname;
            PersonBirthday = Birthday;
        }

        public Person() : this("Person", "Common", new DateTime(2000, 11, 11)) { }

        public override string ToString()
        {
            return $"{PersName} {PersonSurname} was born {PersonBirthday:yyyy-MM-dd}";
        }

        public string ToShortString()
        {
            return $"{PersName} {PersonSurname}";
        }

        public override bool Equals(object obj)
        {
            return obj is Person other &&
                   PersName == other.PersName &&
                   PersonSurname == other.PersonSurname &&
                   PersonBirthday == other.PersonBirthday;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PersName, PersonSurname, PersonBirthday);
        }

        public static bool operator ==(Person lpers, Person rpers)
        {
            if (ReferenceEquals(lpers, rpers))
            {
                return true;
            }

            if (lpers is null || rpers is null)
            {
                return false;
            }

            return lpers.Equals(rpers);
        }

        public static bool operator !=(Person lpers, Person rpers)
        {
            return !(lpers == rpers);
        }

        public virtual object DeepCopy()
        {
            return new Person(PersName, PersonSurname, PersonBirthday);
        }
    }
}
