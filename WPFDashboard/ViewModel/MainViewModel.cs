using DataAgent;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using LocalSynchronization;
using WPFDashboard.ViewModel.ViewModelMenu;

namespace WPFDashboard.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        #region FIELDS
        private LocalSynchronizer localSync;
        private ViewModelBase currentView;
        private bool connectStatus;
        #endregion

        #region PROPERTIES
        public ViewModelBase CurrentView
        {
            get { return currentView; }
            set
            {
                currentView = value;
                RaisePropertyChanged();
            }
        }
        public bool ConnectStatus
        {
            get { return connectStatus; }
            set
            {
                connectStatus = value;
                RaisePropertyChanged();
            }
        }
        public RelayCommand BtnOrdersView { get; set; }
        public RelayCommand BtnCommentsView { get; set; }
        public RelayCommand BtnCreationsView { get; set; }
        public RelayCommand BtnIngredientsView { get; set; }
        public RelayCommand BtnPackagesView { get; set; }
        public RelayCommand BtnRefresh { get; set; }
        #endregion



        public MainViewModel()
        {
            GetConnectionStatus();

            CurrentView = SimpleIoc.Default.GetInstance<OrdersVm>();
            BtnOrdersView = new RelayCommand(() => { CurrentView = SimpleIoc.Default.GetInstance<OrdersVm>(); GetConnectionStatus(); });
            BtnPackagesView = new RelayCommand(() => { CurrentView = SimpleIoc.Default.GetInstance<PackagesVm>(); GetConnectionStatus(); });
            BtnIngredientsView = new RelayCommand(() => { CurrentView = SimpleIoc.Default.GetInstance<IngredientsVm>(); GetConnectionStatus(); });
            BtnCommentsView = new RelayCommand(() => { CurrentView = SimpleIoc.Default.GetInstance<CommentsVm>(); GetConnectionStatus(); });
            BtnCreationsView = new RelayCommand(() => { CurrentView = SimpleIoc.Default.GetInstance<CreationsVm>(); GetConnectionStatus(); });

            BtnRefresh = new RelayCommand(() =>
            {
                GetConnectionStatus();
                Messenger.Default.Send(new RefreshMessage(CurrentView.GetType()));
            });

            StartSynchronizer();
        }

        private void StartSynchronizer()
        {
            localSync = new LocalSynchronizer(
                SimpleIoc.Default.GetInstance<OrdersVm>().ViewSynchronized,
                 SimpleIoc.Default.GetInstance<PackagesVm>().ViewSynchronized,
                  SimpleIoc.Default.GetInstance<CreationsVm>().ViewSynchronized,
                   SimpleIoc.Default.GetInstance<IngredientsVm>().ViewSynchronized,
                    SimpleIoc.Default.GetInstance<CommentsVm>().ViewSynchronized);
            localSync.StartSyncing();
        }
        private bool GetConnectionStatus()
        {
            return ConnectStatus = DataAgentUnit.GetInstance().GetSynchronizerStatus();
        }
    }
}