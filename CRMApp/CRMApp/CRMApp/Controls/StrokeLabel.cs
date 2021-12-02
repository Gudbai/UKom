using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CRMApp.Controls
{
    public class StrokeLabel : Label
    {
        public static readonly BindableProperty StrokeColorProperty = BindableProperty.CreateAttached("StrokeColor", typeof(string), typeof(StrokeLabel), "");
        public string StrokeColor
        {
            get { return base.GetValue(StrokeColorProperty).ToString(); }
            set { base.SetValue(StrokeColorProperty, value); }
        }

        public static readonly BindableProperty StrokeThicknessProperty = BindableProperty.CreateAttached("StrokeThickness", typeof(int), typeof(StrokeLabel), 0);
        public int StrokeThickness
        {
            get { return (int)base.GetValue(StrokeThicknessProperty); }
            set { base.SetValue(StrokeThicknessProperty, value); }
        }

    }
}
