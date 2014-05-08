using Video.Core.Helpers;
using Video.Core.Interfaces;
using Video.Core.Services;
using Video.Core.ViewModels;

namespace Video.Core
{
    public static class ServiceRegistrar
    {
        public static void Startup()
        {
            ServiceContainer.Register<IMovieService>(() => new MovieService());

            ServiceContainer.Register<MoviesViewModel>();
            ServiceContainer.Register<MovieViewModel>();
        }
    }
}
