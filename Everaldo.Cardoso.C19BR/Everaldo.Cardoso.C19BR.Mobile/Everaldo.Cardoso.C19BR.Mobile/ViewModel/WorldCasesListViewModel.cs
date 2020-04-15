using Acr.UserDialogs;
using Everaldo.Cardoso.C19BR.Domain.Services;
using Everaldo.Cardoso.C19BR.Domain.ValueObjects;
using Everaldo.Cardoso.C19BR.Framework.Bases;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Everaldo.Cardoso.C19BR.Mobile.ViewModel
{
    public class WorldCasesListViewModel : BaseViewModel
    {
        public WorldCasesListViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
        }


        #region "Propriedades"
        public List<ItemSearchListVO> _List;
        public List<ItemSearchListVO> List
        {
            get { return _List; }
            set { SetProperty(ref _List, value); }
        }

        public ItemSearchListVO SelectedItem { set { DetailItem(value); } }
        #endregion

        #region "Metodos"
        public override async void Initialize(INavigationParameters parameters)
        {
            IsBusy = true;
            try
            {
                await LoadData();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Toast(ex.Message, TimeSpan.FromSeconds(3));
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void DetailItem(ItemSearchListVO item)
        {
            if (item != null)
            {
                //Abrir nova tela...
                //Application.Current.MainPage = new NavigationPage(new Menu()) { BarBackgroundColor = Color.FromHex("#03A9F4") };
                //UserDialogs.Instance.Toast("Você selecionou o estado: " + item.Name, TimeSpan.FromSeconds(10));
            }
        }

        private async Task LoadData()
        {
            var service = new CasesWorldService();
            var cases = await service.GetCasesFromWorld();
            if (cases != null)
            {
                List = (from pais in cases.data
                        orderby (pais.latest_data.confirmed == null ? 0 : (long)pais.latest_data.confirmed) descending
                        select new ItemSearchListVO
                        {
                            Name = pais.name.ToUpper(),
                            Confirmed = string.Format("{0:N0}", (pais.latest_data.confirmed == null ? 0 : (long)pais.latest_data.confirmed)),
                            Recovered = string.Format("{0:N0}", (pais.latest_data.recovered == null ? 0 : (long)pais.latest_data.recovered)),
                            Deaths = string.Format("{0:N0}", (pais.latest_data.deaths == null ? 0 : (long)pais.latest_data.deaths)),
                            DeathRate = string.Format("{0:N1}", (pais.latest_data.calculated.death_rate == null ? 0 : ((decimal)pais.latest_data.calculated.death_rate))) + "%"
                        }).ToList();
            }
        }
        #endregion
    }
}
