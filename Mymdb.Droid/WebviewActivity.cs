using Android.App;
using Android.Content;
using Android.OS;
using Android.Webkit;

namespace Mymdb.Droid
{
    [Activity(Label = "Imdb")]
    public class WebviewActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.WebView);

            var imdbId = Intent.GetStringExtra("imdbId");

            WebView localWebView = FindViewById<WebView>(Resource.Id.LocalWebView);
            localWebView.LoadUrl(string.Format("http://m.imdb.com/title/{0}", imdbId));
        }
    }
}