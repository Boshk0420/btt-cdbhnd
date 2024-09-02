using BasketballTournament.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballTournament.Utils
{
    public static class ReadinessCalculator
    {
        private static int WinWeight = 50;
        private static int LoseWeight = -15;

        public static void CalculateBaseline(List<ExhibitionMatch> exhibitionMatches, List<Group> groups)
        {
            foreach (Group group in groups)
            {
                foreach (var team in group.Teams)
                {
                    team.TeamReadiness = 1000 + 100/(Math.Log(2+team.FIBARanking)) + CalculateExhibiton(exhibitionMatches, groups, team.ISOCode, team.FIBARanking);
                }
            }
        }

        private static int CalculateExhibiton(List<ExhibitionMatch> exhibitionMatches, List<Group> groups, string TeamISOCode, int TeamRanking)
        {
            int score = 0;

            ExhibitionMatch? matches = exhibitionMatches.Find(x => x.TeamISOCode == TeamISOCode);
            if (matches == null) return score;

            foreach (var matchData in matches.ExhibitionMatches)
            {
                var oppRanking = GetRankingForTeam(groups, TeamISOCode);
                if (oppRanking == 0) continue;

                var resultDifference = matchData.GetResultDifference();
                if (resultDifference > 0)
                {
                    score += WinWeight + resultDifference * TeamRanking / oppRanking;
                }
                else
                {
                    score += LoseWeight + resultDifference * oppRanking / TeamRanking;
                }

            }


            return score;
        }

        private static int GetRankingForTeam(List<Group> groups, string TeamISOCode)
        {
            foreach (var group in groups)
            {
                foreach (var team in group.Teams)
                {
                    if (team.ISOCode == TeamISOCode) return team.FIBARanking;
                }

            }

            return 0;
        }
    }
}
