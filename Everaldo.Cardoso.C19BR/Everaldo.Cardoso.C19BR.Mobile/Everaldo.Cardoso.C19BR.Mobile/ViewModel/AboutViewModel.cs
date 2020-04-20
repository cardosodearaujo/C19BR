using Everaldo.Cardoso.C19BR.Framework.Bases;
using Everaldo.Cardoso.C19BR.Mobile.Services;
using Prism.Navigation;
using Prism.Services;
using System;
using Xamarin.Forms;

namespace Everaldo.Cardoso.C19BR.Mobile.ViewModel
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
        }
        

        #region "Propriedades"
        private WebViewSource _WebViewSource;
        public WebViewSource WebViewSource
        {
            get { return _WebViewSource; }
            set { SetProperty(ref _WebViewSource, value); }
        }
        #endregion

        #region "Metodos"
        public override void Initialize(INavigationParameters parameters)
        {
            IsBusy = true;
            try
            {
                WebViewSource = Xamarin.Forms.DependencyService.Get<IBaseURLService>().Get() + "info.html";
            }
            catch (Exception ex)
            {
                Dialog.Toast(ex.Message, TimeSpan.FromSeconds(5));
            }
            finally
            {
                IsBusy = false;
            }            
        }
        #endregion
    }
}
