using DataAgent;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using LocalSynchronization;
using WPFDashboard.ViewModel.ViewModelMenu;

namespace WPFDashboard.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        #region FIELDS
        private DataAgentUnit dataAgent;
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
        #endregion



        public MainViewModel()
        {
            dataAgent = new DataAgentUnit();
            GetConnectionStatus();
            InitializeVms();

            CurrentView = SimpleIoc.Default.GetInstance<OrdersVm>();
            BtnOrdersView = new RelayCommand(() => { CurrentView = SimpleIoc.Default.GetInstance<OrdersVm>(); GetConnectionStatus(); });
            BtnPackagesView = new RelayCommand(() => { CurrentView = SimpleIoc.Default.GetInstance<PackagesVm>(); GetConnectionStatus(); });
            BtnIngredientsView = new RelayCommand(() => { CurrentView = SimpleIoc.Default.GetInstance<IngredientsVm>(); GetConnectionStatus(); });
            BtnCommentsView = new RelayCommand(() => { CurrentView = SimpleIoc.Default.GetInstance<CommentsVm>(); GetConnectionStatus(); });
            BtnCreationsView = new RelayCommand(() => { CurrentView = SimpleIoc.Default.GetInstance<CreationsVm>(); GetConnectionStatus(); });

            StartSynchronizer();
        }

        private void InitializeVms()
        {
            SimpleIoc.Default.GetInstance<OrdersVm>().DataAgent = dataAgent;
            SimpleIoc.Default.GetInstance<PackagesVm>().DataAgent = dataAgent;
            SimpleIoc.Default.GetInstance<CreationsVm>().DataAgent = dataAgent;
            SimpleIoc.Default.GetInstance<IngredientsVm>().DataAgent = dataAgent;
            SimpleIoc.Default.GetInstance<CommentsVm>().DataAgent = dataAgent;

            SimpleIoc.Default.GetInstance<OrdersVm>().InitializeVm();
            SimpleIoc.Default.GetInstance<PackagesVm>().InitializeVm();
            SimpleIoc.Default.GetInstance<CreationsVm>().InitializeVm();
            SimpleIoc.Default.GetInstance<IngredientsVm>().InitializeVm();
            SimpleIoc.Default.GetInstance<CommentsVm>().InitializeVm();
        }
        private void StartSynchronizer()
        {
            localSync = new LocalSynchronizer(
                SimpleIoc.Default.GetInstance<OrdersVm>().OrdersSynchronized,
                 SimpleIoc.Default.GetInstance<PackagesVm>().PackagesSynchronized,
                  SimpleIoc.Default.GetInstance<CreationsVm>().CreationsSynchronized,
                   SimpleIoc.Default.GetInstance<IngredientsVm>().IngredientsSynchronized,
                    SimpleIoc.Default.GetInstance<CommentsVm>().CommentsSynchronized);
            localSync.StartSyncing();
        }
        private void GetConnectionStatus()
        {
            ConnectStatus = dataAgent.GetSynchronizerStatus();
        }
    }
}