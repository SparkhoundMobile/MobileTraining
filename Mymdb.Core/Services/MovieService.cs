using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Mymdb.Core.Models;
using Newtonsoft.Json;

namespace Mymdb.Core.Services
{
    public class MovieService : Interfaces.IMovieService
    {
        private const string apiKey = "<Enter your api key here>";
        private const string basePath = "https://api.themoviedb.org/3/";

        public string CreateImageUrl(string imageName)
        {
            return "https://image.tmdb.org/t/p/w185" + imageName;
        }

        public async System.Threading.Tasks.Task<byte[]> DownloadImage(string path)
        {
            using (var client = new HttpClient())
            {
                return await client.GetByteArrayAsync(path);
            }
        }

        public async System.Threading.Tasks.Task<Models.Movie> GetMovie(int id)
        {
            string url = string.Format("{0}movie/{1}?api_key={2}", basePath, id, apiKey);

            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<Movie>(result);
            }
        }

        public async System.Threading.Tasks.Task<IEnumerable<Models.Movie>> GetMovies(string title)
        {
            string url = string.Format("{0}search/movie?api_key={1}&query={2}", basePath, apiKey, title);

            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<MovieList>(result).Movies;
            }
        }

        public async System.Threading.Tasks.Task<IEnumerable<Movie>> GetMoviesNowPlaying(int count = 10, int skip = 0)
        {
            string url = string.Format("{0}movie/now_playing?api_key={1}", basePath, apiKey);

            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<MovieList>(result).Movies.OrderBy(m => m.Title).Skip(skip).Take(count).ToList();
            }
        }

        public async System.Threading.Tasks.Task<IEnumerable<Movie>> GetMoviesPopular(int count = 10, int skip = 0)
        {
            string url = string.Format("{0}movie/popular?api_key={1}", basePath, apiKey);

            using (var client = new HttpClient())
            {
                var result = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<MovieList>(result).Movies.OrderBy(m => m.Title).Skip(skip).Take(count).ToList();
            }
        }
    }
}
