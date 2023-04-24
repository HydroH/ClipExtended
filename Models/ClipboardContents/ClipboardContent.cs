using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;

namespace ClipExtended.Models.ClipboardContents
{
    public abstract class ClipboardContent : ObservableObject
    {
        public abstract Task<DataPackage> UpdatePackage(DataPackage package);
        public virtual Task Remove() => Task.CompletedTask;
    }
}
