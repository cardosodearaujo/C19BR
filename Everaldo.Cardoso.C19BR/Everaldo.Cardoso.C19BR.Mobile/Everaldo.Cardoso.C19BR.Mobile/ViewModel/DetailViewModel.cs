using Acr.UserDialogs;
using Everaldo.Cardoso.C19BR.Domain.Services;
using Everaldo.Cardoso.C19BR.Framework.Bases;
using Prism.Navigation;
using Prism.Services;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Everaldo.Cardoso.C19BR.Mobile.ViewModel
{
    public class DetailViewModel : BaseViewModel
    {
        public DetailViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {

        }

        #region "Propriedades"
        public Command _UpdateData;
        public Command UpdateData
        {
            get { return _UpdateData; }
            set { SetProperty(ref _UpdateData, value); }
        }


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
            UpdateData = new Command(LoadDataCountry);
            LoadDataCountry();   
        }

        private async void LoadDataCountry()
        {
            var dialog = UserDialogs.Instance.Loading(title: "Obtendo dados do Brasil...", maskType: MaskType.Gradient);

            var service = new CasesService();
            var cases = await service.GetCasesByStates();

            if (cases != null)
            {
                Location = "Brasil";
                Population = string.Format("{0:N0}", cases.results.Select(F => F.estimated_population_2019).Sum());
                NumberCases = string.Format("{0:N0}", cases.results.Select(F => F.confirmed).Sum());
                NumberDeaths = string.Format("{0:N0}", cases.results.Select(F => F.deaths).Sum());
                DateUpdate = cases.results.Max(F => F.date).ToString("dd/MM/yyyy");
            }

            dialog.Dispose();
        }
    }
}
