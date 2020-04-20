using Everaldo.Cardoso.C19BR.Mobile.Services;
using Foundation;
using Xamarin.Forms;

[assembly: Dependency(typeof(Everaldo.Cardoso.Araujo.C19BR.Mobile.iOS.Services.BaseURLService))]
namespace Everaldo.Cardoso.Araujo.C19BR.Mobile.iOS.Services
{
    public class BaseURLService : IBaseURLService
    {
        public string Get()
        {
            return NSBundle.MainBundle.BundlePath;
        }
    }
}