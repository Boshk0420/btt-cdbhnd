using BasketballTournament.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BasketballTournament.Utils
{
    public static class DataLoader
    {

        public static List<Models.Group> LoadGroups(string path)
        {
            var list = new List<Models.Group>();
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                var groupDict = JsonSerializer.Deserialize<Dictionary<string, List<Team>>>(json);
                if (groupDict != null && groupDict.Count > 0)
                {
                    foreach (var item in groupDict)
                    {
                        list.Add(new Models.Group(item.Key, item.Value));
                    }
                }
            }
            return list;
        }

        public static List<ExhibitionMatch> LoadExhibitions(string path)
        {
            var list = new List<ExhibitionMatch>();
            using (StreamReader r = new StreamReader(path))
            {
                string json = r.ReadToEnd();
                var exhibitionsDict = JsonSerializer.Deserialize<Dictionary<string, List<ExhibitionMatchData>>>(json);
                if (exhibitionsDict != null && exhibitionsDict.Count > 0)
                {
                    foreach (var item in exhibitionsDict)
                    {
                        list.Add(new ExhibitionMatch(item.Key, item.Value));
                    }
                }
            }
            return list;
        }
    }
}
