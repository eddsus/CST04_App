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

        private string selectedOrderState;

        public string SelectedOrderState
        {
            get { return selectedOrderState; }
            set { selectedOrderState = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<string> OrderStateStrings { get; set; }

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

            OrderStateStrings = new ObservableCollection<string>();
            foreach (var item in DataAgentUnit.GetInstance().QueryOrderStates())
            {
                OrderStateStrings.Add(item.Decription);
            }

            //InitPackage();
            //InitCreation();

            BtnDelete = new RelayCommand<OrderContent>((p) => { DeleteItem(p); });
            BtnDetails = new RelayCommand<OrderContent>((p) => { ShowItemDetails(p); });
        }

        

        private void DisplayOrderInfo(Order currentOrder)
        {
            CurrentOrder = currentOrder;
            SelectedOrderState = CurrentOrder.Status.Decription;
            RaisePropertyChanged("CurrentOrder");
            RaisePropertyChanged("SelectedOrderState");
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
