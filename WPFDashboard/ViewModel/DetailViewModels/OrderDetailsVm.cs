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
        private RelayCommand<OrderContentChocolate> btnDeleteChocolate;
        private RelayCommand<OrderContentPackage> btnDeletePackage;
        private string typeChocolate;
        private string typePackage;
        private string contentID;
        private List<OrderContentChocolate> deletedOrderContentChocolates;
        private List<OrderContentPackage> deletedOrderContentPackages;
        private string kitchenNote;

        #endregion


        #region Properties
        private string customerNote;

        public string CustomerNote
        {
            get { return customerNote; }
            set { customerNote = value; RaisePropertyChanged(); }
        }

        public string KitchenNote
        {
            get { return kitchenNote; }
            set { kitchenNote = value; RaisePropertyChanged(); }
        }

        public string ContentID
        {
            get { return contentID; }
            set
            {
                contentID = value;
                RaisePropertyChanged();
            }
        }


        public string TypePackage
        {
            get { return typePackage; }
            set
            {
                typePackage = value;
                RaisePropertyChanged();
            }
        }


        public string TypeChocolate
        {
            get { return typeChocolate; }
            set
            {
                typeChocolate = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand<OrderContentPackage> BtnDeletePackage
        {
            get { return btnDeletePackage; }
            set
            {
                btnDeletePackage = value;
                RaisePropertyChanged();
            }
        }


        public RelayCommand<OrderContentChocolate> BtnDeleteChocolate
        {
            get { return btnDeleteChocolate; }
            set
            {
                btnDeleteChocolate = value;
                RaisePropertyChanged();
            }
        }

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
            KitchenNote = "";
            Messenger.Default.Register<Order>(this, DisplayOrderInfo);
            SaveBtn = new RelayCommand(SaveOrderDetails, () => KitchenNote.Equals("") ? false : (SimpleIoc.Default.GetInstance<MainViewModel>().ConnectStatus) ? true : false);
            InitOrderStates();
        }

        private void SaveOrderDetails()
        {
            if (deletedOrderContentChocolates != null && deletedOrderContentChocolates.Count > 0)
            {
                TypeChocolate = "0";
                foreach (var item in deletedOrderContentChocolates)
                {
                    ContentID = item.OrderContentId.ToString();
                    DataAgentUnit.GetInstance().DeleteOrderContent<OrderContent>(ContentID, TypeChocolate);
                }
                deletedOrderContentChocolates.Clear();
            }
            if (deletedOrderContentPackages != null && deletedOrderContentPackages.Count > 0)
            {
                TypePackage = "1";
                foreach (var item in deletedOrderContentPackages)
                {
                    ContentID = item.OrderContentId.ToString();
                    DataAgentUnit.GetInstance().DeleteOrderContent<OrderContent>(ContentID, TypePackage);
                }
                deletedOrderContentPackages.Clear();
            }
            DataAgentUnit.GetInstance().UpdateOrder(new Order()
            {
                OrderId = CurrentOrder.OrderId,
                Customer = CurrentOrder.Customer,
                DateOfDelivery = CurrentOrder.DateOfDelivery,
                DateOfOrder = CurrentOrder.DateOfOrder,
                Note = "CN: " + CustomerNote + "; KN: " + KitchenNote,
                Status = GetStatusObjectfromString(SelectedOrderState)
            });
            Messenger.Default.Send<string>("Order saved @ " + DateTime.Now);
        }

        private void DeletePackageFromList(OrderContentPackage p)
        {
            OrderContentPackages.Remove(p);
            if (deletedOrderContentPackages == null)
            {
                deletedOrderContentPackages = new List<OrderContentPackage>();
            }
            deletedOrderContentPackages.Add(p);
        }

        private void DeleteChocolateFromList(OrderContentChocolate p)
        {
            OrderContentChocolates.Remove(p);
            if (deletedOrderContentChocolates == null)
            {
                deletedOrderContentChocolates = new List<OrderContentChocolate>();
            }
            deletedOrderContentChocolates.Add(p);
        }

        private void DisplayOrderInfo(Order currentOrder)
        {
            CurrentOrder = currentOrder;
            FillOrderContent();
            SelectedOrderState = CurrentOrder.Status.Decription;
            CustomerNote = CurrentOrder.Note;
            KitchenNote = "";
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
        private void InitOrderStates()
        {
            OrderStateStrings = new ObservableCollection<string>();
            foreach (var item in DataAgentUnit.GetInstance().QueryOrderStates())
            {
                if (!item.Decription.Equals("New"))
                    OrderStateStrings.Add(item.Decription);
            }

            BtnDeleteChocolate = new RelayCommand<OrderContentChocolate>((p) => { DeleteChocolateFromList(p); });
            BtnDeletePackage = new RelayCommand<OrderContentPackage>((p) => { DeletePackageFromList(p); });
        }
        private OrderStatus GetStatusObjectfromString(string selectedOrderState)
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

    }
}
