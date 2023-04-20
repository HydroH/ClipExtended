using ClipExtended.Models.ClipboardContents;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipExtended.ViewModels
{
    public partial class ClipboardListViewModel: ObservableObject
    {
        public readonly ObservableCollection<ClipboardContent> Items = new();

        public void Add(ClipboardContent item)
        {
            Items.Add(item);
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }
    }
}
