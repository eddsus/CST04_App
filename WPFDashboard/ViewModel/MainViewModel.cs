using DataAgent;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using WPFDashboard.ViewModel.ViewModelMenu;

namespace WPFDashboard.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        DataAgentUnit dataAgent = new DataAgentUnit();
        private ViewModelBase currentView;

        public ViewModelBase CurrentView
        {
            get { return currentView; }
            set { currentView = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand BtnOrdersView { get; set; }
        public RelayCommand BtnCommentsView { get; set; }
        public RelayCommand BtnCreationsView { get; set; }
        public RelayCommand BtnIngredientsView { get; set; }
        public RelayCommand BtnPackagesView { get; set; }

        private bool connectStatus;

        public bool ConnectStatus
        {
            get { return connectStatus; }
            set { connectStatus = value;
                RaisePropertyChanged();
            }
        }


        public MainViewModel()
        {
            GetConnectionStatus();
            CurrentView = SimpleIoc.Default.GetInstance<OrdersVm>();
            BtnOrdersView = new RelayCommand(() => { CurrentView = SimpleIoc.Default.GetInstance<OrdersVm>(); GetConnectionStatus(); });
            BtnPackagesView = new RelayCommand(() => { CurrentView = SimpleIoc.Default.GetInstance<PackagesVm>(); GetConnectionStatus(); });
            BtnIngredientsView = new RelayCommand(() => { CurrentView = SimpleIoc.Default.GetInstance<IngredientsVm>(); GetConnectionStatus(); });
            BtnCommentsView = new RelayCommand(() => { CurrentView = SimpleIoc.Default.GetInstance<CommentsVm>(); GetConnectionStatus(); });
            BtnCreationsView = new RelayCommand(() => { CurrentView = SimpleIoc.Default.GetInstance<CreationsVm>(); GetConnectionStatus(); });
        }

        private void GetConnectionStatus()
        {
            ConnectStatus = dataAgent.GetSynchronizerStatus();
        }
    }
}