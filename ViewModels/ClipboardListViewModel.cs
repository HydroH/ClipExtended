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
        public readonly ObservableCollection<ClipboardContents> Items = new();

        public void Add(ClipboardContents item)
        {
            if (Items.Contains(item))
            {
                Items.Move(Items.IndexOf(item), 0);
            }
            else
            {
                Items.Insert(0, item);
            }
        }

        public void Remove(ClipboardContents item)
        {
            Items.Remove(item);
            item.Remove();
        }
        
        public async Task AddData(DataPackageView data)
        {
            var contents = await ClipboardContents.New(data);
            this.Add(contents);
        }

        public async Task UpdateClipboard(ClipboardContents item)
        {
            await item.UpdateClipboard();
        }
    }
}
