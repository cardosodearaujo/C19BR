using Everaldo.Cardoso.C19BR.Framework.Bases;
using Prism.Navigation;
using Prism.Services;

namespace Everaldo.Cardoso.C19BR.Mobile.ViewModel
{
    public class DetailViewModel : BaseViewModel
    {
        public DetailViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
        }
    }
}
