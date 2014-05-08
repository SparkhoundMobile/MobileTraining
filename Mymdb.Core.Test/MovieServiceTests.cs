using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mymdb.Core.Interfaces;
using Mymdb.Core.Services;
using Mymdb.Core.Helpers;

namespace Mymdb.Core.Test
{
    [TestClass]
    public class MovieServiceTests
    {
        [TestInitialize]
        public void Init()
        {
            ServiceContainer.Register<IMovieService>(() => new MovieService());
        }

        [TestMethod]
        public void GetMovie()
        {
            var movieService = ServiceContainer.Resolve<IMovieService>();
            var movie = movieService.GetMovie(550).Result;

            Assert.IsNotNull(movie);
            Assert.IsTrue(movie.Title == "Fight Club");
        }

        [TestMethod]
        public void GetMoviesNowPlaying_3()
        {
            var movieService = ServiceContainer.Resolve<IMovieService>();
            var movies = movieService.GetMoviesNowPlaying(3).Result.ToList();

            Assert.IsNotNull(movies);
            Assert.IsTrue(movies.Count == 3);
        }
    }
}
