package crc64bfd6e9d8643170fe;


public class StrokeTextView
	extends android.widget.TextView
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getLayoutParams:()Landroid/view/ViewGroup$LayoutParams;:GetGetLayoutParametersHandler\n" +
			"n_setLayoutParams:(Landroid/view/ViewGroup$LayoutParams;)V:GetSetLayoutParameters_Landroid_view_ViewGroup_LayoutParams_Handler\n" +
			"n_onMeasure:(II)V:GetOnMeasure_IIHandler\n" +
			"n_onLayout:(ZIIII)V:GetOnLayout_ZIIIIHandler\n" +
			"n_onDraw:(Landroid/graphics/Canvas;)V:GetOnDraw_Landroid_graphics_Canvas_Handler\n" +
			"";
		mono.android.Runtime.register ("CRMApp.Controls.StrokeTextView, CRMApp", StrokeTextView.class, __md_methods);
	}


	public StrokeTextView (android.content.Context p0)
	{
		super (p0);
		if (getClass () == StrokeTextView.class)
			mono.android.TypeManager.Activate ("CRMApp.Controls.StrokeTextView, CRMApp", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public StrokeTextView (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == StrokeTextView.class)
			mono.android.TypeManager.Activate ("CRMApp.Controls.StrokeTextView, CRMApp", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public StrokeTextView (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == StrokeTextView.class)
			mono.android.TypeManager.Activate ("CRMApp.Controls.StrokeTextView, CRMApp", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public StrokeTextView (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == StrokeTextView.class)
			mono.android.TypeManager.Activate ("CRMApp.Controls.StrokeTextView, CRMApp", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public android.view.ViewGroup.LayoutParams getLayoutParams ()
	{
		return n_getLayoutParams ();
	}

	private native android.view.ViewGroup.LayoutParams n_getLayoutParams ();


	public void setLayoutParams (android.view.ViewGroup.LayoutParams p0)
	{
		n_setLayoutParams (p0);
	}

	private native void n_setLayoutParams (android.view.ViewGroup.LayoutParams p0);


	public void onMeasure (int p0, int p1)
	{
		n_onMeasure (p0, p1);
	}

	private native void n_onMeasure (int p0, int p1);


	public void onLayout (boolean p0, int p1, int p2, int p3, int p4)
	{
		n_onLayout (p0, p1, p2, p3, p4);
	}

	private native void n_onLayout (boolean p0, int p1, int p2, int p3, int p4);


	public void onDraw (android.graphics.Canvas p0)
	{
		n_onDraw (p0);
	}

	private native void n_onDraw (android.graphics.Canvas p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
