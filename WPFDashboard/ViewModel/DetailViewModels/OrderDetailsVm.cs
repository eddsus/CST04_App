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
using WPFDashboard.ViewModel.DetailViewModels;
using WPFDashboard.ViewModel.ViewModelMenu;

namespace WPFDashboard.ViewModel.OrderVModels
{

    public class OrderDetailsVm : ViewModelBase

    {
        #region Properties

        private ViewModelBase currentDetail;

        public ViewModelBase CurrentDetail
        {
            get { return currentDetail; }
            set
            {
                currentDetail = value;
                RaisePropertyChanged();
            }
        }

        //::TODO:: Bind to xaml
        public ObservableCollection<OrderStatus> OrderStateSelection { get; set; }
        public ObservableCollection<OrderContent> OrderContentDetailsList { get; set; }
        public RelayCommand<OrderContent> BtnDelete { get; set; }
        public RelayCommand<OrderContent> BtnDetails { get; set; }
        private Order currentOrder;

        public Order CurrentOrder
        {
            get { return currentOrder; }
            set { currentOrder = value;
                RaisePropertyChanged();
            }
        }


        #endregion

        public OrderDetailsVm()
        {
            //PackageDetail = SimpleIoc.Default.GetInstance<PackageDetailVm>();


            Messenger.Default.Register<Order>(this, DisplayOrderInfo);

            //OrderStateSelection = new ObservableCollection<OrderStatus>(DataAgentUnit.GetInstance().QueryOrderStates());
            //InitPackage();
            //InitCreation();

            BtnDelete = new RelayCommand<OrderContent>((p) => { DeleteItem(p); });
            BtnDetails = new RelayCommand<OrderContent>((p) => { ShowItemDetails(p); });
        }

        

        private void DisplayOrderInfo(Order currentOrder)
        {
            CurrentOrder = currentOrder;
            RaisePropertyChanged("CurrentOrder");
           // OrderContentDetailsList = new ObservableCollection<OrderContent>(CurrentOrder.Content);
        }

        private void DeleteItem(OrderContent p)
        {
            //::TODO::also inform localdb and serverdb
            OrderContentDetailsList.Remove(p);
        }

        private void ShowItemDetails(OrderContent p)
        {
            if (p.GetType().ToString().Equals("Package"))
            {
                CurrentDetail = SimpleIoc.Default.GetInstance<PackageDetailsVm>();
            }
            else
            {
                CurrentDetail = SimpleIoc.Default.GetInstance<CreationDetailsVm>();
            }
        }
    }
}
