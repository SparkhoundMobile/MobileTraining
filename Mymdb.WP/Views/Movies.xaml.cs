using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Mymdb.Core.ViewModels;
using System.Windows.Data;

namespace Mymdb.WP.Views
{
    public partial class Movies : PhoneApplicationPage
    {
        private int pageNumber = 1;
        private int offsetKnob = 5;

        public MoviesViewModel ViewModel
        {
            get { return DataContext as MoviesViewModel; }
            set { DataContext = value; }
        }

        public Movies()
        {
            ViewModel = new MoviesViewModel();

            InitializeComponent();

            moviesListBox.ItemRealized += moviesListBox_ItemRealized;
            Loaded += new RoutedEventHandler(Movies_Loaded);
        }

        async void moviesListBox_ItemRealized(object sender, ItemRealizationEventArgs e)
        {
            if (e.ItemKind == LongListSelectorItemKind.Item)
            {
                await ViewModel.ExecuteLoadMoreCommand();
            }
        }

        async void Movies_Loaded(object sender, RoutedEventArgs e)
        {
            var progressIndicator = SystemTray.ProgressIndicator;
            if (progressIndicator != null)
            {
                return;
            }

            progressIndicator = new ProgressIndicator();

            SystemTray.SetProgressIndicator(this, progressIndicator);

            Binding binding = new Binding("IsBusy") { Source = ViewModel };
            BindingOperations.SetBinding(progressIndicator, ProgressIndicator.IsVisibleProperty, binding);
            BindingOperations.SetBinding(progressIndicator, ProgressIndicator.IsIndeterminateProperty, binding);

            progressIndicator.Text = "Loading new movies...";

            await ViewModel.ExecuteLoadMoviesCommand();
        }

        void MovieSelected(object sender, RoutedEventArgs e)
        {
            MovieViewModel movie = moviesListBox.SelectedItem as MovieViewModel;

            if (movie != null)
                NavigationService.Navigate(new Uri("/Views/Movie.xaml?movieId=" + movie.Id, UriKind.Relative));
        }
    }
}