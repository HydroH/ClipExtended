using ClipExtended.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace ClipExtended.ViewModels
{
    public partial class ClipboardListViewModel : ObservableObject
    {
        public readonly ObservableCollection<ClipboardItem> Items = new();

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
    }
}
