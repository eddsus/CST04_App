using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDashboard.ViewModel.OrderVModels;

namespace WPFDashboard.ViewModel.ViewModelMenu
{
    public class OrdersVm:ViewModelBase
    {
        public RelayCommand BtnShowdetails { get; set; }
        private ViewModelBase orderDetailsView;

        public ViewModelBase OrderDetailsView
        {
            get { return orderDetailsView; }
            set { orderDetailsView = value;
                RaisePropertyChanged();
            }
        }
        public OrdersVm()
        {
            BtnShowdetails = new RelayCommand(()=> {OrderDetailsView = SimpleIoc.Default.GetInstance<OrderDetailsVm>(); });
        }



    }
}
