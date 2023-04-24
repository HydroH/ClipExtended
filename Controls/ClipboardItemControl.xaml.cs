// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ClipExtended.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClipExtended.Controls
{
    public sealed partial class ClipboardItemControl : UserControl
    {
        public ClipboardItem ClipboardItem
        {
            get => GetValue(ClipboardItemProperty) as ClipboardItem;
            set => SetValue(ClipboardItemProperty, value);
        }

        public static readonly DependencyProperty ClipboardItemProperty =
            DependencyProperty.Register(nameof(ClipboardItem), typeof(ClipboardItem), typeof(ClipboardItemControl), new PropertyMetadata(null));

        public event RoutedEventHandler PasteClick;

        public ClipboardItemControl()
        {
            this.InitializeComponent();
        }

        private void ClipboardItemButton_Click(object sender, RoutedEventArgs e)
        {
            this.PasteClick?.Invoke(sender, e);
        }
    }
}
