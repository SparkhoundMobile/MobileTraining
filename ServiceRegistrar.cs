using System;
using System.IO;
using Mymdb.Core.Helpers;
using Mymdb.Core.Interfaces;
using Mymdb.Core.Services;
using Mymdb.Core.ViewModels;

namespace Mymdb.Core
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
