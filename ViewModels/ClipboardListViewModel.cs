using ClipExtended.Models.ClipboardContents;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Input.Preview.Injection;

namespace ClipExtended.ViewModels
{
    public partial class ClipboardListViewModel: ObservableObject
    {
        public readonly ObservableCollection<ClipboardContent> Items = new();

        public void Add(ClipboardContent item)
        {
            Items.Insert(0, item);
        }

        public void Remove(ClipboardContent item)
        {
            Items.Remove(item);
        }
        
        public async Task AddData(DataPackageView data)
        {
            if (data.Contains(StandardDataFormats.Text))
            {
                var text = await data.GetTextAsync();
                Items.Add(new TextClipboardContent(text));
            } else if (data.Contains(StandardDataFormats.Bitmap))
            {
                var bitmap = await data.GetBitmapAsync();
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("test.png", CreationCollisionOption.GenerateUniqueName);
                {
                    using var bitmapStream = await bitmap.OpenReadAsync();
                    using var fileStream = await file.OpenAsync(FileAccessMode.ReadWrite);
                    using var inputStream = bitmapStream.AsStreamForRead();
                    using var outputStream = fileStream.AsStreamForWrite();
                    await inputStream.CopyToAsync(outputStream);
                }
                Items.Add(new ImageClipboardContent(file));
            }
        }

        public async Task UpdateClipboard(ClipboardContent item)
        {
            this.Remove(item);
            await item.SetClipboardContent();
        }
    }
}
