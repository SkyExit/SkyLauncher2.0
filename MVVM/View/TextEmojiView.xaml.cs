using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SkyLauncherRemastered.MVVM.View
{
    public partial class TextEmojiView : UserControl
    {
        public static TextEmojiView GetTextEmojiView { get; private set; }

        public TextEmojiView()
        {
            InitializeComponent();
            GetTextEmojiView = this;
            BuildTextEmojiPage(GetEmojiHastSet().Keys.ToList(), GetEmojiHastSet().Values.ToList());
        }

        public void UpdateTextEmojiList(string search)
        {
            IDictionary<string, string> dic = GetEmojiHastSet();
            List<string> keys = new List<string>();
            List<string> values = new List<string>();

            foreach (KeyValuePair<string, string> kvp in dic)
            {
                if(kvp.Key.ToLower().Contains(search.ToLower()))
                {
                    keys.Add(kvp.Key);
                    values.Add(kvp.Value);
                }
            }
            BuildTextEmojiPage(keys, values);
        }

        private void BuildTextEmojiPage(List<string> keys, List<string> values)
        {
            Grid myGrid = _TextEmojiGrid;

            myGrid.Children.Clear();
            myGrid.RowDefinitions.Clear();

            int columnCount = 2-1; //Spalten
            int tempC = 0;
            int rowCount;
            int tempR = 0;

            rowCount = (keys.Count / columnCount);

            //myGrid.Height = rowCount * 75;
            myGrid.Width = 720;
            myGrid.ShowGridLines = false;
            myGrid.HorizontalAlignment = HorizontalAlignment.Center;
            myGrid.VerticalAlignment = VerticalAlignment.Top;

            for (int b = 0; b <= rowCount; b++)
            {
                myGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < keys.Count; i++)
            {
                if (tempC > columnCount)
                {
                    tempC = 0;
                    tempR++;
                    if (tempR > rowCount)
                    {
                        return;
                    }
                }

                StackPanel stackPanel = new StackPanel();
                Border txtBorder = new Border();
                    txtBorder.Height = 40;
                    txtBorder.Width = 300;
                    txtBorder.Margin = new Thickness(10);
                    txtBorder.BorderBrush = Brushes.White;
                    txtBorder.Background = Brushes.White;
                    txtBorder.BorderThickness = new Thickness(1);
                    txtBorder.HorizontalAlignment = HorizontalAlignment.Center;
                    txtBorder.VerticalAlignment = VerticalAlignment.Center;

                TextBlock txt1 = new Emoji.Wpf.TextBlock();
                txt1.Text = values[i];
                txt1.FontSize = 25;
                txt1.Foreground = Brushes.Black;
                txt1.TextAlignment = TextAlignment.Center;
                txt1.HorizontalAlignment = HorizontalAlignment.Center;
                txt1.ToolTip = keys[i];
                txt1.MouseLeftButtonDown += CopyTextEmoji;
                txtBorder.Child = txt1;
                stackPanel.Children.Add(txtBorder);

                Grid.SetColumnSpan(stackPanel, 1);
                Grid.SetColumn(stackPanel, tempC);
                Grid.SetRow(stackPanel, tempR);

                myGrid.Children.Add(stackPanel);

                tempC++;
            }
        }

        private async void CopyTextEmoji(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = (TextBlock)sender;
            try { await copyToClipboard(textBlock.Text, true); }
            catch (System.ArgumentException ex) { return; }
        }

        private IDictionary<string, string> GetEmojiHastSet()
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            //dict.Add("SEARCH", "TEXTEMOJI");
            dict.Add("Lenny Face", "( ͡❛ ͜ʖ ͡❛)");
            dict.Add("shrug", "¯\\_(ツ)_/¯");
            dict.Add("tableflip", "(╯°□°）╯︵ ┻━┻");
            dict.Add("unflip", "┬─┬ ノ( ゜-゜ノ)");
            dict.Add("drink, beer", "(っ＾▿＾)۶🍸🌟🍺٩(˘◡˘ )");
            dict.Add("triggered, angry", "(ㆆ_ㆆ)");
            dict.Add("sad", "( ˘︹˘ )");
            dict.Add("fight, battle", "(ง︡'-'︠)ง");
            dict.Add("stonks, ok, gg", "(͠≖ ͜ʖ͠≖)👌");

            return dict;
        }

        private static async Task copyToClipboard(string emoji, bool addToHistory)
        {
            System.Windows.Clipboard.SetText(emoji);

            TextEmojiView textEmojiView = GetTextEmojiView;
            Button copyButton = textEmojiView._CopyButton;
            copyButton.Content = "📋 Copied to Clipboard";
            copyButton.Visibility = Visibility.Visible;
            await Task.Delay(1500);
            copyButton.Visibility = Visibility.Hidden;
        }
    }
}
