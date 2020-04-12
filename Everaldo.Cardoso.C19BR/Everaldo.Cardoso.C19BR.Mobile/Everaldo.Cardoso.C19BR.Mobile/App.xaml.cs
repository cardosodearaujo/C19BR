using Everaldo.Cardoso.C19BR.Mobile.View;
using Everaldo.Cardoso.C19BR.Mobile.ViewModel;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Xamarin.Forms;

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
            containerRegistry.RegisterForNavigation<Detail, DetailViewModel>();
            containerRegistry.RegisterForNavigation<SearchList, SearchListViewModel>();
        }

        protected async override void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("NavigationPage/Detail");
        }
    }
}
