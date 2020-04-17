using Everaldo.Cardoso.C19BR.Domain.Enums;
using Everaldo.Cardoso.C19BR.Domain.Services;
using Everaldo.Cardoso.C19BR.Domain.ValueObjects;
using Everaldo.Cardoso.C19BR.Framework.Bases;
using Everaldo.Cardoso.C19BR.Framework.Enums;
using Everaldo.Cardoso.C19BR.Framework.ToolBox;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

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
        #endregion

        #region "Metodos"
        public override async void Initialize(INavigationParameters parameters)
        {
            IsBusy = true;
            try
            {
                if (parameters["State"] != null && parameters["UF"] != null)
                {
                    Title = "Estado: " + CultureInfo.CurrentCulture.TextInfo.ToTitleCase(parameters["State"].ToString().ToLower()); 
                    await LoadData((States)EnumUtility.GetEnumByValue<States>(parameters["UF"].ToString()));
                }
            }
            catch (Exception ex)
            {
                Dialog.Toast(ex.Message, TimeSpan.FromSeconds(3));
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadData(States state)
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
                            DeathRate = city.death_rate == null ? "0,0%" : (string.Format("{0:N1}", (((decimal)city.death_rate) * 100)) + "%")
                        }).ToList();
            }
        }
        #endregion
    }
}
