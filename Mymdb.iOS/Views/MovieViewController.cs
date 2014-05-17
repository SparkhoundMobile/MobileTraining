using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.Dialog;
using Mymdb.Core.Models;
using Mymdb.Core.ViewModels;
using Mymdb.Core.Helpers;
using BigTed;
using Xamarin.Media;
using System.IO;
using System.Threading.Tasks;

namespace Mymdb.iOS
{
    public partial class MovieViewController : UIViewController
    {
        private MovieViewModel viewModel;
        private Movie movie;
        private int id = 0;

        public MovieViewController(int id) : base("MovieViewController", null)
        {
            viewModel = ServiceContainer.Resolve<MovieViewModel>();
            this.id = id;
        }
        public MovieViewController(Movie movie) : base("MovieViewController", null)
        {
            this.movie = movie;
            viewModel = ServiceContainer.Resolve<MovieViewModel>();
            viewModel.Init(this.movie);
        }

        public async override void ViewDidLoad()
        {
            base.ViewDidLoad();

            if (id > 0)
            {
                BTProgressHUD.Show("Loading...");
                await viewModel.Init(id);
                BTProgressHUD.Dismiss();
            }

            btnDelete.TouchUpInside += async (sender, e) =>
            {
                await viewModel.ExecuteDeleteMovieCommand(viewModel.Id);
                NavigationController.PopToRootViewController(true);
            };

            btnSave.TouchUpInside += async (sender, e) =>
            {
                viewModel.IsFavorite = swtFavorite.On;
                await viewModel.ExecuteSaveMovieCommand();
                NavigationController.PopToRootViewController(true);
            };

            var picker = new Xamarin.Media.MediaPicker();

            btnCamera.Hidden = !picker.IsCameraAvailable;
            btnCamera.TouchUpInside += async (sender, e) =>
            {
                try
                {
                    MediaFile file = await picker.TakePhotoAsync(new StoreCameraMediaOptions());
                    processImage(file);
                }
                catch
                { }
            };

            btnPhoto.Hidden = !picker.PhotosSupported;
            btnPhoto.TouchUpInside += async (sender, e) =>
            {
                try
                {
                    MediaFile file = await picker.PickPhotoAsync();
                    processImage(file);
                }
                catch
                { }
            };

            btnImdb.TouchUpInside += (sender, e) =>
            {
                var webview = new UIWebView(new System.Drawing.RectangleF(0, 0, 640, 960));
                webview.LoadRequest(new NSUrlRequest(new NSUrl(string.Format("http://m.imdb.com/title/{0}", viewModel.ImdbId))));
                this.NavigationController.PushViewController(new UIViewController() { webview }, true);
            };

            lblTitle.Text = viewModel.Title;
            lblRuntime.Text = viewModel.Runtime.ToString();
            lblReleaseDate.Text = viewModel.ReleaseDate.ToShortDateString();
            swtFavorite.On = viewModel.IsFavorite;

            if (!string.IsNullOrEmpty(viewModel.ImdbId))
            {
                btnImdb.SetTitle(viewModel.ImdbId, UIControlState.Normal);
                btnImdb.TitleLabel.AttributedText = new NSAttributedString(viewModel.ImdbId, underlineStyle: NSUnderlineStyle.Single);
                btnImdb.Enabled = true;
            }

            setImage();
        }

        private void processImage(MediaFile file)
        {
            if (file != null)
            {
                viewModel.Image = file.GetStream();
                viewModel.ImagePath = file.Path;
                setImage();
            }
        }

        private void setImage()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine(viewModel.ImagePath);
                if (!string.IsNullOrEmpty(viewModel.ImagePath) && System.IO.File.Exists(viewModel.ImagePath))
                    imgImage.Image = new UIImage(NSData.FromFile(viewModel.ImagePath));
                else if (viewModel.Image != null && viewModel.Image.Length != 0)
                    imgImage.Image = new UIImage(NSData.FromStream(viewModel.Image));
            }
            //just don't load image
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}

