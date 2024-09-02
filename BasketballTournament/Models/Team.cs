using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BasketballTournament.Models
{
    public class Team
    {
        [JsonPropertyName("Team")]
        public string Name { get; set; }

        [JsonPropertyName("ISOCode")]
        public string ISOCode { get; set; }

        [JsonPropertyName("FIBARanking")]
        public int FIBARanking { get; set; }

        [JsonIgnore]
        public double TeamReadiness { get; set; }

        [JsonIgnore]
        public int Points { get; private set; }

        [JsonIgnore]
        public int Wins { get; private set; }

        [JsonIgnore]
        public int Loses { get; private set; }

        [JsonIgnore]
        public int Score { get; set; }

        [JsonIgnore]
        public int OppScore { get; set; }

        [JsonIgnore]
        public int ScoreDiff { get; set; } // used for three way tie



        public Team() 
        {
            Name = string.Empty;
            ISOCode = string.Empty;
            FIBARanking = 0;
            SetDefaultValues();
        }

        public Team(string Name,string ISOCode, int FIBARanking)
        {
            this.Name = Name;
            this.ISOCode = ISOCode;
            this.FIBARanking = FIBARanking;
            SetDefaultValues();
            
        }

        public void AddWin()
        {
            Wins++;
            Points += 2;
        }

        public void AddLose()
        {
            Loses++;
            Points++;
        }

        private void SetDefaultValues()
        {
            TeamReadiness = 1000;
            Points = 0;
            Wins = 0;
            Loses = 0;
            Score = 0;
            OppScore = 0;
        }


    }
}
