using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class ResearchTeamCollection<TKey>
    {
        
        private readonly Dictionary<TKey, ResearchTeam> _researchTeams = new Dictionary<TKey, ResearchTeam>();
        private readonly KeySelector<TKey> _keySelector;

        
         public delegate TKey KeySelector<TKey>(ResearchTeam rt);

   
        public ResearchTeamCollection(KeySelector<TKey> keySelector)
        {
            _keySelector = keySelector;
        }


        public void AddDefaults(int count = 5)
        {
            for (int i = 0; i < count; i++)
            {
                var rt = new ResearchTeam();
                _researchTeams[_keySelector(rt)] = rt;
            }
        }

        public void AddResearchTeams(params ResearchTeam[] teams)
        {
            foreach (var team in teams)
            {
                var key = _keySelector(team);
                _researchTeams[key] = team;
            }
        }

        public override string ToString()
        {
            return string.Join("\n\n",
                _researchTeams.Select(pair =>
                    $"Ключ: {pair.Key}\n{pair.Value.ToString()}"
                ));
        }

    
        public string ToShortString()
        {
            return string.Join("\n",
                _researchTeams.Select(pair =>
                    $"Ключ: {pair.Key}\n" +
                    $"Организация: {pair.Value.Organisation}\n" +
                    $"Тема: {pair.Value.Theme}\n" +
                    $"Участников: {pair.Value.ProjectParticipants.Count}\n" +
                    $"Публикаций: {pair.Value.Publications.Count}"
                ));
        }

        public DateTime LastPublicationDate =>
            _researchTeams.Values
                .SelectMany(rt => rt.Publications)
                .Select(p => p._TimeOfPublication)
                .DefaultIfEmpty(DateTime.MinValue)
                .Max();

   
        public IEnumerable<KeyValuePair<TKey, ResearchTeam>> TimeFrameGroup(TimeFrame value)
        {
            return _researchTeams
                .Where(pair => pair.Value.ResearchDuration == value)
                .OrderBy(pair => pair.Key);
        }

  
        public IEnumerable<IGrouping<TimeFrame, KeyValuePair<TKey, ResearchTeam>>> GroupByTimeFrame =>
            _researchTeams
                .GroupBy(pair => pair.Value.ResearchDuration)
                .OrderBy(g => g.Key);

      
    }

}
