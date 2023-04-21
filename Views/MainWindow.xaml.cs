// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Windows.ApplicationModel.DataTransfer;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ClipExtended.Models.ClipboardContents;
using ClipExtended.Controls;
using System;
using H.NotifyIcon;
using CommunityToolkit.Mvvm.Input;
using WinUIEx;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClipExtended.Views
{
    public sealed partial class MainWindow : Window
    {

        public MainWindow()
        {
            this.InitializeComponent();
            this.SetIsAlwaysOnTop(true);
            this.Activated += this.OnActivated_EventHandler;
            Clipboard.ContentChanged += this.TrackClipboardChanges_EventHandler;
        }

        [RelayCommand]
        public void ShowWindow()
        {
            this.Show();
        }

        [RelayCommand]
        public void ExitApplication()
        {
            trayIcon.Dispose();
            this.Close();
        }

        private void OnActivated_EventHandler(object sender, WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == WindowActivationState.Deactivated)
            {
                this.Hide();
            }
        }

        private async void TrackClipboardChanges_EventHandler(object sender, object e)
        {
            await ViewModel.AddData(Clipboard.GetContent());
        }

        private void ClipboardItemControl_PasteClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var item = (sender as Button).DataContext;
            ViewModel.Paste(item as ClipboardContent);
        }
    }
}
