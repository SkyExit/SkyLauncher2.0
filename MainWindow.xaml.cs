﻿using SkyLauncherRemastered.MVVM.View;
using System;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SkyLauncherRemastered
{
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }

        private String version = "v1.9.1";
        private String vString;
        private bool upToDate = false;

        public MainWindow()
        {
            InitializeComponent();

            VersionTextBlock.Text = version;
            CheckForUpdates();
            if(!upToDate)
            {
                LauncherName.Text = "Update available!";
                LauncherName.MouseLeftButtonDown += OpenGitHubLink;
                LauncherName.Cursor = Cursors.Hand;
            }
        }

        private void OpenGitHubLink(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer", "https://github.com/SkyExit/SkyLauncher2.0/releases");
        }

        private void CheckForUpdates()
        {
            try
            {
                WebClient webClient = new WebClient();
                String onlineString = webClient.DownloadString("https://raw.githubusercontent.com/SkyExit/SkyLauncher2.0/master/MainWindow.xaml.cs");
                String searchS = "private String version =";
                String[] sString = onlineString.Substring(onlineString.IndexOf(searchS) + searchS.Length).Split(';');
                vString = sString[0].Replace('"', ' ').Trim();
                upToDate = version.Equals(vString) ? true : false;
            } catch (Exception ex) { Console.WriteLine(ex.StackTrace); }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            try { this.DragMove(); } catch (Exception ex) { }
        }

        private void ShutdownLauncher_Click(object sender, RoutedEventArgs e) { Environment.Exit(0); }

        private void MinimizeLauncher_Click(object sender, RoutedEventArgs e) { WindowState = WindowState.Minimized; }

        private void _SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            string[] ModelName = _ContentControl.Content.ToString().Split('.');
            TextBox tb = e.OriginalSource as TextBox;

            switch (ModelName[3])
            {
                case "TextEmojiViewModel": TextEmojiView.GetTextEmojiView.UpdateTextEmojiList(tb.Text); break;
                case "EmojiViewModel": EmojiView.GetEmojiView.UpdateEmojiList(tb.Text); break;
            }
        }
    }
}
