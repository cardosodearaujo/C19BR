using Everaldo.Cardoso.C19BR.Domain.ValueObjects;
using Everaldo.Cardoso.C19BR.Mobile.ViewModel;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms.Xaml;

namespace Everaldo.Cardoso.C19BR.Mobile.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CasesTodayCountriesDetail : PopupPage
    {
        public CasesTodayCountriesDetail(ItemSearchListVO item)
        {
            InitializeComponent();
            lblCountryName.Text = item.Name;
            lblConfirmedToday.Text = item.ConfirmedToday;
            lblDeathsToday.Text = item.DeathsToday;
        }
    }
}