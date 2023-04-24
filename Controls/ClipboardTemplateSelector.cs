using ClipExtended.Models.ClipboardContents;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipExtended.Controls
{
    public class ClipboardTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Text { get; set; }
        public DataTemplate Image { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is TextClipboardContent)
            {
                return Text;
            }
            else if (item is ImageClipboardContent)
            {
                return Image;
            }
            return base.SelectTemplateCore(item, container);
        }
    }
}
