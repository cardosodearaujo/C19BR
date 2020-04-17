using Android.App;
using Everaldo.Cardoso.C19BR.Mobile.Services;
using Xamarin.Forms;

namespace Everaldo.Cardoso.Araujo.C19BR.Mobile.Droid.Services
{
    public class CloseService : ICloseService
    {
        public void Close()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}