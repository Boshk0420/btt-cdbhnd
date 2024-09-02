using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballTournament.Models
{
    public class Group
    {
        public string Name { get; set; }
        public List<Team> Teams { get; set; }
        public List<Match> Matches { get; set; }

        public Group() {
            this.Name = string.Empty;
            this.Teams = new List<Team>();
            this.Matches = new List<Match>();
        }

        public Group(string Name, List<Team> Teams) {
            this.Name = Name;
            this.Teams = Teams;
            this.Matches= new List<Match>();
        }
    }
}
