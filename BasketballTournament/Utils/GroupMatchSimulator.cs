using BasketballTournament.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballTournament.Utils
{
    static class GroupMatchSimulator
    {

        public static void SimulateGroupPhase(List<Group> groups) 
        {
            Console.WriteLine("Grupna faza:");
            //Not the prettiest for loops :/
            foreach (var group in groups)
            {
                Console.WriteLine($"\tGrupa {group.Name}:");
                foreach (var team1 in group.Teams)
                {
                    foreach(var team2 in group.Teams) 
                    { 
                        if(!group
                            .Matches
                            .Any(
                                match => 
                                    (match.Team1.Name == team1.Name && match.Team2.Name == team2.Name) ||
                                    (match.Team1.Name == team2.Name && match.Team2.Name == team1.Name) 
                                    )
                            && team1.Name != team2.Name
                            ) group.Matches.Add(SimulateMatch(team1, team2));
                    }
                }
            }
        }

        public static void SimulateBracketPhase(List<Bracket> brackets)
        {
            var quarterMatches = new List<Match>();
            Console.WriteLine("Četvrtfinale:");
            foreach(var bracket in brackets)
            {
                Console.WriteLine();
                foreach(var match in bracket.Match)
                {
                    quarterMatches.Add(SimulateMatch(match.Team1, match.Team2));
                }
            }

            Console.WriteLine("Polufinale:");

            var halfMatches = new List<Match>
            {
                SimulateMatch(quarterMatches[0].Winner, quarterMatches[1].Winner),
                SimulateMatch(quarterMatches[2].Winner, quarterMatches[3].Winner)
            };

            Console.WriteLine("Utakmica za trece mesto:");

            var thirdPlace = SimulateMatch(halfMatches[0].Loser, halfMatches[1].Loser);

            Console.WriteLine("Finale:");

            var finalMatch = SimulateMatch(halfMatches[0].Winner, halfMatches[1].Winner);

            Console.WriteLine("Medalje:");

            Console.WriteLine($"\t1. {finalMatch.Winner.Name}");
            Console.WriteLine($"\t2. {finalMatch.Loser.Name}");
            Console.WriteLine($"\t3. {thirdPlace.Winner.Name}");


        }

        private static Match SimulateMatch(Team team1, Team team2)
        {
            var team1Score = CalculateScore(team1);
            var team2Score = CalculateScore(team2);
            
            CheckScore(ref team1Score,ref team2Score);

            Console.WriteLine($"\t\t{team1.Name} - {team2.Name} ({team1Score}:{team2Score})");

            UpdateTeamInfo(team1,team2,team1Score,team2Score);

            return new Match(team1,team2,team1Score,team2Score);
        }

        private static int CalculateScore(Team team)
        { 
            var random = new Random();

            double baseline = 1000;
            int minScore = 30;
            int maxScore = 140;

            double readinessNorm = (team.TeamReadiness + random.Next(-400,400)) / baseline;

            double scoreRange = maxScore - minScore;
            double midpoint = minScore + scoreRange / 2.0;

            double readinessFactor = (readinessNorm - 1) * (scoreRange / 2.0);
            int points = (int)Math.Round(midpoint + readinessFactor);

            return Math.Clamp(points, minScore, maxScore);
        }

        private static void CheckScore(ref int team1Score,ref int team2Score)
        {
            if (team1Score == team2Score)
            {
                var random = new Random();
                var num = random.Next(0, 100);
                if (num < 50)
                {
                    team1Score += 2;
                }
                else
                {
                    team2Score += 2;
                }
            }
        }

        private static void UpdateTeamInfo(Team team1, Team team2, int team1Score, int team2Score)
        {
            team1.Score += team1Score;
            team1.OppScore += team2Score;

            team2.Score += team2Score;
            team2.OppScore += team1Score;


            if (team1.Score > team2Score)
            {
                team1.AddWin();
                team2.AddLose();
                team1.TeamReadiness += 25;
                team2.TeamReadiness -= 15;
            }
            else
            {
                team1.AddLose();
                team2.AddWin();
                team2.TeamReadiness += 25;
                team1.TeamReadiness -= 15;
            }
        }

    }
}
