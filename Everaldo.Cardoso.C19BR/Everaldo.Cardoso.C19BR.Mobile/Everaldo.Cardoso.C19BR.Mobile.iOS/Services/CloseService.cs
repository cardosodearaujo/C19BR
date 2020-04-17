using Everaldo.Cardoso.C19BR.Mobile.Services;
using System.Threading;
using Xamarin.Forms;

[assembly: Dependency(typeof(Everaldo.Cardoso.Araujo.C19BR.Mobile.iOS.Services.CloseService))]
namespace Everaldo.Cardoso.Araujo.C19BR.Mobile.iOS.Services
{
    public class CloseService : ICloseService
    {
        public void Close()
        {
            Thread.CurrentThread.Abort();
        }
    }
}