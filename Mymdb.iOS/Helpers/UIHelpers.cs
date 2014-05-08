using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Mymdb.iOS.Helpers
{
    public static class UIHelpers
    {
        public static NSObject Invoker;
        /// <summary>
        /// Ensures the invoked on main thread.
        /// </summary>
        /// <param name="action">Action to run on main thread.</param>
        public static void EnsureInvokedOnMainThread(Action action)
        {
            if (NSThread.Current.IsMainThread)
            {
                action();
                return;
            }
            if (Invoker == null)
                Invoker = new NSObject();

            Invoker.BeginInvokeOnMainThread(() => action());
        }
    }
}