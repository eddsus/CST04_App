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
using WPFDashboard.ViewModel.DetailViewModels;
using WPFDashboard.ViewModel.ViewModelMenu.DummyVm;

namespace WPFDashboard.ViewModel.ViewModelMenu
{
    public class PackagesVm : ViewModelBase, ISynchronizable
    {
        //todo: Generate regions for fields and properties
        #region Properties
        private ViewModelBase packageDetailsView;

        public ViewModelBase PackageDetailsView
        {
            get { return packageDetailsView; }
            set { packageDetailsView = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand BtnPackageDetails { get; set; }

        public ObservableCollection<PackageItemVm> ListPackages { get; set; }

        #endregion
        //testing shapes

        private ObservableCollection<Shape> shapes;

        public ObservableCollection<Shape> Shapes
        {
            get { return shapes; }
            set
            {
                shapes = value;
                RaisePropertyChanged();
            }
        }


        public PackagesVm()
        {
            InitializePackageList();
            Messenger.Default.Register<RefreshMessage>(this, Refresh);
            BtnPackageDetails = new RelayCommand(()=> { ShowPackageDetails(); });
        }

        private void ShowPackageDetails()
        {
            PackageDetailsView = SimpleIoc.Default.GetInstance<PackageDetailsVm>();
        }

        private void Refresh(RefreshMessage obj)
        {
            if (GetType() == obj.View)
            {
                InitializePackageList();
            }
        }

        public void ViewSynchronized()
        {
            InitializePackageList();
        }

        private void InitializePackageList()
        {
            // Shapes = new ObservableCollection<Shape>();

            //foreach (var item in DataAgent.QueryShapes())
            //{
            //    Shapes.Add(item);
            //}

            ListPackages = new ObservableCollection<PackageItemVm>();
            ListPackages.Add(new PackageItemVm("Xmas box","Kitchen","In Work",5));
            ListPackages.Add(new PackageItemVm("Easter package", "Kitchen", "In Work", 4));
            ListPackages.Add(new PackageItemVm("Birthday box", "Kitchen", "In Work", 3));
            ListPackages.Add(new PackageItemVm("Birthday Box", "Maria don Buenos", "In Work", 5));
            ListPackages.Add(new PackageItemVm("Celebration mix", "Kitchen", "In Work", 2));
        }

    }
}
