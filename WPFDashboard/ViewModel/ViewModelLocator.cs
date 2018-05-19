/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:WPFDashboard"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using WPFDashboard.ViewModel.OrderVModels;
using WPFDashboard.ViewModel.ViewModelMenu;

namespace WPFDashboard.ViewModel
{
   
    public class ViewModelLocator
    {
      
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

         

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<OrdersVm>();
            SimpleIoc.Default.Register<CommentsVm>();
            SimpleIoc.Default.Register<CreationsVm>();
            SimpleIoc.Default.Register<PackagesVm>();
            SimpleIoc.Default.Register<IngredientsVm>();

            //Order has inner Views
            //Make a views for order page
            SimpleIoc.Default.Register<OrderDetailsVm>();

        }

       //Order Page View
        public OrderDetailsVm OrderDetails
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OrderDetailsVm>();
            }
        }

        //Main Page Views
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public OrdersVm Orders
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OrdersVm>();
            }
        }
        public CommentsVm Comments
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CommentsVm>();
            }
        }

        public CreationsVm Creations
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CreationsVm>();
            }
        }

        public PackagesVm Packages
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PackagesVm>();
            }
        }

        public IngredientsVm Ingredients
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IngredientsVm>();
            }
        }
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}