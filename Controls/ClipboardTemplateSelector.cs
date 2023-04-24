using ClipExtended.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ClipExtended.Controls
{
    public class ClipboardTemplateSelector : DataTemplateSelector
    {
        public DataTemplate Image { get; set; }
        public DataTemplate Text { get; set; }

        protected override DataTemplate SelectTemplateCore(object item, DependencyObject container)
        {
            if (item is not ClipboardItem contents)
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
