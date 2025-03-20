using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
   public class Paper : IComparable<Paper>, IComparer<Paper>
    {
        public string _Publication { get; set; }
        public Person _Author { get; set; }
        public DateTime _TimeOfPublication { get; set; }

        public Paper(string publication, Person author, DateTime timeOfPublication)
        {
            _Publication = publication;
            _Author = author;
            _TimeOfPublication = timeOfPublication;
        }

        public Paper() : this("Base Publication", new Person(), new DateTime(2000, 01, 02)) { }


        public override string ToString()
        {
            return $"{_Publication} ({_TimeOfPublication:yyyy-MM-dd}) by {_Author}";
        }

        public virtual object DeepCopy()
        {
            return new Paper(_Publication, (Person)_Author.DeepCopy(), _TimeOfPublication);
        }

        // Реализация IComparable<Paper> (сравнение по дате)
        public int CompareTo(Paper other)
        {
            return _TimeOfPublication.CompareTo(other._TimeOfPublication);
        }

        // Реализация IComparer<Paper> (сравнение по названию)
        public int Compare(Paper x, Paper y)
        {
            return string.Compare(x._Publication, y._Publication, StringComparison.Ordinal); // StringComparison - Сравнивать строки, используя правила обычной (двоичной) сортировки.
        }

        // Вспомогательный класс для сравнения по фамилии автора
        public class AuthorSurnameComparer : IComparer<Paper>
        {
            public int Compare(Paper x, Paper y)
            {
                return string.Compare(
                    x._Author.PersonSurname,
                    y._Author.PersonSurname,
                    StringComparison.Ordinal
                );
            }
        }
    }

}
