using DataAgent;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using SharedDataTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDashboard.Helpers;
using WPFDashboard.ViewModel.DetailViewModels;


namespace WPFDashboard.ViewModel.ViewModelMenu
{
    public class OrdersVm:ViewModelBase, ISynchronizable
    {
        #region FIELDS
        private ViewModelBase orderDetailsView;
        private ObservableCollection<Order> ordersList;
        private RelayCommand<Order> btnviewDetails;
        #endregion

        #region PROPERTIES
        public RelayCommand<Order> BtnShowdetails { get; set; }
        public ViewModelBase OrderDetailsView
        {
            get { return orderDetailsView; }
            set { orderDetailsView = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Order> OrdersList
        {
            get { return ordersList; }
            set { ordersList = value;
                RaisePropertyChanged();
            }
        }
        public RelayCommand<Order> BtnViewDetails
        {
            get { return btnviewDetails; }
            set
            {
                btnviewDetails = value;
                RaisePropertyChanged();
            }
        }

        private Order currentItem;

        public Order CurrentItem
        {
            get { return currentItem; }
            set { currentItem = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        public OrdersVm()
        {
            BtnShowdetails = new RelayCommand<Order>((p) =>
            {
                Messenger.Default.Send(new Order(p));
                OrderDetailsView = SimpleIoc.Default.GetInstance<OrderDetailsVm>();
            });

            InitializeOrdersList();
            Messenger.Default.Register<RefreshMessage>(this, Refresh);

        }

        private void Refresh(RefreshMessage obj)
        {
            if (GetType() == obj.View)
            {
                InitializeOrdersList();
            }
        }
        public void ViewSynchronized()
        {
            InitializeOrdersList();
        }
        private void InitializeOrdersList()
        {
            OrdersList = new ObservableCollection<Order>(DataAgentUnit.GetInstance().QueryOrders());
        }
    }
}
