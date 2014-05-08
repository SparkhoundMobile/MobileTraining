using System;

using Android.App;

namespace Mymdb.Droid
{
    [Application(Theme = "@android:style/Theme.Holo.Light")]
    public class MymdbApplication : Application
    {
        public static Activity CurrentActivity { get; set; }
        public MymdbApplication(IntPtr handle, global::Android.Runtime.JniHandleOwnership transer)
            : base(handle, transer)
        {

        }

        public override void OnCreate()
        {
            base.OnCreate();
            Mymdb.Core.ServiceRegistrar.Startup();
        }
    }
}