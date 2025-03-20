using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Lab3
{
    public class ResearchTeam : Team, InameAndCopy
    {
        public string Theme { get; set; }
        public TimeFrame ResearchDuration { get; set; }
        public List<Person> _projectParticipants = new List<Person>();
        public List<Paper> _publications = new List<Paper>();

        public List<Person> ProjectParticipants
        {
            get { return _projectParticipants; }
            set { _projectParticipants = value; }
        }

        public List<Paper> Publications
        {
            get { return _publications; }
            set { _publications = value; }
        }

        public ResearchTeam(string theme, string organisation, int regNumber, TimeFrame duration)
            : base(organisation, regNumber)
        {
            Theme = theme;
            ResearchDuration = duration;
        }

        public ResearchTeam() : base("MIET CORP", 13)
        {
            Theme = "Studying";
            ResearchDuration = TimeFrame.Year;
        }

        public Paper LastPaper
        {
            get
            {
                if (Publications.Count == 0) return null;
                return Publications.Cast<Paper>()
                    .OrderByDescending(p => p._TimeOfPublication)
                    .FirstOrDefault();
            }
        }

        public void AddPapers(params Paper[] papers)
        {
            Publications.AddRange(papers);
        }

        public override string ToString()
        {
            string publications = string.Join("\n", Publications.Cast<Paper>());
            string participants = string.Join("\n", ProjectParticipants.Cast<Person>());
            return $"{base.ToString()}\nTheme: {Theme}\nDuration: {ResearchDuration}" +
                   $"\nParticipants:\n{participants}\n\nPublications:\n{publications}";
        }

        public string ToShortString()
        {
            return $"{base.ToString()}\nTheme: {Theme}\nDuration: {ResearchDuration}";
        }

        public void AddMembers(params Person[] members)
        {
            ProjectParticipants.AddRange(members);
        }

        public Team Team
        {
            get => new Team(Organisation, RegistrationNumber);
            set
            {
                Organisation = value.Organisation;
                RegistrationNumber = value.RegistrationNumber;
            }
        }

        public override object DeepCopy()
        {
            var copy = new ResearchTeam(Theme, Organisation, RegistrationNumber, ResearchDuration)
            {
                ProjectParticipants = ProjectParticipants.Select(p => (Person)p.DeepCopy()).ToList(),
                Publications = Publications.Select(p => (Paper)p.DeepCopy()).ToList()
            };
            return copy;
        }

        public IEnumerable<Person> GetMembersWithoutPublications()
        {
            foreach (Person p in ProjectParticipants)
                if (!Publications.Cast<Paper>().Any(paper => paper._Author.Equals(p)))
                    yield return p;
        }

        public IEnumerable<Paper> GetRecentPublications(int years)
        {
            int currentYear = DateTime.Now.Year;
            foreach (Paper p in Publications)
                if (currentYear - p._TimeOfPublication.Year <= years)
                    yield return p;
        }
        // Сортировки
        public void SortPublicationsByDate() => Publications.Sort();
        public void SortPublicationsByTitle() => Publications.Sort(new Paper());
        public void SortPublicationsByAuthorSurname() => Publications.Sort(new Paper.AuthorSurnameComparer());
    }
}