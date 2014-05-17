using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mymdb.Core.ViewModels;
using Mymdb.Core.Helpers;
using Android.Webkit;
using Xamarin.Media;

namespace Mymdb.Droid
{
    [Activity(Label = "Movie Details")]
    public class MovieActivity : Activity
    {
        private MovieViewModel viewModel;
        private TextView imdbView;
        private Button btnDelete;
        private Button btnSave;
        private Button btnPhoto;
        private Button btnCamera;
        private ImageView imgImage;
        private ToggleButton tglFavorite;

        protected async override void OnCreate(Bundle bundle)
        {
            viewModel = ServiceContainer.Resolve<MovieViewModel>();

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Movie);

            int id = Intent.GetIntExtra("movieId", 0);

            if (id > 0)
            {
                AndroidHUD.AndHUD.Shared.Show(this, "Loading...");
                await viewModel.Init(id);
                AndroidHUD.AndHUD.Shared.Dismiss(this);
            }

            imgImage = FindViewById<ImageView>(Resource.Id.imageView1);

            imdbView = FindViewById<TextView>(Resource.Id.textIMDB);
            imdbView.Text = viewModel.ImdbId;
            imdbView.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(WebviewActivity));
                intent.PutExtra("imdbId", viewModel.ImdbId);
                StartActivity(intent);
            };

            btnDelete = FindViewById<Button>(Resource.Id.btnDelete);
            btnDelete.Click += async (sender, e) =>
            {
                await viewModel.ExecuteDeleteMovieCommand(viewModel.Id);
                //Navigate back?
            };

            btnSave = FindViewById<Button>(Resource.Id.btnSave);
            btnSave.Click += async (sender, e) =>
            {
                tglFavorite = FindViewById<ToggleButton>(Resource.Id.toggleFavorite);
                viewModel.IsFavorite = tglFavorite.Checked;
                await viewModel.ExecuteSaveMovieCommand();
                //Navigate back?
            };

            var picker = new Xamarin.Media.MediaPicker(this);

            btnCamera = FindViewById<Button>(Resource.Id.btnCamera);

            btnPhoto = FindViewById<Button>(Resource.Id.btnPhoto);

            FindViewById<TextView>(Resource.Id.textTitle).Text = viewModel.Title;
            FindViewById<TextView>(Resource.Id.textRuntime).Text = viewModel.Runtime.ToString();
            FindViewById<TextView>(Resource.Id.textReleaseDate).Text = viewModel.ReleaseDate.ToShortDateString();
            FindViewById<ToggleButton>(Resource.Id.toggleFavorite).Checked = viewModel.IsFavorite;
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
                {
                    var path = Android.Net.Uri.Parse(viewModel.ImagePath);
                    imgImage.SetImageURI(path);
                }
                else if (viewModel.Image != null && viewModel.Image.Length != 0)
                {
                    var array = viewModel.Image.ConvertToByte();
                    var bmp = Android.Graphics.BitmapFactory.DecodeByteArray(array, 0, array.Length);
                    imgImage.SetImageBitmap(bmp);
                }
            }
            //just don't load image
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}