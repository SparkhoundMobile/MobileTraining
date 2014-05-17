﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mymdb.Core.Interfaces;
using Mymdb.Core.Services;
using Mymdb.Core.Helpers;
using Mymdb.Core.Models;
using SQLite.Net;
using SQLite.Net.Platform.Win32;

namespace Mymdb.Test
{
    [TestClass]
    public class StorageServiceTests
    {
        SQLiteConnection connection = null;
        const int MOVIE_ID = 12345;
        const string DB_LOCATION = "videoDB.db3";

        [TestInitialize]
        public void Init()
        {
            connection = new SQLiteConnection(new SQLitePlatformWin32(), DB_LOCATION);
            ServiceContainer.Register<IStorageService>(() => new StorageService(connection));
        }

        [TestCleanup]
        public void CleanUp()
        {
            if (connection != null)
                connection.Close();
            ServiceContainer.Clear();
            GC.Collect();

            System.IO.File.Delete(DB_LOCATION);
        }

        [TestMethod]
        public void SaveAndDeleteMovie()
        {
            var storageService = ServiceContainer.Resolve<IStorageService>();

            storageService.SaveMovie(new Movie { Id = MOVIE_ID });
            var result = storageService.GetMovie(MOVIE_ID).Result;
            var count = storageService.DeleteMovie(MOVIE_ID).Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(MOVIE_ID == result.Id);
            Assert.IsTrue(count == 1);
        }
    }
}