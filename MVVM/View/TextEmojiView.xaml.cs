using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// Interaktionslogik für TextEmojiView.xaml
    /// </summary>
    public partial class TextEmojiView : UserControl
    {
        public static TextEmojiView GetTextEmojiView { get; private set; }

        public TextEmojiView()
        {
            InitializeComponent();

            GetTextEmojiView = this;

            BuildTextEmojiPage(GetTextEmojis());
        }

        public void UpdateTextEmojiList(string search)
        {
            Console.WriteLine(search);
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

        private List<string> GetTextEmojis()
        {
            List<string> list = new List<string>();

            list.Add("( ͡❛ ͜ʖ ͡❛)");
            list.Add("(⊙.⊙(☉̃ₒ☉)⊙.⊙)");
            list.Add("(╯°□°）╯︵ ┻━┻");
            list.Add("(ง︡'-'︠)ง");

            return list;
        }
    }
}
