using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage.Streams;
using Windows.Storage;

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

        public override async Task<DataPackage> UpdatePackage(DataPackage package)
        {
            package.SetText(Text);
            return package;
        }
    }
}
