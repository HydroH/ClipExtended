// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Windows.ApplicationModel.DataTransfer;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ClipExtended.Models.ClipboardContents;
using ClipExtended.Controls;
using System;
using System.Drawing;
using H.NotifyIcon;
using CommunityToolkit.Mvvm.Input;
using WinUIEx;
using Microsoft.UI.Windowing;
using Windows.Graphics;
using Windows.Win32;
using Microsoft.UI.Xaml.Input;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClipExtended.Views
{
    public sealed partial class MainWindow : WinUIEx.WindowEx
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.IsAlwaysOnTop = true;
            this.IsShownInSwitchers = false;
            this.IsTitleBarVisible = false;
            WindowManager.Get(this).Backdrop = new MicaSystemBackdrop();

            var dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += OnTick;
            dispatcherTimer.Start();
            this.Activated += this.OnActivated_EventHandler;
            Clipboard.ContentChanged += this.TrackClipboardChanges_EventHandler;
        }

        [RelayCommand]
        public void ShowWindow()
        {
            this.Show();
            this.BringToFront();
        }

        [RelayCommand]
        public void ExitApplication()
        {
            TrayIcon.Dispose();
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

        private int _xWin, _yWin, _xCur, _yCur;
        private bool _isMoving = false;
        
        private void TitleBar_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            _isMoving = true;
            _xWin = AppWindow.Position.X;
            _yWin = AppWindow.Position.Y;
            PInvoke.GetCursorPos(out Point point);
            _xCur = point.X;
            _yCur = point.Y;
        }

        private void OnTick(object sender, object e)
        {
            if (_isMoving)
            {
                PInvoke.GetCursorPos(out Point point);
                AppWindow.Move(new PointInt32(_xWin + point.X - _xCur, _yWin + point.Y - _yCur));
            }
        }

        private void TitleBar_OnPointerReleased(object sender, PointerRoutedEventArgs e)
        {
            _isMoving = false;
            ((UIElement) sender).ReleasePointerCaptures();
        }
    }
}
