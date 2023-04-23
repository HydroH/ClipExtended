using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using Windows.ApplicationModel.DataTransfer;
using System.Drawing;
using Windows.Storage;
using Windows.Storage.Streams;

namespace ClipExtended.Models.ClipboardContents
{
    public partial class ImageClipboardContent: ClipboardContent
    {
        [ObservableProperty]
        private String path;

        public ImageClipboardContent(StorageFile file)
        {
            this.path = file.Path;
        }

        public override async Task SetClipboardContent()
        {
            var package = new DataPackage();
            try
            {
                var file = await StorageFile.GetFileFromPathAsync(Path);
                package.SetBitmap(RandomAccessStreamReference.CreateFromFile(file));
                Clipboard.SetContent(package);
            }
            catch { }
        }
    }
}
