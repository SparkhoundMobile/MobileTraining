// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace Mymdb.iOS
{
	[Register ("MovieViewController")]
	partial class MovieViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton btnCamera { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnDelete { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnImdb { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnPhoto { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton btnSave { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView imgImage { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblImdb { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblReleaseDate { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblRuntime { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel lblTitle { get; set; }

		[Outlet]
		MonoTouch.UIKit.UISwitch swtFavorite { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnImdb != null) {
				btnImdb.Dispose ();
				btnImdb = null;
			}

			if (btnCamera != null) {
				btnCamera.Dispose ();
				btnCamera = null;
			}

			if (btnDelete != null) {
				btnDelete.Dispose ();
				btnDelete = null;
			}

			if (btnPhoto != null) {
				btnPhoto.Dispose ();
				btnPhoto = null;
			}

			if (btnSave != null) {
				btnSave.Dispose ();
				btnSave = null;
			}

			if (imgImage != null) {
				imgImage.Dispose ();
				imgImage = null;
			}

			if (lblImdb != null) {
				lblImdb.Dispose ();
				lblImdb = null;
			}

			if (lblReleaseDate != null) {
				lblReleaseDate.Dispose ();
				lblReleaseDate = null;
			}

			if (lblRuntime != null) {
				lblRuntime.Dispose ();
				lblRuntime = null;
			}

			if (lblTitle != null) {
				lblTitle.Dispose ();
				lblTitle = null;
			}

			if (swtFavorite != null) {
				swtFavorite.Dispose ();
				swtFavorite = null;
			}
		}
	}
}
