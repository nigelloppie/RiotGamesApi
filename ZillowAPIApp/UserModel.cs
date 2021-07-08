using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZillowAPIApp
{
    public class UserModel
    {
        public string SummonerName;
        public string Level;
        public string Tier;
        public string Rank;
        public string Wins;
        public string Losses;
        public string Icon;
        public string LP;
        public string Champ;
        public UserModel(string summonerName, string level, string tier,string rank, string wins, string losses, string icon, string lp, string champ)
        {
            SummonerName = summonerName;
            Level = level;
            Tier = tier;
            Rank = rank;
            Wins = wins;
            Losses = losses;
            Icon = icon;
            LP = lp;
            Champ = champ;
        }
    }
}