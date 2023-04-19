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
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClipExtended.Controls
{
    public sealed partial class ClipboardItem : UserControl, INotifyPropertyChanged
    {
        private DataPackageView _content;
        public DataPackageView ClipboardContent
        {
            get { return _content; }
            set {
                _content = value;
                this.OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public ClipboardItem()
        {
            this.InitializeComponent();
            ClipboardContent = Clipboard.GetContent();
            this.PropertyChanged += ClipboardItem_PropertyChanged;
        }

        private async void ClipboardItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ClipboardContent))
            {
                if (ClipboardContent.Contains(StandardDataFormats.Text))
                {
                    TextBlock textBlock = new();
                    this.Content = textBlock;
                    textBlock.Text = await ClipboardContent.GetTextAsync();
                }
            }
        }
    }
}
