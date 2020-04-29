using Acr.UserDialogs;
using Everaldo.Cardoso.C19BR.Domain.Services;
using Everaldo.Cardoso.C19BR.Domain.ValueObjects;
using Everaldo.Cardoso.C19BR.Framework.Bases;
using Everaldo.Cardoso.C19BR.Framework.ToolBox;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Everaldo.Cardoso.C19BR.Mobile.ViewModel
{
    public class StatesCasesListViewModel : BaseViewModel
    {
        public StatesCasesListViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
            : base(navigationService, pageDialogService)
        {
        }

        #region "Propriedades"
        public List<ItemSearchListVO> _List;
        public List<ItemSearchListVO> List
        {
            get { return _List; }
            set { SetProperty(ref _List, value); }
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

        public ItemSearchListVO SelectedItem { set { DetailItem(value); } }
        #endregion

        #region "Metodos"
        public override async void Initialize(INavigationParameters parameters)
        {
            IsBusy = true;
            try
            {
                Refresh = new Command(LoadData);
                await LoadDataStates();
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

        private async void DetailItem(ItemSearchListVO item)
        {
            if (item != null)
            {
                var states = StatesOfBrazil.getStatesOfBrazil();
                var stade = states.Where(F => F.Name == item.Name).FirstOrDefault();
                if (stade != null)
                {
                    var pairs = new NavigationParameters();
                    pairs.Add("UF", stade.UF);
                    pairs.Add("State", stade.Name);
                    await NavigationService.NavigateAsync("CityCasesList", pairs, animated: true);
                }
                else
                {
                    Dialog.Toast("Nenhum estado selecioando!", TimeSpan.FromSeconds(5));
                }                            
            }
        }

        private async void LoadData()
        {
            IsRefreshing = true;
            try
            {
                await LoadDataStates();
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

        private async Task LoadDataStates()
        {
            var service = new CasesBrasilService();
            var cases = await service.GetCasesFromStates();
            if (cases != null)
            {
                var states = StatesOfBrazil.getStatesOfBrazil();
                List = (from R in cases.results
                        orderby R.confirmed descending
                        select new ItemSearchListVO
                        {
                            Name = states.Where(F => F.UF == R.state).FirstOrDefault().Name.ToUpper(),
                            Confirmed = string.Format("{0:N0}", R.confirmed),
                            Deaths = string.Format("{0:N0}", R.deaths),
                            DeathRate = R.death_rate == null ? "0,0%" : (string.Format("{0:N1}", (((decimal)R.death_rate) * 100)) + "%")
                        }).ToList();
            }           
        }
        #endregion
    }
}
