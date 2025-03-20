using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Team : InameAndCopy
    {
        // Защищенные поля
        protected string _organisation;
        protected int _registrationNumber;

        // Конструктор с параметрами
        public Team(string organisation, int registrationNumber)
        {
            Organisation = organisation;
            RegistrationNumber = registrationNumber;
        }

        // Конструктор без параметров
        public Team() : this("Unknown organisation", 999) { }

        // Свойство для доступа к полю с названием организации
        public string Organisation
        {
            get => _organisation;
            set => _organisation = value;
        }

        // Свойство для доступа к полю с номером регистрации
        public int RegistrationNumber
        {
            get => _registrationNumber;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "Registration number must be more than 0.");
                }
                _registrationNumber = value;
            }
        }

        // Виртуальный метод для создания глубокой копии объекта
        public virtual object DeepCopy()
        {
            return new Team(this.Organisation, this.RegistrationNumber);
        }

        // Переопределение метода Equals
        public override bool Equals(object obj)
        {
            if (obj is Team other)
            {
                return other.Organisation == this.Organisation && other.RegistrationNumber == this.RegistrationNumber;
            }
            return false;
        }

        // Переопределение операторов == и !=
        public static bool operator ==(Team lTeam, Team rTeam)
        {
            if (ReferenceEquals(lTeam, rTeam))
            {
                return true;
            }

            if (lTeam is null || rTeam is null)
            {
                return false;
            }

            return false;
        }

        public static bool operator !=(Team lTeam, Team rTeam)
        {
            return !(lTeam == rTeam);
        }

        // Переопределение метода GetHashCode
        public override int GetHashCode()
        {
            return HashCode.Combine(_organisation, _registrationNumber);
        }

        // Переопределение метода ToString
        public override string ToString()
        {
            return $"Team of organisation {Organisation} with registration number {RegistrationNumber}";
        }

        // Реализация интерфейса INameAndCopy
        public string Name
        {
            get => $"Team of organisation {Organisation} with registration number {RegistrationNumber}";
            set => Organisation = value;
        }
    }
}
