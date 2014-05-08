﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;

using Mymdb.Core.ViewModels;
using Xamarin.Media;
using System.Windows.Media.Imaging;
using Windows.Storage.Streams;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace Mymdb.WP.Views
{
    public partial class Movie : PhoneApplicationPage
    {
        public MovieViewModel ViewModel
        {
            get { return DataContext as MovieViewModel; }
            set { DataContext = value; }
        }

        public Movie()
        {
            ViewModel = new MovieViewModel();

            InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string id;
            NavigationContext.QueryString.TryGetValue("movieId", out id);

            await ViewModel.Init(int.Parse(id));

            await LoadImages();
        }

        private async Task LoadImages()
        {
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            if (local != null)
            {
                var dataFolder = await local.CreateFolderAsync("MovieImages", CreationCollisionOption.OpenIfExists);

                var bitmapImage = new BitmapImage();

                if (string.IsNullOrEmpty(ViewModel.ImagePath) && File.Exists(dataFolder.Path + ViewModel.ImagePath))
                {
                    //local image
                    var file = await dataFolder.OpenStreamForReadAsync(ViewModel.ImagePath);
                    bitmapImage.SetSource(file);
                }
                else
                {
                    //use image from web
                    bitmapImage.UriSource = new Uri(ViewModel.ImagePath);
                }

                bitmapImage.CreateOptions = BitmapCreateOptions.None;
                bitmapImage.ImageOpened += (s, e) =>
                {
                    MemoryStream memoryStream = new MemoryStream();

                    WriteableBitmap wb = new WriteableBitmap(bitmapImage);
                    wb.SaveJpeg(memoryStream, wb.PixelWidth, wb.PixelHeight, 0, 100);


                    Dispatcher.BeginInvoke(() => ViewModel.Image = memoryStream);
                };
            }


        }
    }
}