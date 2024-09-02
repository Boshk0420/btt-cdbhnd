using BasketballTournament.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballTournament.Utils
{
    public static class BracketMaker
    {
        public static List<Bracket> GetBrackets(List<Group> groups)
        {
            var teams = new List<Team>();

            teams.AddRange(GetTeamsByPlacements(groups,1));
            teams.AddRange(GetTeamsByPlacements(groups,2));
            teams.AddRange(GetTeamsByPlacements(groups,3));
            teams.RemoveAt(teams.Count-1);

            PrintHat(teams);
            var matches = MakeMatches(teams);
            var brackets = FormBrackets(matches, groups);
            PrintBrackets(brackets);

            return brackets;
        }

        private static List<Team> GetTeamsByPlacements(List<Group> groups, int placement){
            var teams = new List<Team>();
            foreach(var group in groups)
            {
                teams.Add(group.Teams.ElementAt(placement-1));
            }
            teams.OrderByDescending(team => team.Points).ThenByDescending(team => team.ScoreDiff).ThenByDescending(team => team.Score);
            return teams;
        }

        private static void PrintHat(List<Team> teams)
        {
            Console.WriteLine("Šeširi:");
            Console.WriteLine("\tŠeširi D");
            Console.WriteLine($"\t\t{teams[0].Name}");
            Console.WriteLine($"\t\t{teams[1].Name}");
            Console.WriteLine("\tŠeširi E");
            Console.WriteLine($"\t\t{teams[2].Name}");
            Console.WriteLine($"\t\t{teams[3].Name}");
            Console.WriteLine("\tŠeširi F");
            Console.WriteLine($"\t\t{teams[4].Name}");
            Console.WriteLine($"\t\t{teams[5].Name}");
            Console.WriteLine("\tŠeširi G");
            Console.WriteLine($"\t\t{teams[6].Name}");
            Console.WriteLine($"\t\t{teams[7].Name}");
        }

        private static List<Bracket> FormBrackets(List<Match> matches,List<Group> groups)
        {
            var brackets = new List<Bracket>();
            var groupMatches = GetAllGroupMatches(groups);

            int i = 0;
            int j = matches.Count - 1;

            var groupMatch = groupMatches.Find(x=> 
            x.teamPlayed(matches[i].Team1, matches[j].Team1) ||
            x.teamPlayed(matches[i].Team1, matches[j].Team2) ||
            x.teamPlayed(matches[i].Team2, matches[j].Team1) ||
            x.teamPlayed(matches[i].Team2, matches[j].Team2)
            );

            if (groupMatch == null){
                var matchesInBracketA = new List<Match>
                {
                    matches[i],
                    matches[j]
                };
                brackets.Add(new Bracket("A",matchesInBracketA));
                var matchesInBracketB = new List<Match>
                {
                    matches[i+1],
                    matches[j-1]
                };
                brackets.Add(new Bracket("B", matchesInBracketB));
            }
            else
            {
                var matchesInBracketA = new List<Match>
                {
                    matches[i],
                    matches[j-1]
                };
                brackets.Add(new Bracket("A", matchesInBracketA));
                var matchesInBracketB = new List<Match>
                {
                    matches[i+1],
                    matches[j]
                };
                brackets.Add(new Bracket("B", matchesInBracketB));
            }
           

            return brackets;
        }

        private static List<Match> MakeMatches(List<Team> teams)
        {
            var matches = new List<Match>();
            var rand = new Random();
            int j = teams.Count - 1;
            for (int i = 0; i < teams.Count/2; i += 2) 
            {

                if (rand.Next(100) < 50)
                {
                    matches.Add(new Match(teams[i], teams[j],0,0));
                    matches.Add(new Match(teams[i+1], teams[j-1],0,0));
                }
                else 
                {
                    matches.Add(new Match(teams[i], teams[j-1], 0, 0));
                    matches.Add(new Match(teams[i + 1], teams[j], 0, 0));
                }


                j -= 2;
            }


            return matches;
        }

        private static void PrintBrackets(List<Bracket> brackets)
        {
            Console.WriteLine("Eliminacoina faza:");
            foreach(var bracket in brackets)
            {
                Console.WriteLine();
                foreach(var match in bracket.Match)
                {
                    Console.WriteLine($"\t{match.Team1.Name} - {match.Team2.Name}");
                }
            }
        }

        private static List<Match> GetAllGroupMatches(List<Group> groups)
        {
            var matches = new List<Match>();

            foreach (var group in groups) 
            {
                matches.AddRange(group.Matches);
            }

            return matches;
        }
    }
}
