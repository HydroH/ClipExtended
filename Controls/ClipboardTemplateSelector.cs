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
        public DataTemplate Image { get; set; }
        public DataTemplate Text { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            var contents = item as ClipboardContents;
            if (contents == null)
            {
                return base.SelectTemplateCore(item, container);
            }
            if (contents.Image != null)
            {
                return Image;
            }
            if (contents.Text != null) 
            {
                return Text;
            }
            return base.SelectTemplateCore(item, container);
        }
    }
}
