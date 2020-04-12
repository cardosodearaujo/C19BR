using Acr.UserDialogs;
using Everaldo.Cardoso.C19BR.Framework.Bases;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Threading.Tasks;

namespace Everaldo.Cardoso.C19BR.Mobile.ViewModel
{
    public class DetailViewModel : BaseViewModel
    {
        public DetailViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {

        }

        #region "Propriedades"
        private string _Location;
        public string Location
        {
            get { return _Location; }
            set { SetProperty(ref _Location, value); }
        }


        private string _Population;
        public string Population
        {
            get { return _Population; }
            set { SetProperty(ref _Population, value); }
        }


        private string _NumberDeaths;
        public string NumberDeaths
        {
            get { return _NumberDeaths; }
            set { SetProperty(ref _NumberDeaths, value); }
        }


        private string _NumberCases;
        public string NumberCases
        {
            get { return _NumberCases; }
            set { SetProperty(ref _NumberCases, value); }
        }


        private string _DateUpdate;
        public string DateUpdate
        {
            get { return _DateUpdate; }
            set { SetProperty(ref _DateUpdate, value); }
        }
        #endregion

        public override async void Initialize(INavigationParameters parameters)
        {
            var dialog = UserDialogs.Instance.Loading(title: "Aguarde...", maskType: MaskType.Gradient);
            Location = "Brasil";
            Population = string.Format("{0:N}", 211372054);
            NumberCases = string.Format("{0:N}", 19430);
            NumberDeaths = string.Format("{0:N}", 1124);
            DateUpdate = "11/04/2020";
            await Task.Delay(5000);
            dialog.Dispose();
        }
    }
}
