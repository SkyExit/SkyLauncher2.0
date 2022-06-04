using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkyLauncherRemastered.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SkyLauncherRemastered.MVVM.View
{
    public partial class EmojiView : UserControl
    {
        public static EmojiView GetEmojiView { get; private set; }

        IDictionary<string, string> SmileysEmotion = new Dictionary<string, string>();
        IDictionary<string, string> PeopleBody = new Dictionary<string, string>();
        IDictionary<string, string> AnimalsNature = new Dictionary<string, string>();
        IDictionary<string, string> FoodDrink = new Dictionary<string, string>();
        IDictionary<string, string> TravelPlaces = new Dictionary<string, string>();
        IDictionary<string, string> Activities = new Dictionary<string, string>();
        IDictionary<string, string> Objects = new Dictionary<string, string>();
        IDictionary<string, string> Symbols = new Dictionary<string, string>();
        IDictionary<string, string> Flags = new Dictionary<string, string>();
        IDictionary<string, string> AllEmojis = new Dictionary<string, string>();

        public EmojiView()
        {
            InitializeComponent();
            ParseEmojiCategories();
            GetEmojiView = this;
        }

        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name.ToString())
            {
                case "HISTORY": refreshGrid(GetEmojiHistory().ToList(), new List<string>()); break;
                case "SMILEY": refreshGrid(SmileysEmotion.Values.ToList(), SmileysEmotion.Keys.ToList()); break;
                case "PEOPLE": refreshGrid(PeopleBody.Values.ToList(), PeopleBody.Keys.ToList()); break;
                case "ANIMAL": refreshGrid(AnimalsNature.Values.ToList(), AnimalsNature.Keys.ToList()); break;
                case "FOOD": refreshGrid(FoodDrink.Values.ToList(), FoodDrink.Keys.ToList()); break;
                case "TRAVEL": refreshGrid(TravelPlaces.Values.ToList(), TravelPlaces.Keys.ToList()); break;
                case "ACTIVITY": refreshGrid(Activities.Values.ToList(), Activities.Keys.ToList()); break;
                case "OBJECT": refreshGrid(Objects.Values.ToList(), Objects.Keys.ToList()); break;
                case "SYMBOL": refreshGrid(Symbols.Values.ToList(), Symbols.Keys.ToList()); break;
                case "FLAG": refreshGrid(Flags.Values.ToList(), Flags.Keys.ToList()); break;
                default: refreshGrid(SmileysEmotion.Values.ToList(), SmileysEmotion.Keys.ToList()); break;
            }
        }

        public void UpdateEmojiList(string search)
        {
            if (search.Equals("")) { refreshGrid(new List<string>(), new List<string>()); return; }

            List<string> values = new List<string>();
            List<string> keys = new List<string>();
            foreach (KeyValuePair<string, string> kvp in AllEmojis)
            {
                if (kvp.Key.ToLower().Contains(search.ToLower()))
                {
                    values.Add(kvp.Value);
                    keys.Add(kvp.Key);
                }
            }
            refreshGrid(values, keys);
        }

        private void refreshGrid(List<string> list, List<string> keys)
        {
            Grid myGrid = _EmojyGrid;

            myGrid.Children.Clear();
            myGrid.ColumnDefinitions.Clear();
            myGrid.RowDefinitions.Clear();

            int columnCount = 9; //Spalten
            int tempC = 0;
            int rowCount;
            int tempR = 0;

            rowCount = (list.Count / columnCount);

            myGrid.Width = 720;
            myGrid.ShowGridLines = false;
            myGrid.HorizontalAlignment = HorizontalAlignment.Center;
            myGrid.VerticalAlignment = VerticalAlignment.Top;

            for (int b = 0; b <= columnCount; b++)
            {
                myGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int b = 0; b <= rowCount; b++)
            {
                myGrid.RowDefinitions.Add(new RowDefinition());
            }

            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (tempC > columnCount)
                    {
                        tempC = 0;
                        tempR++;
                        if (tempR > rowCount) { return; }
                    }

                    Emoji.Wpf.TextBlock txt1 = new Emoji.Wpf.TextBlock();
                    txt1.Text = list[i];
                    txt1.FontSize = 35;
                    txt1.TextAlignment = TextAlignment.Center;
                    txt1.HorizontalAlignment = HorizontalAlignment.Center;
                    txt1.MouseLeftButtonDown += EmojiClickedCopy;
                    txt1.ToolTip = keys[i];

                    Grid.SetColumnSpan(txt1, 1);
                    Grid.SetColumn(txt1, tempC);
                    Grid.SetRow(txt1, tempR);

                    myGrid.Children.Add(txt1);

                    tempC++;
                }
            } catch (Exception ex) { Console.WriteLine(ex.Message); return; }
        }

        private async void EmojiClickedCopy(object sender, MouseButtonEventArgs e)
        {
            Emoji.Wpf.TextBlock textBlock = (Emoji.Wpf.TextBlock)sender;
            try { await copyToClipboard(textBlock.Text, true);
            } catch (System.ArgumentException ex) { return; }
        }

        private static async Task<JArray> GetEmojiJson()
        {
            JArray responseBody = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://raw.githubusercontent.com");
                    client.DefaultRequestHeaders.Add("User-Agent", "Anything");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("github/gemoji/master/db/emoji.json");
                    response.EnsureSuccessStatusCode();

                    Stream receiveStream = await response.Content.ReadAsStreamAsync();
                    StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    responseBody = (JArray)JsonConvert.DeserializeObject(readStream.ReadToEnd());
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return null; }
            return responseBody;
        }

        private async void ParseEmojiCategories()
        {
            JArray jArray = await GetEmojiJson();
            try
            {
                for(int i = 0; i < jArray.Count; i++)
                {
                    var emo = jArray[i];
                    string aliasesString = "";
                    JArray aliases = JArray.Parse(emo["aliases"].ToString());
                    foreach(var alias in aliases)
                    {
                        aliasesString = aliasesString + alias.ToString() + ",";
                    }

                    aliasesString = aliasesString.TrimEnd(',');

                    switch (emo["category"].ToString())
                    {
                        case "Smileys & Emotion": SmileysEmotion.Add(aliasesString, emo["emoji"].ToString()); break;
                        case "People & Body": PeopleBody.Add(aliasesString, emo["emoji"].ToString()); break;
                        case "Animals & Nature": AnimalsNature.Add(aliasesString, emo["emoji"].ToString()); break;
                        case "Food & Drink": FoodDrink.Add(aliasesString, emo["emoji"].ToString()); break;
                        case "Travel & Places": TravelPlaces.Add(aliasesString, emo["emoji"].ToString()); break;
                        case "Activities": Activities.Add(aliasesString, emo["emoji"].ToString()); break;
                        case "Objects": Objects.Add(aliasesString, emo["emoji"].ToString()); break;
                        case "Symbols": Symbols.Add(aliasesString, emo["emoji"].ToString()); break;
                        case "Flags": Flags.Add(aliasesString, emo["emoji"].ToString()); break;
                    }
                    AllEmojis.Add(aliasesString, emo["emoji"].ToString());
                }
            } catch (Exception ex) { Console.WriteLine(ex.Message); return; }
            refreshGrid(SmileysEmotion.Values.ToList(), SmileysEmotion.Keys.ToList());
        }

        private String[] GetEmojiHistory()
        {
            String SmileyList;
            SmileyList = Settings.Default.History.Substring(0,Settings.Default.History.Length-1);
            return SmileyList.Split(',');
        }

        public static async Task copyToClipboard(String emoji, bool addToHistory)
        {
            System.Windows.Clipboard.SetText(emoji);

            EmojiView emojiView = GetEmojiView;
            Button copyButton = emojiView._CopyButton;
            copyButton.Content = "📋 Copied to Clipboard";
            copyButton.Visibility = Visibility.Visible;
            if (addToHistory) AddToHistory(emoji);
            await Task.Delay(1500);
            copyButton.Visibility = Visibility.Hidden;
        }

        private static void AddToHistory(string emoji)
        {
            string history = Settings.Default.History;
            if (history.IndexOf(emoji) != -1) return;
            history = emoji + "," + history;
            if (history.Length > 30*9)
            {
                Settings.Default.History = history.Substring(0, 30*9);
            } else {
                Settings.Default.History = history;
            }
            Settings.Default.Save();
        }
    }
}
