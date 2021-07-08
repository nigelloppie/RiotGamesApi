using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace ZillowAPIApp
{
    public class ChampionModel
    {
        public BitmapImage Icon;
        public string Name;
        public string Title;
        public string Blurb;
        public string Attack;
        public string Defense;
        public string Magic;
        public string Difficulty;

        public ChampionModel(string name, string title, string blurb, string attack, string defense, string magic, string difficulty)
        {
            // manipulate data due to API data inconsistency
            string linkName = String.Concat(name.Where(c => !Char.IsWhiteSpace(c) && !Char.IsPunctuation(c)));
            if (linkName.Length >= 5)
            {
                linkName = linkName.Substring(0, 3) + Char.ToLower(linkName[3]) + linkName.Substring(4);
            }
            // set properties from args
            Icon = new BitmapImage(new Uri("http://ddragon.leagueoflegends.com/cdn/11.8.1/img/champion/" + linkName + ".png", UriKind.Absolute));
            Name = name;
            Title = title;
            Blurb = blurb;
            Attack = attack;
            Defense = defense;
            Magic = magic;
            Defense = defense;
            Difficulty = difficulty;
        }
    }
}
