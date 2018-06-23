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
using WPFDashboard.Helpers.MessengerWrappers;
using WPFDashboard.ViewModel.DetailViewModels;


namespace WPFDashboard.ViewModel.ViewModelMenu
{
    public class OrdersVm:ViewModelBase, ISynchronizable
    {
        #region FIELDS
        private ViewModelBase orderDetailsView;
        private ObservableCollection<Order> ordersList;
        private Order selectedOrder;
        #endregion

        #region PROPERTIES
        public Order SelectedOrder
        {
            get { return selectedOrder; }
            set { selectedOrder = value;
                if (SelectedOrder != null)
                    ShowOrderDetails(value);
                RaisePropertyChanged();
               
                RaisePropertyChanged();
            }
        }
        
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
        #endregion

        public OrdersVm()
        {
            InitializeOrdersList();
            Messenger.Default.Register<RefreshMessage>(this, Refresh);
            Messenger.Default.Register<IngredientDeactivatedMessage>(this, IngredientDeactivated);

        }

        private void IngredientDeactivated(IngredientDeactivatedMessage ing)
        {
            foreach (var order in OrdersList)
            {
                foreach (var orderContent in order.Content)
                {
                    if (orderContent is OrderContentChocolate)
                    {
                        OrderContentChocolate occ = (OrderContentChocolate)orderContent;
                        foreach (var item in occ.Chocolate.Ingredients)
                        {
                            if (item.IngredientId == ing.DeactivatedIngredient.IngredientId)
                            {
                                order.Status.OrderStatusId = new Guid("e9ea67d5-bee2-4372-9abb-408a2afe3640");
                                DataAgentUnit.GetInstance().UpdateOrder(order);
                                RaisePropertyChanged("OrderList");
                                Messenger.Default.Send("Orders have been paused because " + item.Name + "was deactivated. Check the orders view to take action.");
                            } 
                        } 

                    } else
                    {
                        OrderContentPackage ocp = (OrderContentPackage)orderContent;
                        foreach (var choco in ocp.Package.Chocolates)
                        {
                            foreach (var item in choco.Ingredients)
                            {
                                if (item.IngredientId == ing.DeactivatedIngredient.IngredientId)
                                {
                                    order.Status.OrderStatusId = new Guid("e9ea67d5-bee2-4372-9abb-408a2afe3640");
                                    DataAgentUnit.GetInstance().UpdateOrder(order);
                                    RaisePropertyChanged("OrderList");
                                    Messenger.Default.Send("Orders have been paused because " + item.Name + "was deactivated. Check the orders view to take action.");
                                }
                            }
                        }
                    }
                }
            }
            Refresh(new RefreshMessage(GetType()));
        }

        private void ShowOrderDetails(Order p)
        {
            Messenger.Default.Send(new Order(p));
            OrderDetailsView = SimpleIoc.Default.GetInstance<OrderDetailsVm>();
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
            var temp = DataAgentUnit.GetInstance().QueryOrders();
            temp = temp.Where(x => !x.Status.Decription.Equals("Completed") && !x.Status.Decription.Equals("Canceled")).ToList();
           OrdersList = new ObservableCollection<Order>(temp.OrderBy(x => x.Status.Decription).ToList());
        }
    }
}
