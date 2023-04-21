using ClipExtended.Models.ClipboardContents;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.System;
using Windows.UI.Input.Preview.Injection;

namespace ClipExtended.ViewModels
{
    public partial class ClipboardListViewModel: ObservableObject
    {
        public readonly ObservableCollection<ClipboardContent> Items = new();
        private InputInjector inputInjector = InputInjector.TryCreate();

        public void Add(ClipboardContent item)
        {
            Items.Add(item);
        }

        public void Remove(ClipboardContent item)
        {
            Items.Remove(item);
        }
        
        public async Task AddData(DataPackageView data)
        {
            if (data.Contains(StandardDataFormats.Text))
            {
                string text = await data.GetTextAsync();
                Items.Add(new TextClipboardContent(text));
            }
        }

        public void Paste(ClipboardContent item)
        {
            this.Remove(item);
            item.SetClipboardContent();

            var ctrl = new InjectedInputKeyboardInfo();
            ctrl.VirtualKey = (ushort)(VirtualKey.Control);
            ctrl.KeyOptions = InjectedInputKeyOptions.None;

            var v = new InjectedInputKeyboardInfo();
            v.VirtualKey = (ushort)(VirtualKey.V);
            v.KeyOptions = InjectedInputKeyOptions.None;
            inputInjector.InjectKeyboardInput(new[] { ctrl, v });
        }
    }
}
