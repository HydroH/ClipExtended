// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClipExtended
{
    public sealed partial class MainWindow : Window
    {
        public ObservableCollection<DataPackageView> Packages { get; }
            = new ObservableCollection<DataPackageView>();

        public MainWindow()
        {
            this.InitializeComponent();
            Clipboard.ContentChanged += new EventHandler<object>(this.TrackClipboardChanges_EventHandler);
            ClipboardListView.ItemsSource = Packages;
        }

        private void TrackClipboardChanges_EventHandler(object sender, object e)
        {
            DataPackageView dataPackageView = Clipboard.GetContent();
            Packages.Add(dataPackageView);
        }
    }
}
