using Everaldo.Cardoso.C19BR.Mobile.View;
using Everaldo.Cardoso.C19BR.Mobile.ViewModel;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Menu = Everaldo.Cardoso.C19BR.Mobile.View.Menu;

namespace Everaldo.Cardoso.C19BR.Mobile
{
    public partial class App : Prism.DryIoc.PrismApplication
    {
        public App() : base(null)
        {
        }

        public App(IPlatformInitializer initializer)
           : this(initializer, true)
        {
        }

        public App(IPlatformInitializer initializer, bool setFormsDependencyResolver)
            : base(initializer, setFormsDependencyResolver)
        {
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<Menu, MenuViewModel>();
            containerRegistry.RegisterForNavigation<CountryDetail, CountryDetailViewModel>();
            containerRegistry.RegisterForNavigation<StatesCasesList, StatesCasesListViewModel>();
            containerRegistry.RegisterForNavigation<WorldCasesList, WorldCasesListViewModel>();
            containerRegistry.RegisterForNavigation<CityCasesList, CityCasesListViewModel>();
            containerRegistry.RegisterForNavigation<CountryGraphicsDetail, CountryGraphicsDetailViewModel>();
            containerRegistry.RegisterForNavigation<StateGraphicsDetail, StateGraphicsDetailViewModel>();
            containerRegistry.RegisterForNavigation<CityGraphicsDetail, CityGraphicsDetailViewModel>();
            containerRegistry.RegisterForNavigation<About, AboutViewModel>();
            
        }

        protected async override void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("NavigationPage/Menu");
        }
    }
}
