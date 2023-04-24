// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

using ClipExtended.Models.ClipboardContents;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace ClipExtended.Controls
{
    public sealed partial class ClipboardItemControl : UserControl
    {
        public ClipboardContents ClipboardContents
        { 
            get => GetValue(ClipboardContentsProperty) as ClipboardContents;
            set => SetValue(ClipboardContentsProperty, value);
        }

        public static readonly DependencyProperty ClipboardContentsProperty =
            DependencyProperty.Register(nameof(ClipboardContents), typeof(ClipboardContents), typeof(ClipboardItemControl), new PropertyMetadata(null));

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
