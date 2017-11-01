using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Mono.Samples.Snake
{
    [Activity()]
    public class Credits : Activity
    {

        private Button _btnBack;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Credits);

            _btnBack = FindViewById<Button>(Resource.Id.btnBackCredits);
            _btnBack.Click += _btnBack_Click; ;
        }

        private void _btnBack_Click(object sender, EventArgs e)
        {
            StartActivity(new Intent(Application.Context, typeof(MainMenuActivity)));
        }
    }
}