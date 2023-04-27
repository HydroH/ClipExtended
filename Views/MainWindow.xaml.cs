// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ClipExtended.Models;
using CommunityToolkit.Mvvm.Input;
using H.NotifyIcon;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using Windows.ApplicationModel.DataTransfer;
using Windows.Graphics;
using Windows.System;
using Windows.UI.Input.Preview.Injection;
using Windows.Win32;
using Windows.Win32.Foundation;
using Windows.Win32.UI.Input.KeyboardAndMouse;
using Windows.Win32.UI.WindowsAndMessaging;
using WinUIEx;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClipExtended.Views
{
    public sealed partial class MainWindow : WinUIEx.WindowEx
    {
        private IntPtr _prevWndProc;
        private WNDPROC _wndProc;
        private InputInjector _inputInjector = InputInjector.TryCreate();

        public MainWindow()
        {
            this.InitializeComponent();
            this.IsAlwaysOnTop = true;
            this.IsShownInSwitchers = false;
            this.IsTitleBarVisible = false;
            WindowManager.Get(this).Backdrop = new MicaSystemBackdrop();

            this.Activated += this.OnActivated;
            this.Closed += this.OnClosed;
            Clipboard.ContentChanged += this.OnClipboardChange;

            _ = ViewModel.Load();
            _wndProc = WndProc;
            _prevWndProc = PInvoke.SetWindowLongPtr(new HWND(this.GetWindowHandle()), WINDOW_LONG_PTR_INDEX.GWL_WNDPROC, Marshal.GetFunctionPointerForDelegate(_wndProc));
            PInvoke.RegisterHotKey(new HWND(this.GetWindowHandle()), 1, HOT_KEY_MODIFIERS.MOD_ALT, (uint)VirtualKey.V);
        }

        private LRESULT WndProc(HWND hWnd, uint msg, WPARAM wParam, LPARAM lParam)
        {
            if (msg == PInvoke.WM_HOTKEY)
            {
                ShowWindow();
            }
            return PInvoke.CallWindowProc(Marshal.GetDelegateForFunctionPointer<WNDPROC>(_prevWndProc), new HWND(this.GetWindowHandle()), msg, wParam, lParam);
        }

        [RelayCommand]
        private void ShowWindow()
        {
            this.Show();
            this.BringToFront();
        }

        [RelayCommand]
        private void ExitApplication()
        {
            this.Close();
        }

        private void OnActivated(object sender, WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == WindowActivationState.Deactivated)
            {
                this.Hide();
            }
        }

        private async void OnClosed(object sender, WindowEventArgs args)
        {
            await ViewModel.Save();
            PInvoke.UnregisterHotKey(new HWND(this.GetWindowHandle()), 1);
            TrayIcon.Dispose();
        }

        private async void OnClipboardChange(object sender, object e)
        {
            await ViewModel.AddData(Clipboard.GetContent());
        }

        private async void ClipboardItemControl_PasteClick(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var item = ((Button)sender).DataContext;
            await ViewModel.UpdateClipboard(item as ClipboardItem);

            var ctrl = new InjectedInputKeyboardInfo
            {
                VirtualKey = (ushort)(VirtualKey.Control),
                KeyOptions = InjectedInputKeyOptions.None
            };
            var v = new InjectedInputKeyboardInfo
            {
                VirtualKey = (ushort)(VirtualKey.V),
                KeyOptions = InjectedInputKeyOptions.None
            };
            _inputInjector.InjectKeyboardInput(new[] { ctrl, v });
        }

        private int _xWin, _yWin, _xCur, _yCur;
        private bool _isMoving = false;

        private void TitleBar_OnPointerPressed(object sender, PointerRoutedEventArgs e)
        {
            _isMoving = true;
            ((UIElement)sender).CapturePointer(e.Pointer);
            _xWin = AppWindow.Position.X;
            _yWin = AppWindow.Position.Y;
            PInvoke.GetCursorPos(out Point point);
            _xCur = point.X;
            _yCur = point.Y;
        }

        private void TitleBar_OnPointerMoved(object sender, PointerRoutedEventArgs e)
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
            ((UIElement)sender).ReleasePointerCapture(e.Pointer);
        }

        private void ShowWindow_OnInvoked(KeyboardAccelerator sender, KeyboardAcceleratorInvokedEventArgs args)
        {
            ShowWindow();
        }
    }
}