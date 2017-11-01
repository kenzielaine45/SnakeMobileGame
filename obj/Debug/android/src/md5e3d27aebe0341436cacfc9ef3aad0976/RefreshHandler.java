package md5e3d27aebe0341436cacfc9ef3aad0976;


public class RefreshHandler
	extends android.os.Handler
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_handleMessage:(Landroid/os/Message;)V:GetHandleMessage_Landroid_os_Message_Handler\n" +
			"";
		mono.android.Runtime.register ("Mono.Samples.Snake.RefreshHandler, Snake, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", RefreshHandler.class, __md_methods);
	}


	public RefreshHandler () throws java.lang.Throwable
	{
		super ();
		if (getClass () == RefreshHandler.class)
			mono.android.TypeManager.Activate ("Mono.Samples.Snake.RefreshHandler, Snake, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public RefreshHandler (android.os.Handler.Callback p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == RefreshHandler.class)
			mono.android.TypeManager.Activate ("Mono.Samples.Snake.RefreshHandler, Snake, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.OS.Handler+ICallback, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public RefreshHandler (android.os.Looper p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == RefreshHandler.class)
			mono.android.TypeManager.Activate ("Mono.Samples.Snake.RefreshHandler, Snake, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.OS.Looper, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public RefreshHandler (android.os.Looper p0, android.os.Handler.Callback p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == RefreshHandler.class)
			mono.android.TypeManager.Activate ("Mono.Samples.Snake.RefreshHandler, Snake, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.OS.Looper, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.OS.Handler+ICallback, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}

	public RefreshHandler (md5e3d27aebe0341436cacfc9ef3aad0976.SnakeView p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == RefreshHandler.class)
			mono.android.TypeManager.Activate ("Mono.Samples.Snake.RefreshHandler, Snake, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Mono.Samples.Snake.SnakeView, Snake, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public void handleMessage (android.os.Message p0)
	{
		n_handleMessage (p0);
	}

	private native void n_handleMessage (android.os.Message p0);

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
