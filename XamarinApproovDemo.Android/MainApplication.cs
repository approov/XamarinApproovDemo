
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

using XamarinApproov;
using XamarinApproov.Droid;

namespace XamarinApproovDemo.Droid
{
    [Application]
    public class MainApp : Application
    {
        public MainApp(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();

            // Set up shared Android Approover
            Approover.Shared = new AndroidApproover(this.ApplicationContext);
        }
    }
}