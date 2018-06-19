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
        public ObservableCollection<Chocolate> Chocolates { get; set; }

        public PackageDetailsVm()
        {
            
            InitChocolates();
            BtnShowDetails = new RelayCommand(()=> {CurrentChocolateView = SimpleIoc.Default.GetInstance<CreationDetailsVm>(); });
            
        }

        private void InitChocolates()
        {
            Chocolates = new ObservableCollection<Chocolate>();
           
        }
    }
}
