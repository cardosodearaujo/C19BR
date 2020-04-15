using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Everaldo.Cardoso.C19BR.Mobile.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : TabbedPage
    {
        public Menu()
        {
            InitializeComponent();
            LoadCurrentPage();
        }

        private void LoadCurrentPage()
        {
            var currentPage = tbpMenu.Children.Where(F => F.GetType() == typeof(CountryDetail)).FirstOrDefault();
            if (currentPage != null) tbpMenu.CurrentPage = currentPage;
        }

        private void PageChanged(object sender, EventArgs e)
        {
            switch (CurrentPage.GetType().Name)
            {
                case "WorldCasesList":
                    Title = "Casos no mundo";
                    break;
                case "CountryDetail":
                    Title = "Casos no Brasil";
                    break;
                case "StatesCasesList":
                    Title = "Casos nos estados";
                    break;
            }
        }
    }
}