using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mymdb.Core.Models;

namespace Mymdb.Core.Interfaces
{
    public interface IMovieService
    {
        Task<Movie> GetMovie(int id);
        Task<IEnumerable<Movie>> GetMovies(string title);
        Task<IEnumerable<Movie>> GetMoviesNowPlaying(int count = 0, int skip = 0);
        Task<IEnumerable<Movie>> GetMoviesPopular(int count = 0, int skip = 0);
        string CreateImageUrl(string imageName);
        Task<byte[]> DownloadImage(string path);
    }
}
