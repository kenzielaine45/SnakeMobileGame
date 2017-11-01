using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;

namespace Mono.Samples.Snake
{
    [Activity()]
    public class MainMenuActivity : Activity
    {
        private Button _btnPlay, _btnInstructions, _btnCredits;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.MainMenu);

            _btnPlay = FindViewById<Button>(Resource.Id.btnPlay);
            _btnPlay.Click += _btnPlay_Click;

            _btnInstructions = FindViewById<Button>(Resource.Id.btnInstructions);
            _btnInstructions.Click += _btnInstructions_Click;

            _btnCredits = FindViewById<Button>(Resource.Id.btnCredits);
            _btnCredits.Click += _btnCredits_Click;
        }

        private void _btnCredits_Click(object sender, System.EventArgs e)
        {
            StartActivity(new Intent(Application.Context, typeof(Credits)));
        }

        private void _btnInstructions_Click(object sender, System.EventArgs e)
        {
            StartActivity(new Intent(Application.Context, typeof(InstructionsActivity)));
        }

        private void _btnPlay_Click(object sender, System.EventArgs e)
        {
            StartActivity(new Intent(Application.Context, typeof(SnakeActivity)));
        }
    }
}