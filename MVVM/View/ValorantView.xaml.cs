﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SkyLauncherRemastered.MVVM.View
{
    public partial class ValorantView : UserControl
    {
        private JObject jsonO;
        int WeaponInt;
        private enum Weapon
        {
            ODIN,
            ARES,
            VANDAL,
            BULLDOG,
            PHANTOM,
            JUDGE,
            BUCKY,
            FRENZY,
            CLASSIC,
            GHOST,
            SHERIFF,
            SHORTY,
            OPERATOR,
            GUARDIAN,
            MARSHAL,
            SPECTRE,
            STINGER,
            MELEE,
            KNIFE
        }

        public ValorantView()
        {
            setWeapons();
            InitializeComponent();
        }

        private async Task setWeapons()
        {
            try { jsonO = await GetWeapons();
            } catch (Exception ex) { jsonO = null; }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Image clickedImage = e.Source as Image;
            string Name = clickedImage.Name;

            switch (Name)
            {
                case "ODIN": OpenWeaponPage(((int)Weapon.ODIN)); break;
                case "ARES": OpenWeaponPage((int)Weapon.ARES); break;
                case "VANDAL": OpenWeaponPage((int)Weapon.VANDAL); break;
                case "BULLDOG": OpenWeaponPage((int)Weapon.BULLDOG); break;
                case "PHANTOM": OpenWeaponPage((int)Weapon.PHANTOM); break;
                case "JUDGE": OpenWeaponPage((int)Weapon.JUDGE); break;
                case "BUCKY": OpenWeaponPage((int)Weapon.BUCKY); break;
                case "FRENZY": OpenWeaponPage((int)Weapon.FRENZY); break;
                case "CLASSIC": OpenWeaponPage((int)Weapon.CLASSIC); break;
                case "GHOST": OpenWeaponPage((int)Weapon.GHOST); break;
                case "SHERIFF": OpenWeaponPage((int)Weapon.SHERIFF); break;
                case "SHORTY": OpenWeaponPage((int)Weapon.SHORTY); break;
                case "OPERATOR": OpenWeaponPage((int)Weapon.OPERATOR); break;
                case "GUARDIAN": OpenWeaponPage((int)Weapon.GUARDIAN); break;
                case "MARSHAL": OpenWeaponPage((int)Weapon.MARSHAL); break;
                case "SPECTRE": OpenWeaponPage((int)Weapon.SPECTRE); break;
                case "STINGER": OpenWeaponPage((int)Weapon.STINGER); break;
                case "MELEE": OpenWeaponPage((int)Weapon.MELEE); break;
                case "KNIFE": OpenWeaponPage((int)Weapon.KNIFE); break;
            }
        }

        private void OpenWeaponPage(int weapon)
        {
            if (jsonO == null) return;
            try
            {
                WeaponInt = weapon;
                var weaponDefault = jsonO["data"][weapon];
                JArray weaponSkins = JArray.Parse(jsonO["data"][weapon]["skins"].ToString());
                
                //Print Weapon data
                if(weapon != 17)
                {
                    var weaponStats = jsonO["data"][weapon]["weaponStats"];
                    var weaponShopData = jsonO["data"][weapon]["shopData"];
                    JArray weaponRanges = JArray.Parse(weaponStats["damageRanges"].ToString());

                    TBWeaponName.Text = weaponDefault["displayName"] + " - " + weaponShopData["categoryText"] + " - " + weaponShopData["cost"] + "$";

                    string[] wallPenetration = weaponStats["wallPenetration"].ToString().Split(':');
                    TBWeaponStats.Text = "Fire rate: " + weaponStats["fireRate"] + "\n" +
                                            "Magazine size: " + weaponStats["magazineSize"] + "\n" +
                                            "Run speed multiplicator: " + weaponStats["runSpeedMultiplier"] + "% \n" +
                                            "Reload speed: " + weaponStats["reloadTimeSeconds"] + "s \n" +
                                            "Wall penetration: " + wallPenetration[2];

                    Grid RangeGrid = _RangeGrid;
                    RangeGrid.Children.Clear();
                    RangeGrid.ColumnDefinitions.Clear();

                    for (int a = 0; a < weaponRanges.Count; a++)
                    {
                        var range = weaponRanges[a];
                        RangeGrid.ColumnDefinitions.Add(new ColumnDefinition());

                        TextBlock txt1 = new TextBlock();
                            txt1.Text = range["rangeStartMeters"] + "m - " + range["rangeEndMeters"] + "m" + "\n";
                            txt1.FontSize = 18;
                            txt1.Foreground = Brushes.White;
                            txt1.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetColumn(txt1, a);
                        Grid.SetRow(txt1, 0);
                        RangeGrid.Children.Add(txt1);

                        TextBlock txt2 = new TextBlock();
                            txt2.Text =     "Head: " + range["headDamage"] + "\n" +
                                            "Body: " + range["bodyDamage"] + "\n" +
                                            "Leg: " + range["legDamage"];
                            txt2.FontSize = 17;
                            txt2.Foreground = Brushes.White;
                            txt2.HorizontalAlignment = HorizontalAlignment.Center;
                        Grid.SetColumn(txt2, a);
                        Grid.SetRow(txt2, 1);
                        RangeGrid.Children.Add(txt2);
                    }
                }

                //Print Weapon Skins
                Grid projectGrid = _SkinGrid;

                projectGrid.Children.Clear();
                projectGrid.ColumnDefinitions.Clear();
                projectGrid.RowDefinitions.Clear();

                for (int i = 0; i < weaponSkins.Count; i++)
                {
                    var skin = weaponSkins[i];
                    projectGrid.RowDefinitions.Add(new RowDefinition());

                    StackPanel stackPanel = new StackPanel();
                    TextBlock txt1 = new Emoji.Wpf.TextBlock();
                        txt1.Text = skin["displayName"].ToString();
                        txt1.FontSize = 15;
                        txt1.Foreground = Brushes.White;
                        txt1.TextAlignment = TextAlignment.Left;

                    stackPanel.Children.Add(txt1);

                    try
                    {
                        Image img = new Image();
                        img.Source = skin["displayIcon"].ToString().Length > 0 ?
                            new BitmapImage(new Uri(skin["displayIcon"].ToString())) : null;
                        img.Width = 250;
                        img.VerticalAlignment = VerticalAlignment.Top;
                        img.HorizontalAlignment = HorizontalAlignment.Left;
                        img.Name = "if" + i.ToString();
                        img.MouseLeftButtonDown += OpenImageLarge;

                        stackPanel.Children.Add(img);
                    } catch (UriFormatException ex)
                    {
                        Console.WriteLine("ValorantView.OpenWeaponPage:" + ex.StackTrace);
                        Grid.SetColumnSpan(stackPanel, 1);
                        Grid.SetRow(stackPanel, i);
                        projectGrid.Children.Add(stackPanel);
                        continue;
                    }

                    Grid.SetColumnSpan(stackPanel, 1);
                    Grid.SetRow(stackPanel, i);
                    projectGrid.Children.Add(stackPanel);
                }
                _InfoGrid.Visibility = Visibility.Visible;
            }
            catch (Exception ex) { Console.WriteLine("ValorantView.OpenWeaponPage:" + ex.StackTrace); }
        }

        private void OpenImageLarge(object sender, MouseButtonEventArgs e)
        {
            Image clickedImage = e.Source as Image;
            string Name = clickedImage.Name;

            String[] id = Name.Split('f');

            var weaponDefault = jsonO["data"][WeaponInt];
            JArray weaponSkins = JArray.Parse(jsonO["data"][WeaponInt]["skins"].ToString());

            var skin = weaponSkins[int.Parse(id[1])];
            JArray chromas = JArray.Parse(skin["chromas"].ToString());
            JArray levels = JArray.Parse(skin["levels"].ToString());

            Grid chromaGrid = _SkinChromaGrid;
            Grid levelGrid = _SkinLevelGrid;

            chromaGrid.Children.Clear();
            chromaGrid.ColumnDefinitions.Clear();
            chromaGrid.RowDefinitions.Clear();

            levelGrid.Children.Clear();
            levelGrid.ColumnDefinitions.Clear();
            levelGrid.RowDefinitions.Clear();

            for (int i = 0; i < chromas.Count; i++)
            {
                var chroma = chromas[i];
                chromaGrid.RowDefinitions.Add(new RowDefinition());

                StackPanel stackPanel = new StackPanel();
                TextBlock txt1 = new TextBlock();
                txt1.Text = chroma["displayName"].ToString();
                txt1.FontSize = 15;
                txt1.Foreground = Brushes.White;
                txt1.TextAlignment = TextAlignment.Left;

                stackPanel.Children.Add(txt1);

                try
                {
                    Image img = new Image();
                    img.Source = new BitmapImage(new Uri(chroma["fullRender"].ToString()));
                    img.Width = 250;
                    img.VerticalAlignment = VerticalAlignment.Top;
                    img.HorizontalAlignment = HorizontalAlignment.Left;

                    stackPanel.Children.Add(img);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ValorantView.OpenImageLarge.Chromas:" + ex.StackTrace);
                    Grid.SetColumnSpan(stackPanel, 1);
                    Grid.SetRow(stackPanel, i);
                    chromaGrid.Children.Add(stackPanel);
                    continue;
                }

                Grid.SetColumnSpan(stackPanel, 1);
                Grid.SetRow(stackPanel, i);
                chromaGrid.Children.Add(stackPanel);
            }

            for (int i = 0; i < levels.Count; i++)
            {
                var level = levels[i];
                levelGrid.RowDefinitions.Add(new RowDefinition());

                StackPanel stackPanel = new StackPanel();
                TextBlock txt1 = new TextBlock();
                txt1.Text = level["displayName"].ToString();
                txt1.FontSize = 15;
                txt1.Foreground = Brushes.White;
                txt1.TextAlignment = TextAlignment.Left;

                stackPanel.Children.Add(txt1);

                try
                {
                    String[] lvlItem = level["levelItem"].ToString().Split(':');
                    TextBlock txt2 = new TextBlock();
                    txt2.Text = level["levelItem"].ToString().Length > 0 ? lvlItem[2] : "Default";
                    txt2.FontSize = 35;
                    txt2.Foreground = Brushes.White;
                    txt2.TextAlignment = TextAlignment.Center;
                    txt2.DataContext = level["streamedVideo"].ToString();
                    txt2.MouseLeftButtonDown += OpenLevelVideo;

                    stackPanel.Children.Add(txt2);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ValorantView.OpenImageLarge.Levels:" + ex.StackTrace);
                    Grid.SetColumnSpan(stackPanel, 1);
                    Grid.SetRow(stackPanel, i);
                    levelGrid.Children.Add(stackPanel);
                    continue;
                }

                Grid.SetColumnSpan(stackPanel, 1);
                Grid.SetRow(stackPanel, i);
                levelGrid.Children.Add(stackPanel);
            }
            _InfoSkinGrid.Visibility = Visibility.Visible;
        }

        private void OpenLevelVideo(object sender, MouseButtonEventArgs e)
        {
            TextBlock clickedImage = e.Source as TextBlock;
            string UrlString = clickedImage.DataContext.ToString();
            Process.Start("explorer", UrlString);
        }

        private static async Task<JObject> GetWeapons()
        {
            JObject responseBody = null;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://valorant-api.com");
                    client.DefaultRequestHeaders.Add("User-Agent", "Anything");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("v1/weapons");
                    response.EnsureSuccessStatusCode();

                    Stream receiveStream = await response.Content.ReadAsStreamAsync();
                    StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    responseBody = (JObject)JsonConvert.DeserializeObject(readStream.ReadToEnd());
                }
            }
            catch (Exception ex) { Console.WriteLine("ValorantView.GetWeapons:" + ex.StackTrace); return null; }
            return responseBody;
        }

        private void CloseInfoWindow(object sender, RoutedEventArgs e)
        {
            _InfoGrid.Visibility = Visibility.Hidden;
        }

        private void CloseSkinInfoWindow(object sender, RoutedEventArgs e)
        {
            _InfoSkinGrid.Visibility = Visibility.Hidden;
        }
    }
}
