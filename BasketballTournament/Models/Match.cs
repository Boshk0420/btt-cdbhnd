using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballTournament.Models
{
    public class Match
    {
        public Team Team1 { get; set; }  
        public Team Team2 { get; set; }
        public int Team1Score { get; set; }
        public int Team2Score { get; set; }
        public Team Winner => Team1Score > Team2Score ? Team1 : Team2;
        public Team Loser => Team1Score < Team2Score ? Team1 : Team2;

        public Match(Team team1, Team team2, int team1Score, int team2Score)
        {
            Team1 = team1;
            Team2 = team2;
            Team1Score = team1Score;
            Team2Score = team2Score;
        }

        public bool teamPlayed(Team team1, Team team2)
        {
            if (
                (Team1.ISOCode == team1.ISOCode && Team2.ISOCode == team2.ISOCode) ||
                (Team2.ISOCode == team1.ISOCode && Team1.ISOCode == team2.ISOCode)
                ) return true;
            return false;
        }
    }
}
