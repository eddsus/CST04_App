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

namespace WPFDashboard.ViewModel.DetailViewModels
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
            set
            {
                selectedOrderState = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<string> OrderStateStrings { get; set; }

        //public ObservableCollection<OrderContentChocolate> OrderContentChocolates { get; set; }

        private ObservableCollection<OrderContentChocolate> orderContentChocolates;

        public ObservableCollection<OrderContentChocolate> OrderContentChocolates
        {
            get { return orderContentChocolates; }
            set { orderContentChocolates = value;
                RaisePropertyChanged();
            }
        }


        //public ObservableCollection<OrderContentPackage> OrderContentPackages { get; set; }

        private ObservableCollection<OrderContentPackage> orderContentPackages;

        public ObservableCollection<OrderContentPackage> OrderContentPackages
        {
            get { return orderContentPackages; }
            set { orderContentPackages = value;
                RaisePropertyChanged();
            }
        }


        public RelayCommand<OrderContent> BtnDelete { get; set; }


        public RelayCommand<OrderContentChocolate> BtnDetailsChocolate { get; set; }

        public RelayCommand<OrderContentPackage> BtnDetailsPackage { get; set; }



        private Order currentOrder;

        public Order CurrentOrder
        {
            get { return currentOrder; }
            set
            {
                currentOrder = value;
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


            BtnDelete = new RelayCommand<OrderContent>((p) => { DeleteItem(p); });
            BtnDetailsChocolate = new RelayCommand<OrderContentChocolate>((p) => { ShowChocolateDetails(p); });
            BtnDetailsPackage = new RelayCommand<OrderContentPackage>((p) => { ShowPackageDetails(p); });
        }



        private void DisplayOrderInfo(Order currentOrder)
        {
            CurrentOrder = currentOrder;
            FillOrderContent();
            SelectedOrderState = CurrentOrder.Status.Decription;
            RaisePropertyChanged("CurrentOrder");
            RaisePropertyChanged("SelectedOrderState");
            // OrderContentDetailsList = new ObservableCollection<OrderContent>(CurrentOrder.Content);
        }

        private void FillOrderContent()
        {
            if (DataAgentUnit.GetInstance().QueryOrdersContentChocolate(CurrentOrder.OrderId) != null)
            {

                OrderContentChocolates = new ObservableCollection<OrderContentChocolate>(DataAgentUnit.GetInstance().QueryOrdersContentChocolate(CurrentOrder.OrderId));
                RaisePropertyChanged("OrderContentChocolates");
            }
            else
            {
                OrderContentChocolates = new ObservableCollection<OrderContentChocolate>();
                RaisePropertyChanged("OrderContentChocolates");
            }

            if (DataAgentUnit.GetInstance().QueryOrdersContentPackage(CurrentOrder.OrderId) != null)
            {
                OrderContentPackages = new ObservableCollection<OrderContentPackage>(DataAgentUnit.GetInstance().QueryOrdersContentPackage(CurrentOrder.OrderId));
                RaisePropertyChanged("OrderContentPackages");
            }
            else
            {
                OrderContentPackages = new ObservableCollection<OrderContentPackage>();
                RaisePropertyChanged("OrderContentPackages");
            }
        }

        private void DeleteItem(OrderContent p)
        {
            //::TODO::also inform localdb and serverdb
            //OrderContentDetailsList.Remove(p);
        }

       

        private void ShowPackageDetails(OrderContentPackage p)
        {
            Messenger.Default.Send(p);
            CurrentDetail = SimpleIoc.Default.GetInstance<PackageDetailsVm>();
        }

        private void ShowChocolateDetails(OrderContentChocolate p)
        {
            Messenger.Default.Send(p);
            CurrentDetail = SimpleIoc.Default.GetInstance<CreationDetailsVm>();
        }

    }
}
