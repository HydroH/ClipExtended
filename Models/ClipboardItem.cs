using ClipExtended.Models.ClipboardContents;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;

namespace ClipExtended.Models
{
    public partial class ClipboardItem : ObservableObject
    {
        // [ObservableProperty]
        // private HtmlClipboardContent html;

        // [ObservableProperty]
        // private RtfClipboardContent rtf;

        [ObservableProperty]
        private ImageClipboardContent image;

        [ObservableProperty]
        private TextClipboardContent text;

        // [ObservableProperty]
        // private FileDropListClipboardContent fileDropList;

        private ClipboardItem() { }

        public static async Task<ClipboardItem> New(DataPackageView data)
        {
            var contents = new ClipboardItem();

            if (data.Contains(StandardDataFormats.Bitmap))
            {
                var stream = await data.GetBitmapAsync();
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("test.png", CreationCollisionOption.GenerateUniqueName);
                {
                    using var objectStream = await stream.OpenReadAsync();
                    using var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite);
                    using var inputStream = objectStream.AsStreamForRead();
                    using var outputStream = fileStream.AsStreamForWrite();
                    await inputStream.CopyToAsync(outputStream);
                }
                contents.Image = new ImageClipboardContent(file);
            }
            else if (data.Contains(StandardDataFormats.Text))
            {
                var text = await data.GetTextAsync();
                contents.Text = new TextClipboardContent(text);
            }

            return contents;
        }

        public async Task UpdateClipboard()
        {
            var package = new DataPackage();
            package = await Image.UpdatePackage(package);
            package = await Text.UpdatePackage(package);
            Clipboard.SetContent(package);
        }

        public void Remove()
        {
            _ = Image.Remove();
            _ = Text.Remove();
        }
    }
}
