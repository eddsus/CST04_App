using System;
using DataAgent;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using LocalSynchronization;
using SharedDataTypes;
using WPFDashboard.ViewModel.ViewModelMenu;
using WPFDashboard.Helpers;
using System.Threading.Tasks;
using System.Threading;

namespace WPFDashboard.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        #region FIELDS
        private LocalSynchronizer localSync;
        private ViewModelBase currentView;
        private bool connectStatus;
        private string infoMessage;
        #endregion

        #region PROPERTIES

        public string InfoMessage
        {
            get { return infoMessage; }
            set
            {
                infoMessage = value;
                RaisePropertyChanged();
            }
        }


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
            BtnOrdersView = new RelayCommand(
                () => {
                CurrentView = SimpleIoc.Default.GetInstance<OrdersVm>();
                    GetConnectionStatus(); }
               );
            BtnPackagesView = new RelayCommand(() => { CurrentView = SimpleIoc.Default.GetInstance<PackagesVm>(); GetConnectionStatus(); });
            BtnIngredientsView = new RelayCommand(() => { CurrentView = SimpleIoc.Default.GetInstance<IngredientsVm>(); GetConnectionStatus(); });
            BtnCommentsView = new RelayCommand(() => { CurrentView = SimpleIoc.Default.GetInstance<CommentsVm>(); GetConnectionStatus(); });
            BtnCreationsView = new RelayCommand(() => { CurrentView = SimpleIoc.Default.GetInstance<CreationsVm>(); GetConnectionStatus(); });

            BtnRefresh = new RelayCommand(() =>
            {
                RefreshCurrentView();
            });
            Messenger.Default.Register<PropertyChanged<Ingredient>>(this, DisplayInformation);
            Messenger.Default.Register<string>(this, DisplayInformation);

            StartSynchronizer();
        }

        private void RefreshCurrentView()
        {
            Messenger.Default.Send(new RefreshMessage(CurrentView.GetType()));
        }

        private void SetConnectionStatus(bool msg)
        {
            ConnectStatus = msg;
            RefreshCurrentView();
        }

        private void DisplayInformation(PropertyChanged<Ingredient> message)
        {
            InfoMessage = String.Format("{0} {1}", message.ChangedProperty.Name, message.Message);
        }

        private void DisplayInformation(string message)
        {
            RefreshCurrentView();
            InfoMessage = message;
        }


        private void StartSynchronizer()
        {
             localSync =  new LocalSynchronizer(
                SimpleIoc.Default.GetInstance<OrdersVm>().ViewSynchronized,
                 SimpleIoc.Default.GetInstance<PackagesVm>().ViewSynchronized,
                  SimpleIoc.Default.GetInstance<CreationsVm>().ViewSynchronized,
                   SimpleIoc.Default.GetInstance<IngredientsVm>().ViewSynchronized,
                    SimpleIoc.Default.GetInstance<CommentsVm>().ViewSynchronized,
                    DisplayInformation, SetConnectionStatus);
            localSync.StartSyncing();
        }
        private bool GetConnectionStatus()
        {
            return ConnectStatus = DataAgentUnit.GetInstance().GetSynchronizerStatus();
        }
    }
}