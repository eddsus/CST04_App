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
using WPFDashboard.ViewModel.ViewModelMenu;

namespace WPFDashboard.ViewModel.DetailViewModels
{
    public class OrderDetailsVm : ViewModelBase
    {
        #region FIELDS
        private ViewModelBase currentDetail;
        private ObservableCollection<OrderContentChocolate> orderContentChocolates;
        private string selectedOrderState;
        private ObservableCollection<OrderContentPackage> orderContentPackages;
        private Order currentOrder;
        private OrderContentChocolate currentOrderContentChocolate;
        private OrderContentPackage currentOrderContentPackage;
        #endregion


        #region Properties
        public RelayCommand SaveBtn { get; set; }
        public OrderContentPackage CurrentOrderContentPackage
        {
            get { return currentOrderContentPackage; }
            set
            {
                currentOrderContentPackage = value;
                if (CurrentOrderContentPackage != null)
                    ShowPackageDetails(value);
                RaisePropertyChanged();

                RaisePropertyChanged();
            }
        }


        public ViewModelBase CurrentDetail
        {
            get { return currentDetail; }
            set
            {
                currentDetail = value;
                RaisePropertyChanged();
            }
        }

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

        public ObservableCollection<OrderContentChocolate> OrderContentChocolates
        {
            get { return orderContentChocolates; }
            set
            {
                orderContentChocolates = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<OrderContentPackage> OrderContentPackages
        {
            get { return orderContentPackages; }
            set
            {
                orderContentPackages = value;
                RaisePropertyChanged();
            }
        }


        public RelayCommand<OrderContent> BtnDelete { get; set; }


        public Order CurrentOrder
        {
            get { return currentOrder; }
            set
            {
                currentOrder = value;
                RaisePropertyChanged();
            }
        }


        public OrderContentChocolate CurrentOrderContentChocolate
        {
            get { return currentOrderContentChocolate; }
            set
            {
                currentOrderContentChocolate = value;
                if (CurrentOrderContentChocolate != null)
                    ShowChocolateDetails(value);
                RaisePropertyChanged();

                RaisePropertyChanged();
            }
        }



        #endregion

        public OrderDetailsVm()
        {

            Messenger.Default.Register<Order>(this, DisplayOrderInfo);
            Messenger.Default.Register<RefreshMessage>(this, Refresh);
            SaveBtn = new RelayCommand(SaveOrderDetails);
            InitOrderStates();
        }

        private void SaveOrderDetails()
        {
            DataAgentUnit.GetInstance().UpdateOrder(new Order()
            {
                OrderId = CurrentOrder.OrderId,
                Customer = CurrentOrder.Customer,
                DateOfDelivery = CurrentOrder.DateOfDelivery,
                DateOfOrder = CurrentOrder.DateOfOrder,
                Note = CurrentOrder.Note,
                Status = getStatusObjectfromString(SelectedOrderState)
            });
            Refresh(new RefreshMessage(GetType()));
        }

        private OrderStatus getStatusObjectfromString(string selectedOrderState)
        {
            switch (selectedOrderState)
            {
                //case "InProgress":
                //    return new OrderStatus()
                //    {
                //        OrderStatusId = new Guid("1d7ed2c9-e273-49e7-b7fd-b014f71a8a40"),
                //        Decription = "InProgress"
                //    };
                case "Delayed":
                    return new OrderStatus()
                    {
                        OrderStatusId = new Guid("83d176ef-0c09-4fdb-9e8e-3f422bed7867"),
                        Decription = "Delayed"
                    };
                case "Paused":
                    return new OrderStatus()
                    {
                        OrderStatusId = new Guid("e9ea67d5-bee2-4372-9abb-408a2afe3640"),
                        Decription = "Paused"
                    };
                case "Completed":
                    return new OrderStatus()
                    {
                        OrderStatusId = new Guid("4f8b49f2-5ce8-45f2-bd17-51b96efffc10"),
                        Decription = "Completed"
                    };
                case "Canceled":
                    return new OrderStatus()
                    {
                        OrderStatusId = new Guid("4677b96e-d1a0-47bc-95db-52730c3d9985"),
                        Decription = "Canceled"
                    };
                default:
                    return new OrderStatus()
                    {
                        OrderStatusId = new Guid("1d7ed2c9-e273-49e7-b7fd-b014f71a8a40"),
                        Decription = "InProgress"
                    };
            }
        }

        private void InitOrderStates()
        {
            OrderStateStrings = new ObservableCollection<string>();
            foreach (var item in DataAgentUnit.GetInstance().QueryOrderStates())
            {
                if (!item.Decription.Equals("New"))
                    OrderStateStrings.Add(item.Decription);
            }
        }

        /// <summary>
        /// PLEASE COMPLETE REFRESH IMPLEMENTATION
        /// </summary>
        /// <param name="obj"></param>
        private void Refresh(RefreshMessage obj)
        {
            //throw new NotImplementedException();
        }

        private void DisplayOrderInfo(Order currentOrder)
        {
            CurrentOrder = currentOrder;
            FillOrderContent();
            SelectedOrderState = CurrentOrder.Status.Decription;
            RaisePropertyChanged("CurrentOrder");
            RaisePropertyChanged("SelectedOrderState");

        }

        private void FillOrderContent()
        {
            if (DataAgentUnit.GetInstance().QueryOrdersContentChocolate(CurrentOrder.OrderId) != null)
            {

                OrderContentChocolates = new ObservableCollection<OrderContentChocolate>(DataAgentUnit.GetInstance().QueryOrdersContentChocolate(CurrentOrder.OrderId));
                RaisePropertyChanged("OrderContentChocolates");
                // RaisePropertyChanged("CurrentDetail");
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
                //RaisePropertyChanged("CurrentDetail");
            }
            else
            {
                OrderContentPackages = new ObservableCollection<OrderContentPackage>();
                RaisePropertyChanged("OrderContentPackages");

            }

            BtnDelete = new RelayCommand<OrderContent>((p) => { DeleteItem(p); });

        }

        /// <summary>
        /// NOT COMPLETED DELETE IMPLEMENTATION
        /// </summary>
        /// <param name="p"></param>
        private void DeleteItem(OrderContent p)
        {
            //::TODO::also inform localdb and serverdb
            //OrderContentDetailsList.Remove(p);
        }



        private void ShowPackageDetails(OrderContentPackage p)
        {
            Messenger.Default.Send(p.Package);
            CurrentDetail = SimpleIoc.Default.GetInstance<PackageDetailsVm>();
            RaisePropertyChanged("CurrentDetail");
        }

        private void ShowChocolateDetails(OrderContentChocolate p)
        {
            Messenger.Default.Send(p.Chocolate);
            CurrentDetail = SimpleIoc.Default.GetInstance<CreationDetailsVm>();
            RaisePropertyChanged("CurrentDetail");
        }

    }
}
