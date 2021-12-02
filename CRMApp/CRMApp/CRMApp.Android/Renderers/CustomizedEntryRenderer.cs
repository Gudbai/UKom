using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using CRMApp.Droid.Renderer;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomizedEntryRenderer))]
namespace CRMApp.Droid.Renderer
{
    public class CustomizedEntryRenderer : EntryRenderer
    {
        public CustomizedEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Black);
            }
        }
    }
}