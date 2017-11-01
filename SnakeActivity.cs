using System;
using Android.App;
using Android.OS;
using Android.Widget;

namespace Mono.Samples.Snake
{

	[Activity (Label = "Snake", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
	public class SnakeActivity : Activity
	{
		private SnakeView snake_view;

		private static String ICICLE_KEY = "snake-view";

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.snake_layout);

			snake_view = FindViewById<SnakeView> (Resource.Id.snake);
			snake_view.SetTextView (FindViewById<TextView> (Resource.Id.text));

			if (savedInstanceState == null) {

				snake_view.SetMode (GameMode.Ready);
			} else {

				Bundle map = savedInstanceState.GetBundle (ICICLE_KEY);

				if (map != null)
					snake_view.RestoreState (map);
				else
					snake_view.SetMode (GameMode.Paused);
			}
		}

		protected override void OnPause ()
		{
			base.OnPause ();

			snake_view.SetMode (GameMode.Paused);
		}

		protected override void OnSaveInstanceState (Bundle outState)
		{

			outState.PutBundle (ICICLE_KEY, snake_view.SaveState ());
		}
	}
}
