using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Streams;
using WinRT;
using static System.Net.Mime.MediaTypeNames;

namespace ClipExtended.Models.ClipboardContents
{
    public abstract class ClipboardContent: ObservableObject
    {
        public abstract Task<DataPackage> UpdatePackage(DataPackage package);
        public virtual Task Remove() => Task.CompletedTask;
    }
}
