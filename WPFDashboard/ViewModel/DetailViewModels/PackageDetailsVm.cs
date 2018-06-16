using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
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
        private ViewModelBase currentChocolateView;

        public ViewModelBase CurrentChocolateView
        {
            get { return currentChocolateView; }
            set { currentChocolateView = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand BtnShowDetails { get; set; }
        public ObservableCollection<ChocolateTestVm> Chocolates { get; set; }

        public PackageDetailsVm()
        {
            
            InitChocolates();
            BtnShowDetails = new RelayCommand(()=> {CurrentChocolateView = SimpleIoc.Default.GetInstance<CreationDetailsVm>(); });
            
        }

        private void InitChocolates()
        {
            Chocolates = new ObservableCollection<ChocolateTestVm>();
            Chocolates.Add(new ChocolateTestVm("My custom Choco", 6));
            Chocolates.Add(new ChocolateTestVm("Toblerone", 2));
            Chocolates.Add(new ChocolateTestVm("Milka", 4));
            Chocolates.Add(new ChocolateTestVm("Lindt", 1));
        }
    }
}
