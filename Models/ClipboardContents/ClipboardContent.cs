using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClipExtended.Models.ClipboardContents
{
    public abstract class ClipboardContent: ObservableObject
    {
        public abstract void SetClipboardContent();
    }
}
