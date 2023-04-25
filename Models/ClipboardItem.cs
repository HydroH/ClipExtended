using ClipExtended.Models.ClipboardContents;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;

namespace ClipExtended.Models
{
    public partial class ClipboardItem : ObservableObject
    {
        [ObservableProperty]
        private Dictionary<string, ClipboardContent> contentMap;

        public ImageClipboardContent Image => ContentMap[StandardDataFormats.Bitmap] as ImageClipboardContent;
        public TextClipboardContent Text => ContentMap[StandardDataFormats.Text] as TextClipboardContent;

        private ClipboardItem() { }

        public static async Task<ClipboardItem> New(DataPackageView data)
        {
            var contents = new ClipboardItem();
            var contentMap = new Dictionary<string, ClipboardContent>();

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
                contentMap.Add(StandardDataFormats.Bitmap, new ImageClipboardContent(file));
            }

            if (data.Contains(StandardDataFormats.Html))
            {
                var text = await data.GetHtmlFormatAsync();
                contentMap.Add(StandardDataFormats.Html, new TextClipboardContent(text, StandardDataFormats.Html));
            }

            if (data.Contains(StandardDataFormats.Rtf))
            {
                var text = await data.GetRtfAsync();
                contentMap.Add(StandardDataFormats.Rtf, new TextClipboardContent(text, StandardDataFormats.Rtf));
            }

            if (data.Contains(StandardDataFormats.Text))
            {
                var text = await data.GetTextAsync();
                contentMap.Add(StandardDataFormats.Text, new TextClipboardContent(text, StandardDataFormats.Text));
            }

            if (data.Contains(StandardDataFormats.WebLink))
            {
                var uri = await data.GetWebLinkAsync();
                contentMap.Add(StandardDataFormats.WebLink, new TextClipboardContent(uri.ToString(), StandardDataFormats.WebLink));
            }

            contents.ContentMap = contentMap;
            return contents;
        }

        public async Task UpdateClipboard()
        {
            var package = new DataPackage();
            foreach (ClipboardContent content in ContentMap.Values)
            {
                package = await content.UpdatePackage(package);
            }
            Clipboard.SetContent(package);
        }

        public void Remove()
        {
            foreach (ClipboardContent content in ContentMap.Values)
            {
                _ = content.Remove();
            }
        }
    }
}
