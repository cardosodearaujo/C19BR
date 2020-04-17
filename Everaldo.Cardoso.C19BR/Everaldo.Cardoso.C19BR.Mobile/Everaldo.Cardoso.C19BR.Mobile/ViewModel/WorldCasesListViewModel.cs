using Everaldo.Cardoso.C19BR.Domain.Services;
using Everaldo.Cardoso.C19BR.Domain.ValueObjects;
using Everaldo.Cardoso.C19BR.Framework.Bases;
using Everaldo.Cardoso.C19BR.Framework.ToolBox;
using Everaldo.Cardoso.C19BR.Framework.Translation;
using Everaldo.Cardoso.C19BR.Mobile.View;
using Prism.Navigation;
using Prism.Navigation.Xaml;
using Prism.Services;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Everaldo.Cardoso.C19BR.Mobile.ViewModel
{
    public class WorldCasesListViewModel : BaseViewModel
    {
        public WorldCasesListViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
        }

        #region "Propriedades"
        private bool AtivarTraducao = false;

        public List<ItemSearchListVO> _List;
        public List<ItemSearchListVO> List
        {
            get { return _List; }
            set { SetProperty(ref _List, value); }
        }

        public ItemSearchListVO SelectedItem { set { DetailItem(value); } }


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
                await LoadDataWorld();
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
            if (item != null) await PopupNavigation.Instance.PushAsync(new CasesTodayCountriesDetail(item));
        }

        private async void LoadData()
        {
            IsRefreshing = true;
            try
            {
                await LoadDataWorld();
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

        private async Task LoadDataWorld()
        {
            var casesService = new CasesWorldService();         
            var cases = await casesService.GetCasesFromWorld();
            if (cases != null)
            {
                var countries = CountriesOfWorld.getCountriesOfWorld();

                var lista = (from pais in cases.data
                        orderby (pais.latest_data.confirmed == null ? 0 : (long)pais.latest_data.confirmed) descending
                        select new ItemSearchListVO
                        {
                            Name = countries.Where(F => F.NameEn == pais.name.ToUpper().Trim()).Any() ? countries.Where(F => F.NameEn == pais.name.ToUpper().Trim()).FirstOrDefault().NamePT.ToUpper().Trim() : pais.name.ToUpper().ToUpper(),
                            Confirmed = string.Format("{0:N0}", (pais.latest_data.confirmed == null ? 0 : (long)pais.latest_data.confirmed)),
                            Recovered = string.Format("{0:N0}", (pais.latest_data.recovered == null ? 0 : (long)pais.latest_data.recovered)),
                            Deaths = string.Format("{0:N0}", (pais.latest_data.deaths == null ? 0 : (long)pais.latest_data.deaths)),
                            DeathRate = string.Format("{0:N1}", (pais.latest_data.calculated.death_rate == null ? 0 : ((decimal)pais.latest_data.calculated.death_rate))) + "%",
                            ConfirmedToday = string.Format("{0:N0}",(pais.today.confirmed == null ? 0 : (long)pais.today.confirmed)),
                            DeathsToday = string.Format("{0:N0}", (pais.today.deaths == null ? 0 : (long)pais.today.deaths))
                        }).ToList();

                if (AtivarTraducao)
                {
                    //Traduz até 2 milhões de caracteres por mês (modo gratuito)                    
                    var translationService = new TextTranslationService(new AuthenticationService(TranslationConstants.TextTranslatorApiKey));
                    foreach (var item in lista)
                    {
                        item.Name = await translationService.TranslateTextAsync(item.Name);
                    }
                }

                List = lista; 
            }
        }
        #endregion
    }
}
