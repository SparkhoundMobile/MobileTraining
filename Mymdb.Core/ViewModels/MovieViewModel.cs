using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Mymdb.Core.Helpers;
using Mymdb.Core.Interfaces;
using Mymdb.Core.Models;
using Mymdb.Core.Services;

namespace Mymdb.Core.ViewModels
{
    public class MovieViewModel : ViewModelBase
    {
        private IMovieService movieService;
        private Movie currentMovie;

        public MovieViewModel()
        {
            movieService = ServiceContainer.Resolve<IMovieService>();
        }

        public MovieViewModel(IMovieService movieService)
        {
            this.movieService = movieService;
            this.Image = new System.IO.MemoryStream();
        }

        public async Task Init(int id)
        {
            if (id >= 0)
            {
                currentMovie = await movieService.GetMovie(id);
            }
            else
            {
                currentMovie = null;
            }

            Init();
        }

        public void Init(Movie movie)
        {
            currentMovie = movie;
            Init();
        }

        public void Init()
        {
            if (currentMovie == null)
            {
                Title = string.Empty;
                ImdbId = string.Empty;
                IsFavorite = false;
                Runtime = 0;
                Id = -1;
                ImagePath = string.Empty;
                return;
            }
            else
            {
                Title = currentMovie.Title;
                ImdbId = currentMovie.ImdbId;
                IsFavorite = currentMovie.IsFavorite;
                Runtime = currentMovie.Runtime;
                ReleaseDate = currentMovie.ReleaseDate;
                Id = currentMovie.Id;
                ImagePath = movieService.CreateImageUrl(currentMovie.ImageUrl);
                if (currentMovie.Image != null)
                    Image = currentMovie.Image.ConvertToStream();
            }
        }

        private int id;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged("Title"); }
        }

        private string imdbId = string.Empty;
        public string ImdbId
        {
            get { return imdbId; }
            set { imdbId = value; OnPropertyChanged("ImdbId"); }
        }

        private int runtime = 0;
        public int Runtime
        {
            get { return runtime; }
            set { runtime = value; OnPropertyChanged("Runtime"); }
        }

        private DateTime releaseDate;
        public DateTime ReleaseDate
        {
            get { return releaseDate; }
            set { releaseDate = value; }
        }

        private bool isFavorite = false;
        public bool IsFavorite
        {
            get { return isFavorite; }
            set { isFavorite = value; OnPropertyChanged("IsFavorite"); }
        }

        private string imagePath = string.Empty;
        public string ImagePath
        {
            get { return imagePath; }
            set { imagePath = value; OnPropertyChanged("ImagePath"); OnPropertyChanged("Image"); }
        }

        private System.IO.Stream image;
        public System.IO.Stream Image
        {
            get { return image; }
            set { image = value; OnPropertyChanged("Image"); OnPropertyChanged("ImagePath"); }
        }
    }
}
