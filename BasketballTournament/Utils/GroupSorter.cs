using BasketballTournament.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace BasketballTournament.Utils
{
    static class GroupSorter
    {


        
       public static void SortTeamsInGroup(List<Models.Group> groups) 
       {
            foreach (var group in groups)
            {
                group.Teams = group.Teams.OrderByDescending(team => team.Points).ToList();
            }

            CheckTies(groups);
            PrintGroups(groups);
        }

       private static void PrintGroups(List<Models.Group> groups)
        {
            Console.WriteLine("Konačan plasman u grupama:");
            foreach (var group in groups)
            {
                int i = 1;
                Console.WriteLine($"\tGrupa {group.Name} (Ime - \t\t\tpobede/porazi/bodovi/postignuti koševi/primljeni koševi/koš razlika):");
                foreach (var team in group.Teams)
                {
                    var scoreDiff = team.Score - team.OppScore;
                    string scoreDiffStr = scoreDiff > 0 ? "+" + scoreDiff.ToString() : scoreDiff.ToString(); 
                    Console.WriteLine($"\t\t{i}. {team.Name} \t\t {team.Wins} /\t {team.Loses} /\t {team.Points} /\t {team.Score} /\t\t {team.OppScore} /\t\t {scoreDiffStr} ");
                    i++;
                }
            }
        }

        private static void CheckTies(List<Models.Group> groups)
        {
            foreach(var group in groups) { 
                for(int i = 0; i<group.Teams.Count-1;i++) 
                {
                    int j = i + 1;
                    int k = j + 1;

                    if(k<group.Teams.Count && group.Teams.ElementAt(i).Points == group.Teams.ElementAt(k).Points)
                    {
                        ThreeWayTie(group.Teams.ToList(),group.Matches.ToList(),i);
                    }

                    if(j<group.Teams.Count && group.Teams.ElementAt(i).Points == group.Teams.ElementAt(j).Points)
                    {
                        HeadToHeadTie(group.Teams.ToList(),group.Matches.ToList(), i);
                    }

                }   
            }
        }

        private static void HeadToHeadTie(List<Team> teams,List<Models.Match> matches,int i)
        {
            int j = i + 1;
            var match = matches.Find(x => x.teamPlayed(teams[i], teams[j]));

            if (match == null) return;
                
            if (match.Winner.ISOCode == teams[j].ISOCode)
                ReplaceTeams(teams, i, j);
           
        }

        private static void ThreeWayTie(List<Team> teams,List<Models.Match> matches,int i)
        {
            int j = i + 1;
            int k = j + 1;

            var matchIvJ = matches.Find(x => x.teamPlayed(teams[i], teams[j]));
            var matchJvK = matches.Find(x => x.teamPlayed(teams[j], teams[k]));
            var matchKvI = matches.Find(x => x.teamPlayed(teams[k], teams[i]));

            if (matchIvJ == null || matchJvK == null || matchKvI == null) return;

            teams[i].ScoreDiff = GetScoreForMatch(matchIvJ, teams[i]) + GetScoreForMatch(matchKvI, teams[i]);
            teams[j].ScoreDiff = GetScoreForMatch(matchIvJ, teams[j]) + GetScoreForMatch(matchJvK, teams[j]);
            teams[k].ScoreDiff = GetScoreForMatch(matchKvI, teams[k]) + GetScoreForMatch(matchJvK, teams[k]);


            teams = teams.OrderByDescending(team => team.Points)
                .ThenByDescending(team => team.ScoreDiff)
                .ToList();
            
            
        }

        private static int GetScoreForMatch(Models.Match match,Team team)
        {
            if (match.Team1.ISOCode == team.ISOCode) return match.Team1Score - match.Team2Score;
            return match.Team2Score - match.Team1Score;
        }

        private static void ReplaceTeams(List<Team> teams,int i, int j)
        {
            var tmp = teams[i];
            teams[i] = teams[j];
            teams[j] = tmp;
        }

        
        
        

    }
}
