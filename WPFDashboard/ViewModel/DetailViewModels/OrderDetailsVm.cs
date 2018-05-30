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

namespace WPFDashboard.ViewModel.OrderVModels
{

    public class OrderDetailsVm:ViewModelBase

    {
        private ViewModelBase packageDetail;

        public ViewModelBase PackageDetail
        {
            get { return packageDetail; }
            set { packageDetail = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<TestVm> OrderContents { get; set; }
        public RelayCommand<TestVm> BtnDelete { get; set; }
        public RelayCommand BtnDetails { get; set; }
        public OrderDetailsVm()
        {
            //PackageDetail = SimpleIoc.Default.GetInstance<PackageDetailVm>();
            OrderContents = new ObservableCollection<TestVm>();
            OrderContents.Add(new TestVm(5,"Chokos"));
            OrderContents.Add(new TestVm(10, "Nice packages"));
            BtnDelete = new RelayCommand<TestVm>((p)=> { DeleteItem(p); });
            BtnDetails = new RelayCommand(()=> { PackageDetail = SimpleIoc.Default.GetInstance<PackageDetailVm>(); });
        }

       

        private void DeleteItem(TestVm item)
        {
            OrderContents.Remove(item);
        }
    }
}
