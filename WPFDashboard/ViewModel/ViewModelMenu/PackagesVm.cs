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


namespace WPFDashboard.ViewModel.ViewModelMenu
{
    public class PackagesVm : ViewModelBase, ISynchronizable
    {
        
        #region FIELDS
        private ViewModelBase packageDetailsView;
        private ObservableCollection<Package> listPackages;
        private Package selectedPackage;
        #endregion


        #region PROPERTIES
        public Package SelectedPackage
        {
            get { return selectedPackage; }
            set { selectedPackage = value;
                if (SelectedPackage != null)
                    ShowPackageDetails(value);
                RaisePropertyChanged();

                RaisePropertyChanged();
            }
        }

        public ViewModelBase PackageDetailsView
        {
            get { return packageDetailsView; }
            set { packageDetailsView = value;
                RaisePropertyChanged();
            }
        }

       
        public ObservableCollection<Package> ListPackages
        {
            get { return listPackages; }
            set { listPackages = value;
                RaisePropertyChanged();
            }
        }

        #endregion
        
        public PackagesVm()
        {
            InitializePackageList();
            Messenger.Default.Register<RefreshMessage>(this, Refresh);
        }

        private void ShowPackageDetails(Package p)
        {
            Messenger.Default.Send(p);
            PackageDetailsView = SimpleIoc.Default.GetInstance<PackageDetailsVm>();
            RaisePropertyChanged("PackageDetailsView");
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
            ListPackages = new ObservableCollection<Package>(DataAgentUnit.GetInstance().QueryPackagesWithChocolatesAndIngredients());
           
        }

    }
}
