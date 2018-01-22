using System;
using System.IO;
using System.Threading.Tasks;

using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

using Xamarin.Forms;

using MediaHelpers.UWP;

[assembly: Dependency(typeof(VideoPicker))]

namespace MediaHelpers.UWP
{
    public class VideoPicker : IVideoPicker
    {
        public async Task<string> GetVideoFileAsync()
        {
            // Create and initialize the FileOpenPicker
            FileOpenPicker openPicker = new FileOpenPicker
            {
                ViewMode = PickerViewMode.Thumbnail,
                SuggestedStartLocation = PickerLocationId.PicturesLibrary           // .VideosLibrary
            };

            openPicker.FileTypeFilter.Add(".wmv");
            openPicker.FileTypeFilter.Add(".mp4");                  // TODO !!!
    //        openPicker.FileTypeFilter.Add(".jpeg");
   //         openPicker.FileTypeFilter.Add(".png");

            // Get a file and return a Stream 
            StorageFile storageFile = await openPicker.PickSingleFileAsync();

            if (storageFile == null)
            {
                return null;
            }

            return storageFile.Path;
                                                    // IRandomAccessStreamWithContentType raStream = await storageFile.OpenReadAsync();
                                          //  raStream.AsStreamForRead();
        }
    }
}
