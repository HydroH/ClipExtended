using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipExtended.Models.ClipboardContents
{
    public partial class TextClipboardContent: ClipboardContent
    {
        [ObservableProperty]
        private string text;
    }
}
