using BasketballTournament.Models;
using BasketballTournament.Utils;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;

namespace BasketballTournament
{
    internal class Program
    {
        public const string GroupsPathVisualStudio = "../../../Data/groups.json";
        public const string ExhibitionsPathVisualStudio = "../../../Data/exibitions.json";

        public const string GroupsPath = "./Data/groups.json";
        public const string ExhibitionsPath = "./Data/exibitions.json";



        static void Main(string[] args)
        {
            List<Group> groups = DataLoader.LoadGroups(GroupsPath);
            if(groups.Count == 0)
            {
                Console.WriteLine("ERROR while loading groups data!");
                Console.WriteLine("Terminating program!");
                return;
            }

            List<ExhibitionMatch> exhibitionMatches = DataLoader.LoadExhibitions(ExhibitionsPath);
            if(exhibitionMatches.Count == 0) 
            {
                Console.WriteLine("ERROR while loading exhibition match data!");
                Console.WriteLine("Terminating program!");
                return;
            }

            Console.WriteLine("Data loaded in successfully!");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("Calculating baselines readiness for teams!");
            ReadinessCalculator.CalculateBaseline(exhibitionMatches, groups);
            GroupMatchSimulator.SimulateGroupPhase(groups);
            GroupSorter.SortTeamsInGroup(groups);
            var brackets = BracketMaker.GetBrackets(groups);
            GroupMatchSimulator.SimulateBracketPhase(brackets);

                    
            
        }

        

        

        

        

    }
}