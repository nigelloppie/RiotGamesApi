using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace ZillowAPIApp
{
    public class ChampionViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ChampionModel> champList = new ObservableCollection<ChampionModel>();
        public List<ChampionModel> AllChamps = new List<ChampionModel>();

        private MainPage page;
        public string SelectedName;
        public string SelectedTitle;
        public string SelectedBlurb;
        public string SelectedAttack;
        public string SelectedDefense;
        public string SelectedMagic;
        public string SelectedDifficulty;

        private ChampionModel _selectedChampion;
        public ChampionModel SelectedChampion
        {
            get { return _selectedChampion; }

            set
            {
                _selectedChampion = value;

                if (value == null)
                {
                    SelectedName = "No Champion Selected";
                    SelectedTitle = "";
                    SelectedBlurb = "";
                    SelectedAttack = "";
                    SelectedDefense = "";
                    SelectedMagic = "";
                    SelectedDifficulty = "";

                    page.champImg.Source = null;
                }
                else
                {
                    SelectedName = value.Name;
                    SelectedTitle = value.Title;
                    SelectedBlurb = value.Blurb;
                    SelectedAttack = "Attack: " + value.Attack;
                    SelectedDefense = "Defense: " + value.Defense;
                    SelectedMagic = "Magic: " + value.Magic;
                    SelectedDifficulty = "Difficulty: " + value.Difficulty;
                    
                    string linkName = String.Concat(SelectedName.Where(c => !Char.IsWhiteSpace(c) && !Char.IsPunctuation(c)));

                    if (linkName.Length >= 5)
                    {
                        linkName = linkName.Substring(0, 3) + Char.ToLower(linkName[3]) + linkName.Substring(4);
                    }
                    page.champImg.Source = new BitmapImage(new Uri("http://ddragon.leagueoflegends.com/cdn/img/champion/splash/" + linkName + "_0.jpg", UriKind.Absolute));
                }

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedName"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedTitle"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedBlurb"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedAttack"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedDefense"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedMagic"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedDifficulty"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private string _filter;
        public string Filter
        {
            get { return _filter; }
            set
            {
                if (value == _filter)
                { return; }
                _filter = value;
                PerformFiltering();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filter)));
            }
        }

        public ChampionViewModel(MainPage mainPage)
        {
            page = mainPage;
            getData();
        }

        public void getData()
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString("http://ddragon.leagueoflegends.com/cdn/11.8.1/data/en_US/champion.json");
                var champions = Newtonsoft.Json.Linq.JObject.Parse(json);

                var props = champions["data"].Select(x => ((JProperty)x).Name).ToList();

                foreach (var name in props)
                {
                    // get properties
                    string champName = (string)champions["data"][name]["name"];
                    string champTitle = (string)champions["data"][name]["title"];
                    string champBlurb = (string)champions["data"][name]["blurb"];
                    string champAttack = (string)champions["data"][name]["info"]["attack"];
                    string champDefense = (string)champions["data"][name]["info"]["defense"];
                    string champMagic = (string)champions["data"][name]["info"]["magic"];
                    string champDifficulty = (string)champions["data"][name]["info"]["difficulty"];

                    // create new champ
                    ChampionModel newChamp = new ChampionModel(champName, champTitle, champBlurb, champAttack, champDefense, champMagic, champDifficulty);
                    // add champion to list
                    champList.Add(newChamp);
                    AllChamps.Add(newChamp);
                }
            }
        }

        private void PerformFiltering()
        {
            if (_filter == null)
            {
                _filter = "";
            }

            var lowerCaseFilter = Filter.ToLowerInvariant().Trim();

            var result = AllChamps.Where(d => d.Name.ToLowerInvariant().Contains(lowerCaseFilter)).ToList();

            var toRemove = champList.Except(result).ToList();

            foreach (var x in toRemove)
            {
                champList.Remove(x);
            }

            var resultCount = result.Count;

            for (int i = 0; i < resultCount; i++)
            {
                var resultItem = result[i];
                if (i + 1 > champList.Count || !champList[i].Equals(resultItem))
                {
                    champList.Insert(i, resultItem);
                }
            }
        }
    }
}
