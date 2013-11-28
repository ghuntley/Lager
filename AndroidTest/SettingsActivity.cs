using Akavache;
using Android.App;
using Android.OS;
using Android.Preferences;
using Lager.Android;
using System;

namespace AndroidTest
{
    [Activity(Label = "My Activity")]
    public class SettingsActivity : PreferenceActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.AddPreferencesFromResource(Resource.Layout.Settings);

            BlobCache.ApplicationName = "Settings Test App";

            var storage = new TestSettings();

            var textPreference = (EditTextPreference)this.FindPreference("pref_text");
            textPreference.BindToSetting(storage, x => x.Text, x => x.Text, x => x.ToString());

            var boolPreference = (CheckBoxPreference)this.FindPreference("pref_bool");
            boolPreference.BindToSetting(storage, x => x.Boolean, x => x.Checked, x => bool.Parse((string)x));

            var listPreference = (ListPreference)this.FindPreference("pref_list");
            listPreference.SetEntryValues(Enum.GetNames(typeof(ListEnum)));
            listPreference.BindToSetting(storage, x => x.ListItem, x => x.Value, x => Enum.Parse(typeof(ListEnum), (string)x), x => x.ToString());

            var validationPreference = (EditTextPreference)this.FindPreference("pref_validation");
            validationPreference.BindToSetting(storage, x => x.Number, x => x.Text, x => int.Parse(x.ToString()), x => x.ToString(), x => x < 100 && x > 200);
        }
    }
}