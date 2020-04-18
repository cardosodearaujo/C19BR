using Everaldo.Cardoso.C19BR.Domain.Services;
using Everaldo.Cardoso.C19BR.Domain.ValueObjects;
using Everaldo.Cardoso.C19BR.Framework.Bases;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Everaldo.Cardoso.C19BR.Mobile.ViewModel
{
    public class CountryDetailViewModel : BaseViewModel
    {
        public CountryDetailViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base(navigationService, pageDialogService)
        {
        }

        #region "Propriedades"
        public Command _ListStates;
        public Command ListStates
        {
            get { return _ListStates; }
            set { SetProperty(ref _ListStates, value); }
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

        private string _Recovered;
        public string Recovered
        {
            get { return _Recovered; }
            set { SetProperty(ref _Recovered, value); }
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


        private string _DeathsRate;
        public string DeathsRate
        {
            get { return _DeathsRate; }
            set { SetProperty(ref _DeathsRate, value); }
        }

        private string _Critical;
        public string Critical
        {
            get { return _Critical; }
            set { SetProperty(ref _Critical, value); }
        }

        private string _ConfirmedToday;
        public string ConfirmedToday
        {
            get { return _ConfirmedToday; }
            set { SetProperty(ref _ConfirmedToday, value); }
        }

        private string _DeathsToday;
        public string DeathsToday
        {
            get { return _DeathsToday; }
            set { SetProperty(ref _DeathsToday, value); }
        }

        private string _DateUpdate;
        public string DateUpdate
        {
            get { return _DateUpdate; }
            set { SetProperty(ref _DateUpdate, value); }
        }

        private Command _Refresh;
        public Command Refresh
        {
            get { return _Refresh; }
            set { SetProperty(ref _Refresh, value); }
        }

        private bool _IsRefreshing;
        public bool IsRefreshing
        {
            get { return _IsRefreshing; }
            set { SetProperty(ref _IsRefreshing, value); }
        }
        #endregion

        #region "Metodos"
        public override async void Initialize(INavigationParameters parameters)
        {
            IsBusy = true;
            try
            {
                Refresh = new Command(LoadData);
                await LoadDataCountry();                
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

        private async void LoadData()
        {
            IsRefreshing = true;
            try
            {                
                await LoadDataCountry();                
            }
            catch (Exception ex)
            {
                Dialog.Toast(ex.Message, TimeSpan.FromSeconds(5));
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private async Task LoadDataCountry()
        {            
            var service = new CasesWorldService();
            var cases = await service.GetCasesFromBrasil();

            if (cases != null && cases.data != null)
            {
                var data = cases.data;

                Location = "Brasil";
                Recovered = string.Format("{0:N0}", data.latest_data.recovered);
                NumberCases = string.Format("{0:N0}", data.latest_data.confirmed);
                NumberDeaths = string.Format("{0:N0}", data.latest_data.deaths);
                Critical = string.Format("{0:N0}", data.latest_data.critical);
                Population = string.Format("{0:N0}", data.population);
                ConfirmedToday = string.Format("{0:N0}", data.today.confirmed);
                DeathsToday = string.Format("{0:N0}", data.today.deaths);
                DeathsRate = string.Format("{0:N1}", (data.latest_data.calculated.death_rate)) + "%";
                DateUpdate = data.updated_at.ToString("dd/MM/yyyy");
            }            
        }
        #endregion
    }
}
