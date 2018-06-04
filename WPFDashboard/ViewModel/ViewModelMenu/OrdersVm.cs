using DataAgent;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using SharedDataTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDashboard.ViewModel.DetailViewModels;
using WPFDashboard.ViewModel.OrderVModels;

namespace WPFDashboard.ViewModel.ViewModelMenu
{
    public class OrdersVm:ViewModelBase
    {
        public DataAgentUnit DataAgent { get; set; }
        public RelayCommand BtnShowdetails { get; set; }
        private ViewModelBase orderDetailsView;

        public ViewModelBase OrderDetailsView
        {
            get { return orderDetailsView; }
            set { orderDetailsView = value;
                RaisePropertyChanged();
            }
        }
        private ObservableCollection<Order> ordersList;

        public ObservableCollection<Order> OrdersList
        {
            get { return ordersList; }
            set { ordersList = value;
                RaisePropertyChanged();
            }
        }
        private RelayCommand<Order> btnviewDetails;

        public RelayCommand<Order> BtnViewDetails
        {
            get { return btnviewDetails; }
            set {
                btnviewDetails = value;
                RaisePropertyChanged();
            }
        }

        public OrdersVm()
        {
            //LEAVE ME EMPTY AND USE initializevm instead!!!
        }

        public void InitializeVm()
        {
            BtnShowdetails = new RelayCommand(() => { OrderDetailsView = SimpleIoc.Default.GetInstance<OrderDetailsVm>(); });
            OrdersList = new ObservableCollection<Order>(DataAgent.QueryOrders());
        }

        public void OrdersSynchronized()
        {
            OrdersList = new ObservableCollection<Order>(DataAgent.QueryOrders());
        }
    }
}
