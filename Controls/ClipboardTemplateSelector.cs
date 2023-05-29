using ClipExtended.Models;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.ApplicationModel.DataTransfer;

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
            if (contents.ContentMap.ContainsKey(StandardDataFormats.Bitmap))
            {
                return Image;
            }
            return Text;
        }
    }
}
