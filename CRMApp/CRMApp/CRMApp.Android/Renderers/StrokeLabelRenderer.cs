using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using CRMApp.Droid.Renderer;
using CRMApp.Controls;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(StrokeLabel), typeof(StrokeLabelRenderer))]
namespace CRMApp.Droid.Renderer
{
    public class StrokeLabelRenderer : LabelRenderer
    {
        Context context;
        public StrokeLabelRenderer(Context context) : base(context)
        {
            this.context = context;
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            StrokeLabel customLabel = (StrokeLabel)Element;
            var StrokeTextViewColor = customLabel.StrokeColor;

            int StrokeThickness = customLabel.StrokeThickness;
            if (Control != null)
            {

                StrokeTextView strokeTextView = new StrokeTextView(context, Control.TextSize, StrokeTextViewColor, StrokeThickness);
                strokeTextView.Text = e.NewElement.Text;
                strokeTextView.SetTextColor(Control.TextColors);

                SetNativeControl(strokeTextView);
            }
        }
    }
}