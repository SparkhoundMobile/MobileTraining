using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.Dialog;
using MonoTouch.UIKit;
using Mymdb.Core.ViewModels;
using Mymdb.Core.Helpers;
using BigTed;

namespace Mymdb.iOS.Views
{
    public partial class MoviesViewController : DialogViewController
    {
        private Section section;
        private LoadMoreElement loadMore;

        private MoviesViewModel viewModel;
        public MoviesViewController() : base(UITableViewStyle.Plain, null)
        {

        }

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();
            this.NavigationController.NavigationBar.Translucent = false;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            viewModel = ServiceContainer.Resolve<MoviesViewModel>();

            viewModel.IsBusyChanged = (busy) =>
            {
                if (busy)
                    BTProgressHUD.Show("Loading...");
                else
                    BTProgressHUD.Dismiss();
            };
        }

        public async override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (viewModel.NeedsUpdate)
            {
                await viewModel.ExecuteLoadMoviesCommand();
                Root = new RootElement("Movies");
                Root.Add(section = new Section());
                loadMovies();
            }
        }

        private void loadMovies()
        {
            section.Remove(loadMore);
            section.AddAll(viewModel.MoviesList.Select(movie => getElement(movie)));
            section.Add(loadMore = new LoadMoreElement("Load more", "Loading...", async (element) =>
            {
                await viewModel.ExecuteLoadMoreCommand();

                loadMovies();
            }));
        }
        private Element getElement(Mymdb.Core.Models.Movie movie)
        {
            var image = (string.IsNullOrEmpty(movie.ImageUrl)) ?
                (UIImage)null :
                new UIImage(NSData.FromUrl(new NSUrl(viewModel.ExecuteGetImageCommand(movie.ImageUrl))));

            return (Element)new ImageStringElement(movie.Title, () =>
            {
                this.NavigationController.PushViewController(new MovieViewController(movie.Id), true);
            }, image);
        }

    }
}