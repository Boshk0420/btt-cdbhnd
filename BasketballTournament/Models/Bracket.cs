using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballTournament.Models
{
    public class Bracket
    {
        public string Name { get; set; }
        public List<Match> Match { get; set; }

        public Bracket() 
        { 
            Name = string.Empty;
            Match = new List<Match>();
        }

        public Bracket(string name,List<Match> match)
        {
            this.Name = name;
            this.Match = match;
        }



    }
}
