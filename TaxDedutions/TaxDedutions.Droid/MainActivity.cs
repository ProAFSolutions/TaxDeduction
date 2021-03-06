﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services;

namespace TaxDedutions.Droid
{
    [Activity(Label = "TaxDedutions", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            try
            {
                #region Resolver Init
                SimpleContainer container = new SimpleContainer();
                container.Register<IDevice>(t => AndroidDevice.CurrentDevice);
                container.Register<IDisplay>(t => t.Resolve<IDevice>().Display);
                container.Register<INetwork>(t => t.Resolve<IDevice>().Network);

                Resolver.SetResolver(container.GetResolver());
                #endregion
            }
            catch (Exception ex)
            {
            }
            

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}

