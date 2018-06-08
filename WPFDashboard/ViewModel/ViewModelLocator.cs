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

using DataAgent;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using WPFDashboard.ViewModel.DetailViewModels;
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
            SimpleIoc.Default.Register<OrdersVm>(true);
            SimpleIoc.Default.Register<CommentsVm>(true);
            SimpleIoc.Default.Register<CreationsVm>(true);
            SimpleIoc.Default.Register<PackagesVm>(true);
            SimpleIoc.Default.Register<IngredientsVm>(true);

            //Order has inner Views
            //Make a detail views for all pages
            SimpleIoc.Default.Register<OrderDetailsVm>();
            SimpleIoc.Default.Register<PackageDetailsVm>();
            SimpleIoc.Default.Register<CreationDetailsVm>();

        }

        //Creation Details View
        public CreationDetailsVm CreationDetail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CreationDetailsVm>();
            }
        }
        //Package Details Views
        public PackageDetailsVm PackageDetail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PackageDetailsVm>();
            }
        }

        //Order Details View
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