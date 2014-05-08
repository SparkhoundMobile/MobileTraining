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
using Mymdb.Core.Models;

namespace Mymdb.Droid
{
    [Activity(Label = "Video", MainLauncher = true)]
    public class MoviesActivity : Activity
    {
        private MoviesViewModel viewModel;
        ListView moviesListView;

        protected async override void OnCreate(Bundle bundle)
        {
            viewModel = ServiceContainer.Resolve<MoviesViewModel>();

            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Movies);

            moviesListView = FindViewById<ListView>(Resource.Id.moviesListView);

            moviesListView.ItemClick += (sender, e) =>
            {
                var intent = new Intent(this, typeof(MovieActivity));
                intent.PutExtra("movieId", viewModel.MoviesList[e.Position].Id);
                StartActivity(intent);
            };

            await viewModel.ExecuteLoadMoviesCommand(true);

            moviesListView.Adapter = new MovieListAdapter(this, viewModel.MoviesList);
        }
    }
}