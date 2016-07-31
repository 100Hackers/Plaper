using AdBuddiz.Xamarin;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Preferences;
using Android.Views;

namespace Plaper {
    [Activity(Label = "Plaper"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.Portrait
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity {
        public static int TimesPlayed { get; private set; }
        protected override void OnCreate(Bundle bundle) {
            base.OnCreate(bundle);
            var g = new Game1();
            SetContentView((View)g.Services.GetService(typeof(View)));

#if DEBUG
            AdBuddizHandler.Instance.SetLogLevel(ABLogLevel.ABLogLevelInfo);
            AdBuddizHandler.Instance.SetTestModeActive();
#endif
            AdBuddizHandler.Instance.SetPublisherKey("c57dd531-29aa-4ca5-8dbd-35088b5ba993");
            AdBuddizHandler.Instance.CacheAds(this);

            ISharedPreferences prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            int timesPlayed = prefs.GetInt("times_played", 0);
            TimesPlayed = timesPlayed;

            ISharedPreferencesEditor editor = prefs.Edit();
            editor.PutInt("times_played", timesPlayed + 1);
            editor.Apply();

            g.Run();
        }
    }
}

