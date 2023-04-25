using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace ClipExtended.Models.ClipboardContents
{
    public partial class TextClipboardContent : ClipboardContent
    {
        [ObservableProperty]
        private string format;

        [ObservableProperty]
        private string text;

        public TextClipboardContent(string text, string format)
        {
            this.Text = text;
            this.Format = format;
        }

        public override Task<DataPackage> UpdatePackage(DataPackage package)
        {
            if (Format == StandardDataFormats.Html)
            {
                package.SetHtmlFormat(Text);
            }
            else if (Format == StandardDataFormats.Rtf)
            {
                package.SetRtf(Text);
            }
            else if (Format == StandardDataFormats.WebLink)
            {
                package.SetWebLink(new System.Uri(Text));
            }
            else
            {
                package.SetText(Text);
            }
            return Task.FromResult(package);
        }
    }
}
