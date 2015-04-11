using System;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace SSWindows.Common
{
    public class FileHelper
    {

        private static FileHelper _mInstance;
        public static FileHelper Instance
        {
            get { return _mInstance ?? (_mInstance = new FileHelper()); }
        }

        public async Task<string> CopyAndAssignFilePathAsync(StorageFile file)
        {
            var copiedFile = await file.CopyAsync(
                ApplicationData.Current.LocalFolder,
                file.DisplayName,
                NameCollisionOption.ReplaceExisting);

            return copiedFile.Path;
        }

        public async Task<IRandomAccessStream> GetFileStreamFromFileAsync(string path)
        {
            var fileStream = await (await StorageFile.GetFileFromPathAsync(path)).OpenAsync(FileAccessMode.Read);
            return fileStream;
        }

        public async Task DeleteCopyAsync(string path)
        {
            try
            {
                var file = await StorageFile.GetFileFromPathAsync(path);
                await file.DeleteAsync();
            }
            catch
            {
                // we didn't find it... we don't care
            }
        }
    }
}
