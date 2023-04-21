// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using Windows.ApplicationModel.DataTransfer;
using Microsoft.UI.Xaml;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClipExtended.Views
{
    public sealed partial class MainWindow : Window
    {

        public MainWindow()
        {
            this.InitializeComponent();
            Clipboard.ContentChanged += this.TrackClipboardChanges_EventHandler;
        }

        private async void TrackClipboardChanges_EventHandler(object sender, object e)
        {
            await ViewModel.AddData(Clipboard.GetContent());
        }
    }
}
