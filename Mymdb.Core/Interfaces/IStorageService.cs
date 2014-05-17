using System.Threading.Tasks;
using Mymdb.Core.Models;

namespace Mymdb.Core.Interfaces
{
    public interface IStorageService
    {
        Task<Movie> GetMovie(int id);
        Task<Movie> SaveMovie(Movie movie);
        Task<int> DeleteMovie(int id);
    }
}
