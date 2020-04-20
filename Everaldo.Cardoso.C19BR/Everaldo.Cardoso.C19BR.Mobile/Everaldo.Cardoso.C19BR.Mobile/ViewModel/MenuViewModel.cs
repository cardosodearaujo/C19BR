using Everaldo.Cardoso.C19BR.Framework.Bases;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace Everaldo.Cardoso.C19BR.Mobile.ViewModel
{
    public class MenuViewModel : BaseViewModel
    {
        public MenuViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
        }

        #region "Propriedades"
        public Command _AppAbout;
        public Command AppAbout
        {
            get { return _AppAbout; }
            set { SetProperty(ref _AppAbout, value); }
        }
        #endregion

        #region "Metodos"
        public override void Initialize(INavigationParameters parameters)
        {
            AppAbout = new Command(OpenAbout);
        }

        private async void OpenAbout()
        {
            await NavigationService.NavigateAsync("NavigationPage/About", animated: true);
        }
        #endregion
    }
}
