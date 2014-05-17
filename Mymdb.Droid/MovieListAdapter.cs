using System.Collections.Generic;

using Android.App;
using Android.Views;
using Android.Widget;
using Mymdb.Core.Models;
using Mymdb.Core.ViewModels;
using Mymdb.Core.Helpers;


namespace Mymdb.Droid
{
    public class MovieListAdapter : BaseAdapter<Movie>
    {
        List<Movie> movies;
        Activity context;
        private MoviesViewModel viewModel;

        public MovieListAdapter(Activity context, List<Movie> movies)
            : base()
        {
            this.context = context;
            this.movies = movies;

            viewModel = ServiceContainer.Resolve<MoviesViewModel>();
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Movie this[int index]
        {
            get
            {
                return movies[index];
            }
        }
        public override int Count
        {
            get
            {
                return movies.Count;
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var movie = movies[position];
            View view = convertView;
            if (view == null)
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.ActivityListItem, null);
            view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = movie.Title;

            if (movie.Image != null)
            {
                var bmp = Android.Graphics.BitmapFactory.DecodeByteArray(movie.Image, 0, movie.Image.Length);
                view.FindViewById<ImageView>(Android.Resource.Id.Icon).SetImageBitmap(bmp);
            }

            return view;
        }
    }
}