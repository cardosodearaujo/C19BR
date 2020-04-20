using Everaldo.Cardoso.C19BR.Mobile.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(Everaldo.Cardoso.Araujo.C19BR.Mobile.Droid.Services.BaseURLService))]
namespace Everaldo.Cardoso.Araujo.C19BR.Mobile.Droid.Services
{
    
    public class BaseURLService : IBaseURLService
    {
        public string Get()
        {
            return "file:///android_asset/";
        }
    }
}