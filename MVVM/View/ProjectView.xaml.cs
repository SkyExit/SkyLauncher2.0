using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SkyLauncherRemastered.MVVM.View
{
    public partial class ProjectView : UserControl
    {
        public ProjectView()
        {
            InitializeComponent();
            PrintUserReposAsync();
        }

        private async Task PrintUserReposAsync()
        {
            Grid projectGrid = _ProjectGrid;

            JArray projects = await GetUserRepos("SkyExit");

            for (int i = 0; i < projects.Count; i++)
            {
                var project = projects[i];
                projectGrid.RowDefinitions.Add(new RowDefinition());

                StackPanel stackPanel = new StackPanel();
                Border txtBorder = new Border();
                    txtBorder.Height = 200;
                    txtBorder.Width = 600;
                    txtBorder.Margin = new Thickness(10);
                    txtBorder.BorderBrush = Brushes.White;
                    txtBorder.BorderThickness = new Thickness(1);
                    txtBorder.HorizontalAlignment = HorizontalAlignment.Left;

                TextBlock txt1 = new Emoji.Wpf.TextBlock();
                txt1.Text = "     " + project["name"].ToString() + "\n " +
                            " \n" +
                            " =❯ Owner: " + project["owner"]["login"] + "\n" +
                            " =❯ Url: " + project["html_url"] + "\n" +
                            " =❯ Description: " + project["description"] + "\n" +
                            " =❯ Created / Updated: " + project["created_at"] + " / " + project["updated_at"] + "\n" +
                            " =❯ Language: " + project["language"];
                    txt1.FontSize = 15;
                    txt1.Foreground = Brushes.White;
                    txt1.TextAlignment = TextAlignment.Left;
                    //txt1.HorizontalAlignment = HorizontalAlignment.Center;
                    //txt1.MouseLeftButtonDown += ButDeletOnPreviewMouseDown;
                txtBorder.Child = txt1;
                stackPanel.Children.Add(txtBorder);

                Grid.SetColumnSpan(stackPanel, 1);
                //Grid.SetColumn(stackPanel, tempC);
                Grid.SetRow(stackPanel, i);

                projectGrid.Children.Add(stackPanel);
            }
        }

        private static async Task<JArray> GetUserRepos(string user)
        {
            JArray responseBody = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.github.com");
                    client.DefaultRequestHeaders.Add("User-Agent", "Anything");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("users/" + user +  "/repos");
                    response.EnsureSuccessStatusCode();

                    Stream receiveStream = await response.Content.ReadAsStreamAsync();
                    StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    responseBody = (JArray)JsonConvert.DeserializeObject(readStream.ReadToEnd());
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return null; }
            return responseBody;
        }

        private static async Task<JArray> GetRepoStats(string user, string repo)
        {
            JArray responseBody = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://api.github.com");
                    client.DefaultRequestHeaders.Add("User-Agent", "Anything");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("users/" + user + "/repos/" + repo + "/languages");
                    response.EnsureSuccessStatusCode();

                    Stream receiveStream = await response.Content.ReadAsStreamAsync();
                    StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    responseBody = (JArray)JsonConvert.DeserializeObject(readStream.ReadToEnd());
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); return null; }
            return responseBody;
        }
    }
}
