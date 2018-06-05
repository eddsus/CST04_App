using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDashboard.ViewModel.DetailViewModels
{
    public class PackageDetailsVm:ViewModelBase
    {
        public ObservableCollection<ChocolateTestVm> Chocolates { get; set; }
        public PackageDetailsVm()
        {
            Chocolates = new ObservableCollection<ChocolateTestVm>();
            Chocolates.Add(new ChocolateTestVm("My custom Choco", 6));
            Chocolates.Add(new ChocolateTestVm("Toblerone", 2));
            Chocolates.Add(new ChocolateTestVm("Milka", 4));
            Chocolates.Add(new ChocolateTestVm("Lindt", 1));
        }
    }
}
