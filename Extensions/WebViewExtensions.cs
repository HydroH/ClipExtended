using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace ClipExtended.Extensions
{
    public static class WebViewExtensions
    {
        public static readonly DependencyProperty ContentProperty = DependencyProperty.RegisterAttached("Content", typeof(string), typeof(WebViewExtensions), new PropertyMetadata(string.Empty, OnContentChanged));

        public static string GetContent(DependencyObject obj)
        {
            return (string)obj.GetValue(ContentProperty);
        }

        public static void SetContent(DependencyObject obj, string value)
        {
            obj.SetValue(ContentProperty, value);
        }

        private static void OnContentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            WebView2 wv = d as WebView2;
            var content = e.NewValue as string;

            if (string.IsNullOrEmpty(content))
            {
                return;
            }
            wv?.NavigateToString(content);
        }
    }
}
