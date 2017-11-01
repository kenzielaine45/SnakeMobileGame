using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;

namespace Mono.Samples.Snake
{
    [Activity(Theme = "@style/MyTheme.Splash", NoHistory = true, Label = "SplashActivity", MainLauncher = true)]
    public class SplashActivity : Activity
    {
        static readonly string TAG = "X:" + typeof(MainMenuActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { SimulateStartup(); });
            startupWork.Start();
        }

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed() { }

        // Simulates background work that happens behind the splash screen
        async void SimulateStartup()
        {
            await Task.Delay(800); // Simulate a bit of startup work.
            StartActivity(new Intent(Application.Context, typeof(MainMenuActivity)));
        }
    }
}