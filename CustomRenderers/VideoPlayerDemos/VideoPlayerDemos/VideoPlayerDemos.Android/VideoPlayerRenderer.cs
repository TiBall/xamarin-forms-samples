using System;
using System.ComponentModel;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

using Android.Content;
using Android.Media;
using Android.Widget;

[assembly: ExportRenderer(typeof(MediaHelpers.VideoPlayer),
                          typeof(MediaHelpers.Droid.VideoPlayerRenderer))]

namespace MediaHelpers.Droid
{
    public class VideoPlayerRenderer : ViewRenderer<VideoPlayer, VideoView>
    {
        // Used to display transport controls
        MediaController mediaController;

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> args)
        {
            base.OnElementChanged(args);

            if (Control == null)
            {
                VideoView videoView = new VideoView(Context);

                // TODO: Fix aspect ratio. This is not helping!
                // Might have to override GetDesiredSize!
                videoView.LayoutParameters = new LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);

                SetNativeControl(videoView);
            }

            if (args.OldElement != null)
            {
                Control.Prepared -= OnVideoViewPrepared;
                Control.Info -= OnVideoViewInfo;
                Control.Completion -= OnVideoViewCompletion;
                Control.Error -= OnVideoViewError;

                args.OldElement.UpdateStatus -= OnUpdateStatus;

                args.OldElement.PlayRequested -= OnPlayRequested;
                args.OldElement.PauseRequested -= OnPauseRequested;
                args.OldElement.StopRequested -= OnStopRequested;
            }

            if (args.NewElement != null)
            {
                SetSource();
                SetAreTransportControlsEnabled();

                Control.Prepared += OnVideoViewPrepared;
                Control.Info += OnVideoViewInfo;
                Control.Completion += OnVideoViewCompletion;
                Control.Error += OnVideoViewError;

                args.NewElement.UpdateStatus += OnUpdateStatus;

                args.NewElement.PlayRequested += OnPlayRequested;
                args.NewElement.PauseRequested += OnPauseRequested;
                args.NewElement.StopRequested += OnStopRequested;
            }
        }

        // VideoView event handlers
        private void OnVideoViewPrepared(object sender, EventArgs args)
        {
            ((IVideoPlayerController)Element).Duration = TimeSpan.FromMilliseconds(Control.Duration);
        }

        private void OnVideoViewInfo(object sender, MediaPlayer.InfoEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine("{0}", args.What);
        }

        private void OnVideoViewCompletion(object sender, EventArgs args)
        {
            ;
        }

        private void OnVideoViewError(object sender, MediaPlayer.ErrorEventArgs args)
        {
            ;
        }





        public override SizeRequest GetDesiredSize(int widthConstraint, int heightConstraint)
        {
            SizeRequest size = base.GetDesiredSize(widthConstraint, heightConstraint);
            return size;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(sender, args);

            if (args.PropertyName == VideoPlayer.SourceProperty.PropertyName)
            {
                SetSource();
            }
            else if (args.PropertyName == VideoPlayer.AreTransportControlsEnabledProperty.PropertyName)
            {
                SetAreTransportControlsEnabled();
            }
        }

        void SetSource()
        {
            bool hasSetSource = false;

            if (Element.Source != null)
            {
                if (Element.Source is UriVideoSource)
                {
                    string uri = (Element.Source as UriVideoSource).Uri;
                    Control.SetVideoURI(Android.Net.Uri.Parse(uri));
                    hasSetSource = true;
                }
                else if (Element.Source is FileVideoSource)
                {
                    string filename = (Element.Source as FileVideoSource).File;
                    Control.SetVideoPath(filename);
                    hasSetSource = true;
                }
                else if (Element.Source is ResourceVideoSource)
                {
                    string package = Context.PackageName;
                    string path = (Element.Source as ResourceVideoSource).Path;
                    string filename = Path.GetFileNameWithoutExtension(path).ToLowerInvariant();
                    string uri = "android.resource://" + package + "/raw/" + filename;
                    Control.SetVideoURI(Android.Net.Uri.Parse(uri));
                    hasSetSource = true;
                }

                
            }

            // TODO: Is there an AutoPlay property to use instead of this logic?
            if (hasSetSource && Element.AutoPlay)
            {
                Control.Start();
            }
        }

        void SetAreTransportControlsEnabled()
        {
            if (Element.AreTransportControlsEnabled)
            {
                mediaController = new MediaController(Context);
                // SetAnchorView and SetMediaPlayer seem to have the same effect 
                //     mediaController.SetAnchorView(videoView);
                mediaController.SetMediaPlayer(Control);
                Control.SetMediaController(mediaController);
            }
            else
            {
                Control.SetMediaController(null);

                if (mediaController != null)
                {
                    mediaController.SetMediaPlayer(null);
                    mediaController = null;
                }
            }
        }

        // Event handler to update status
        void OnUpdateStatus(object sender, EventArgs args)
        {
            ((IElementController)Element).SetValueFromRenderer(VideoPlayer.PositionProperty, TimeSpan.FromMilliseconds(Control.CurrentPosition));
        }

        // Event handlers to implement methods
        void OnPlayRequested(object sender, EventArgs args)
        {
            Control.Start();
        }

        void OnPauseRequested(object sender, EventArgs args)
        {
            Control.Pause();
        }

        void OnStopRequested(object sender, EventArgs args)
        {
            Control.StopPlayback();
        }
    }
}