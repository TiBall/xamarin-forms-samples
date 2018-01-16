using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using MediaHelpers;

namespace VideoPlayerDemos
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public static readonly string BigBuckBunny = "https://archive.org/download/BigBuckBunny_328/BigBuckBunny_512kb.mp4";

        public static readonly string ElephantsDream = "https://archive.org/download/ElephantsDream/ed_hd_512kb.mp4";

        void OnPlayButtonClicked(object sender, EventArgs args)
        {
            videoPlayer.Play();
        }

        void OnPauseButtonClicked(object sender, EventArgs args)
        {
            videoPlayer.Pause();
        }

        void OnStopButtonClicked(object sender, EventArgs args)
        {
            videoPlayer.Stop();
        }

        async void OnLoadButtonClicked(object sender, EventArgs args)
        {
            // disable button

            string filename = await DependencyService.Get<IVideoPicker>().GetVideoFileAsync();

            System.Diagnostics.Debug.WriteLine(filename);

            FileVideoSource fileVideoSource = new FileVideoSource
            {
                File = filename
            };

            videoPlayer.Source = fileVideoSource;
        }
    }
}
