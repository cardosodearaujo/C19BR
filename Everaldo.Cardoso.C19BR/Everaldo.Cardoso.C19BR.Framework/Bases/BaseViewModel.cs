using Acr.UserDialogs;
using Prism;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Everaldo.Cardoso.C19BR.Framework.Bases
{
    public class BaseViewModel : BindableBase, IInitialize, INavigationAware, IDestructible, IActiveAware, INotifyPropertyChanged
    {
        #region "Eventos"
        public event EventHandler IsActiveChanged;
        #endregion

        #region "Propriedades e eventos - Não MVVM"
        public IUserDialogs Dialog { get { return Dialogs(); } }
        private IUserDialogs Dialogs() { return UserDialogs.Instance; }
        #endregion

        #region "Propriedades - Simples"
        protected INavigationService NavigationService { get; set; }
        protected IPageDialogService PageDialogService { get; set; }
        protected bool HasInitialized { get; set; }
        #endregion

        #region "Propriedades - Completas"
        private string _title = string.Empty;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                SetProperty(ref _title, value);
            }
        }


        private string _subtitle = string.Empty;
        public string Subtitle
        {
            get
            {
                return _subtitle;
            }
            set
            {
                SetProperty(ref _subtitle, value);
            }
        }


        private string _icon = string.Empty;
        public string Icon
        {
            get
            {
                return _icon;
            }
            set
            {
                SetProperty(ref _icon, value);
            }
        }


        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                if (SetProperty(ref _isBusy, value))
                    IsNotBusy = !_isBusy;
            }
        }


        private bool _isNotBusy;
        public bool IsNotBusy
        {
            get
            {
                return _isNotBusy;
            }
            set
            {
                if (SetProperty(ref _isNotBusy, value))
                    IsBusy = !_isNotBusy;
            }
        }


        private bool _canLoadMore = true;
        public bool CanLoadMore
        {
            get
            {
                return _canLoadMore;
            }
            set
            {
                SetProperty(ref _canLoadMore, value);
            }
        }


        private string _header = string.Empty;
        public string Header
        {
            get
            {
                return _header;
            }
            set
            {
                SetProperty(ref _header, value);
            }
        }


        private string _footer = string.Empty;
        public string Footer
        {
            get
            {
                return _footer;
            }
            set
            {
                SetProperty(ref _footer, value);
            }
        }


        private bool _isActive;
        public bool IsActive
        {
            get =>_isActive;
            set => SetProperty(ref _isActive, value, RaiseIsActiveChanged);
        }
        #endregion

        #region "Construtor"
        public BaseViewModel(INavigationService navigationService, IPageDialogService pageDialogService)
        {
            NavigationService = navigationService;
            PageDialogService = pageDialogService;
        }
        #endregion

        #region "Metodos"
        public virtual void Destroy()
        {
        }
        protected virtual void RaiseIsActiveChanged()
        {
            IsActiveChanged?.Invoke(this, EventArgs.Empty);
        }
        public virtual Task LoadAsync(object[] args)
        {
            return Task.FromResult(true);
        }
        public virtual Task LoadAsync()
        {
            return Task.FromResult(true);
        }
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }
        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }
        public virtual void Initialize(INavigationParameters parameters)
        {
        }
        #endregion
    }
}


