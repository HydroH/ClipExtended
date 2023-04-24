using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Streams;

namespace ClipExtended.Models.ClipboardContents
{
    public partial class ImageClipboardContent : ClipboardContent
    {
        [ObservableProperty]
        private String path;

        public ImageClipboardContent(StorageFile file)
        {
            this.path = file.Path;
        }

        public override async Task<DataPackage> UpdatePackage(DataPackage package)
        {
            try
            {
                var file = await StorageFile.GetFileFromPathAsync(Path);
                package.SetBitmap(RandomAccessStreamReference.CreateFromFile(file));
            }
            catch { }
            return package;
        }

        public override async Task Remove()
        {
            try
            {
                var file = await StorageFile.GetFileFromPathAsync(Path);
                await file.DeleteAsync();
            }
            catch { }
        }
    }
}
