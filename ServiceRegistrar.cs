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
            SQLite.Net.SQLiteConnection connection = null;
            SQLite.Net.Interop.ISQLitePlatform platform = null;
            string dbLocation = "videoDB.db3";

#if XAMARIN_ANDROID
            var library = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            dbLocation = Path.Combine(library, dbLocation);
            platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
#elif __IOS__
            var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var library = Path.Combine(docsPath, "../Library/");
            dbLocation = Path.Combine(library, dbLocation);
            platform = new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
#elif WINDOWS_PHONE
            platform = new SQLite.Net.Platform.WindowsPhone8.SQLitePlatformWP8();
            dbLocation = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, dbLocation);

#elif WINDOWS_APP
            platform = new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT();
            dbLocation = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, dbLocation);
#else //desktop
            platform = new SQLite.Net.Platform.Win32.SQLitePlatformWin32();
#endif
            connection = new SQLite.Net.SQLiteConnection(platform, dbLocation);

            ServiceContainer.Register<IStorageService>(() => new StorageService(connection));
            ServiceContainer.Register<IMovieService>(() => new MovieService());

            ServiceContainer.Register<MoviesViewModel>();
            ServiceContainer.Register<MovieViewModel>();
        }
    }
}
