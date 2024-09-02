using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BasketballTournament.Models
{
    public class ExhibitionMatchData
    {

        [JsonPropertyName("Opponent")]
        public string OpponentISOCode { get; set; }

        [JsonPropertyName("Result")]
        public string Result { get; set; }

        public ExhibitionMatchData() 
        { 
            OpponentISOCode = string.Empty;
            Result = string.Empty;
        }

        public ExhibitionMatchData(DateOnly Date,string OpponentISOCode, string Result)
        {
            this.OpponentISOCode = OpponentISOCode;
            this.Result = Result;
        }

        public int GetResultDifference()
        {
            var split = Result.Split('-');
            var home = int.Parse(split[0]);
            var opponent = int.Parse(split[1]);

            return home - opponent;
        }
    }
}
