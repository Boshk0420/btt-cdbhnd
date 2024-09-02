using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballTournament.Models
{
    public class ExhibitionMatch
    {
        public string TeamISOCode { get; set; }
        public ICollection<ExhibitionMatchData> ExhibitionMatches { get; set; }

        public ExhibitionMatch(string teamISOCode, ICollection<ExhibitionMatchData> exhibitionMatches)
        {
            this.TeamISOCode = teamISOCode;
            this.ExhibitionMatches = exhibitionMatches;
        }

        public ExhibitionMatch() 
        {
            this.TeamISOCode = string.Empty;
            this.ExhibitionMatches = new List<ExhibitionMatchData>();
        }

    }
}
