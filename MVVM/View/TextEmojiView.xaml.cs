using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
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
            BuildTextEmojiPage(GetEmojiHastSet().Values.ToList());
        }

        public void UpdateTextEmojiList(string search)
        {
            IDictionary<string, string> dic = GetEmojiHastSet();
            List<string> list = new List<string>();

            foreach(KeyValuePair<string, string> kvp in dic)
            {
                if(kvp.Key.ToLower().Contains(search.ToLower()))
                {
                    list.Add(kvp.Value);
                }
            }
            BuildTextEmojiPage(list);
        }

        private void BuildTextEmojiPage(List<string> list)
        {
            Grid myGrid = _TextEmojiGrid;

            myGrid.Children.Clear();
            myGrid.RowDefinitions.Clear();

            int columnCount = 2-1; //Spalten
            int tempC = 0;
            int rowCount;
            int tempR = 0;

            rowCount = (list.Count / columnCount);

            //myGrid.Height = rowCount * 75;
            myGrid.Width = 720;
            myGrid.ShowGridLines = false;
            myGrid.HorizontalAlignment = HorizontalAlignment.Center;
            myGrid.VerticalAlignment = VerticalAlignment.Top;

            for (int b = 0; b <= rowCount; b++)
            {
                myGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < list.Count; i++)
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
                txt1.Text = list[i];
                txt1.FontSize = 25;
                txt1.Foreground = Brushes.Black;
                txt1.TextAlignment = TextAlignment.Center;
                txt1.HorizontalAlignment = HorizontalAlignment.Center;
                //txt1.HorizontalAlignment = HorizontalAlignment.Center;
                //txt1.MouseLeftButtonDown += ButDeletOnPreviewMouseDown;
                txtBorder.Child = txt1;
                stackPanel.Children.Add(txtBorder);

                Grid.SetColumnSpan(stackPanel, 1);
                Grid.SetColumn(stackPanel, tempC);
                Grid.SetRow(stackPanel, tempR);

                myGrid.Children.Add(stackPanel);

                tempC++;
            }
        }

        private IDictionary<string, string> GetEmojiHastSet()
        {
            IDictionary<string, string> dict = new Dictionary<string, string>();
            //dict.Add("SEARCH", "TEXTEMOJI");
            dict.Add("Lenny Face", "( ͡❛ ͜ʖ ͡❛)");
            dict.Add("shrug", "¯\\_(ツ)_/¯");
            dict.Add("tableflip", "(╯°□°）╯︵ ┻━┻");
            dict.Add("unflip", "┬─┬ ノ( ゜-゜ノ)");
            dict.Add("drink;beer", "(っ＾▿＾)۶🍸🌟🍺٩(˘◡˘ )");
            dict.Add("triggered;angry", "(ㆆ_ㆆ)");
            dict.Add("sad", "( ˘︹˘ )");
            dict.Add("fight;battle", "(ง︡'-'︠)ง");
            dict.Add("stonks;ok;gg", "(͠≖ ͜ʖ͠≖)👌");

            return dict;
        }
    }
}
