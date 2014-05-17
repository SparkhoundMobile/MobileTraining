using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Mymdb.Core.Helpers;
using Mymdb.Core.Interfaces;
using Mymdb.Core.Models;

namespace Mymdb.Core.ViewModels
{
    public class MoviesViewModel : ViewModelBase
    {
        private IMovieService movieService;

        public MoviesViewModel()
        {
            movieService = ServiceContainer.Resolve<IMovieService>();
            NeedsUpdate = true;
        }

        /// <summary>
        /// Gets or sets if an update is needed
        /// </summary>
        public bool NeedsUpdate { get; set; }
        /// <summary>
        /// Gets or sets if we have loaded alert
        /// </summary>
        public bool LoadedAlert { get; set; }

        private ObservableCollection<MovieViewModel> movies = new ObservableCollection<MovieViewModel>();
        public ObservableCollection<MovieViewModel> Movies
        {
            get { return movies; }
            set { movies = value; OnPropertyChanged("Movies"); }
        }

        private List<Movie> moviesList = new List<Movie>();
        public List<Movie> MoviesList
        {
            get { return moviesList; }
            set { moviesList = value; OnPropertyChanged("MoviesList"); }
        }

        private RelayCommand<bool> loadMoviesCommand;
        public ICommand LoadMoviesCommand
        {
            get { return loadMoviesCommand ?? (loadMoviesCommand = new RelayCommand<bool>(async (loadImages) => await ExecuteLoadMoviesCommand(loadImages))); }
        }
        public async Task ExecuteLoadMoviesCommand(bool loadImages = false)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            Movies.Clear();
            NeedsUpdate = false;
            try
            {
                var movies = await movieService.GetMoviesNowPlaying(5);
                MovieViewModel vm;

                foreach (var movie in movies)
                {
                    vm = new MovieViewModel();
                    vm.Init(movie);

                    if (loadImages && !string.IsNullOrEmpty(movie.ImageUrl))
                    {
                        var url = movieService.CreateImageUrl(movie.ImageUrl);
                        movie.Image = await movieService.DownloadImage(url);
                    }

                    Movies.Add(vm);
                }
                MoviesList = movies.ToList();
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Unable to load movies");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private RelayCommand loadMoreCommand;
        public ICommand LoadMoreCommand
        {
            get { return loadMoreCommand ?? (loadMoreCommand = new RelayCommand(async () => await ExecuteLoadMoreCommand())); }
        }
        public async Task ExecuteLoadMoreCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            MoviesList.Clear();
            NeedsUpdate = false;
            try
            {
                var movies = await movieService.GetMoviesNowPlaying(5, Movies.Count);
                MovieViewModel vm;

                foreach (var movie in movies)
                {
                    vm = new MovieViewModel();

                    vm.Init(movie);

                    vm.ImagePath = movieService.CreateImageUrl(movie.ImageUrl);
                    Movies.Add(vm);
                }
                MoviesList = movies.ToList();
            }
            catch (Exception exception)
            {
                Debug.WriteLine("Unable to load more movies");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private RelayCommand<string> getImageCommand;
        public ICommand GetImageCommand
        {
            get { return getImageCommand ?? (getImageCommand = new RelayCommand<string>((item) => ExecuteGetImageCommand(item))); }
        }
        public string ExecuteGetImageCommand(string path)
        {
            if (IsBusy)
                return string.Empty;

            IsBusy = true;
            try
            {
                return movieService.CreateImageUrl(path);
            }
            catch (Exception)
            {
                Debug.WriteLine("Unable to create image url");
            }
            finally
            {
                IsBusy = false;
            }
            return string.Empty;
        }

        private RelayCommand<string> downloadImageCommand;
        public ICommand DownloadImageCommand
        {
            get { return downloadImageCommand ?? (downloadImageCommand = new RelayCommand<string>((item) => ExecuteDownloadImageCommand(item))); }
        }
        public Task<byte[]> ExecuteDownloadImageCommand(string path)
        {
            if (IsBusy)
                return null;

            IsBusy = true;
            try
            {
                var url = movieService.CreateImageUrl(path);
                return movieService.DownloadImage(url);
            }
            catch (Exception)
            {
                Debug.WriteLine("Unable to download image");
            }
            finally
            {
                IsBusy = false;
            }
            return null;
        }
    }
}
