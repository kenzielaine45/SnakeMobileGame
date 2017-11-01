using System;
using Android.OS;

namespace Mono.Samples.Snake
{
	public class RefreshHandler : Handler
	{
		private SnakeView view;

		public RefreshHandler (SnakeView view)
		{
			this.view = view;
		}

		public override void HandleMessage (Message msg)
		{
			view.Update ();
			view.Invalidate ();
		}

		public void Sleep (long delayMillis)
		{
			this.RemoveMessages (0);
			SendMessageDelayed (ObtainMessage (0), delayMillis);
		}
	}
}