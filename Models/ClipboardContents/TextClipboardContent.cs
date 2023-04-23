using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace ClipExtended.Models.ClipboardContents
{
    public partial class TextClipboardContent: ClipboardContent
    {
        [ObservableProperty]
        private string text;

        public TextClipboardContent(string text)
        {
            this.text = text;
        }

        public override Task SetClipboardContent()
        {
            var package = new DataPackage();
            package.SetText(Text);
            Clipboard.SetContent(package);
            return Task.CompletedTask;
        }
    }
}
