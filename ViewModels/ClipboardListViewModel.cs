using ClipExtended.Models;
using ClipExtended.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.WinUI.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace ClipExtended.ViewModels
{
    public partial class ClipboardListViewModel : ObservableObject
    {
        public readonly ObservableCollection<ClipboardItem> Items = new();

        public ApplicationDataStorageHelper storageHelper = ApplicationDataStorageHelper.GetCurrent(new JsonObjectSerializer());

        public void Add(ClipboardItem item)
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

        public void Remove(ClipboardItem item)
        {
            Items.Remove(item);
            item.Remove();
        }

        public async Task AddData(DataPackageView data)
        {
            var contents = await ClipboardItem.New(data);
            this.Add(contents);
        }

        public async Task UpdateClipboard(ClipboardItem item)
        {
            await item.UpdateClipboard();
        }

        public async Task Save()
        {
            var items = new List<Dictionary<string, string>>();
            foreach (var item in Items)
            {
                items.Add(item.ToMap());
            }
            await storageHelper.CreateFileAsync("clipboard.json", items);
        }

        public async Task Load()
        {
            try
            {
                var items = await storageHelper.ReadFileAsync<List<Dictionary<string, string>>>("clipboard.json");
                Items.Clear();
                foreach (var item in items)
                {
                    Items.Add(ClipboardItem.New(item));
                }
            }
            catch { }
        }
    }
}
