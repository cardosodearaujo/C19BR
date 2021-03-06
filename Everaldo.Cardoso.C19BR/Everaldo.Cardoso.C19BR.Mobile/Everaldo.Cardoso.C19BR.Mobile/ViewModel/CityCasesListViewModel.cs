﻿using Everaldo.Cardoso.C19BR.Domain.Enums;
using Everaldo.Cardoso.C19BR.Domain.Services;
using Everaldo.Cardoso.C19BR.Domain.ValueObjects;
using Everaldo.Cardoso.C19BR.Framework.Bases;
using Everaldo.Cardoso.C19BR.Framework.Enums;
using Everaldo.Cardoso.C19BR.Framework.ToolBox;
using Everaldo.Cardoso.C19BR.Mobile.Services;
using Everaldo.Cardoso.C19BR.Mobile.View;
using Prism.Navigation;
using Prism.Services;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Everaldo.Cardoso.C19BR.Mobile.ViewModel
{
    public class CityCasesListViewModel : BaseViewModel
    {
        public CityCasesListViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
        }

        #region "Propriedades"
        private List<ItemSearchListVO> _List;
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

        public Command _Graphics;
        public Command Graphics
        {
            get { return _Graphics; }
            set { SetProperty(ref _Graphics, value); }
        }

        private bool _IsRefreshing;
        public bool IsRefreshing
        {
            get { return _IsRefreshing; }
            set { SetProperty(ref _IsRefreshing, value); }
        }

        public ItemSearchListVO SelectedItem { set { DetailItem(value); } }

        public States AtualState { get; set; }
        #endregion

        #region "Metodos"
        public override async void Initialize(INavigationParameters parameters)
        {
            IsBusy = true;
            try
            {
                if (parameters["State"] != null && parameters["UF"] != null)
                {
                    Title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(parameters["State"].ToString().ToLower());
                    AtualState = (States)EnumUtility.GetEnumByValue<States>(parameters["UF"].ToString());
                    await LoadDataCity(AtualState);
                    Refresh = new Command(LoadData);
                    Graphics = new Command(OpenGraphics);
                }
                else
                {
                    Xamarin.Forms.DependencyService.Get<ICloseService>().Close();
                }
                
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
                await LoadDataCity(AtualState);
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

        private async Task LoadDataCity(States state)
        {
            var service = new CasesBrasilService();
            var cases = await service.GetCasesFromCity(state);
            if (cases != null)
            {
                var states = StatesOfBrazil.getStatesOfBrazil();
                List = (from city in cases.results
                        orderby city.confirmed descending
                        where city.city != null //Para ignorar o totalizador...
                        select new ItemSearchListVO
                        {
                            Name = city.city.ToUpper(),
                            Confirmed = string.Format("{0:N0}", city.confirmed),
                            Deaths = string.Format("{0:N0}", city.deaths),
                            DeathRate = city.death_rate == null ? "0,0%" : (string.Format("{0:N1}", (((decimal)city.death_rate) * 100)) + "%"),
                            IBGE = city.city_ibge_code
                        }).ToList();
            }
        }

        private async void OpenGraphics()
        {
            if (AtualState != null) await PopupNavigation.Instance.PushAsync(new StateGraphicsDetail(AtualState));
        }

        private async void DetailItem(ItemSearchListVO item)
        {
            if (item != null) await PopupNavigation.Instance.PushAsync(new CityGraphicsDetail(AtualState,item));
        }
        #endregion
    }
}
