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
        private RelayCommand<Package> btnPublish;

        public RelayCommand<Package> BtnPublish
        {
            get { return btnPublish; }
            set { btnPublish = value;
                RaisePropertyChanged();
            }
        }

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
            BtnPublish = new RelayCommand<Package>(
                 (i) =>
                 {
                     if (i.Available)
                     {
                         SelectedPackage = i;
                         SelectedPackage.Available = false;
                         //Update the databases
                         DataAgentUnit.GetInstance().UpdatePackage(i);
                         ShowPackageDetails(i);
                         //and inform the infobar
                         Messenger.Default.Send(new PropertyChanged<Package>(i, "has been deactivated", 5));
                         //IngredientList.Where(j => j.IngredientId == i.IngredientId).Select(k => k).First().Available = false;
                         Refresh(new RefreshMessage(GetType()));
                     }
                     else
                     {
                         SelectedPackage = i;
                         SelectedPackage.Available = true;
                         //Update the databases
                         DataAgentUnit.GetInstance().UpdatePackage(i);
                         ShowPackageDetails(i);
                         //and inform the infobar
                         Messenger.Default.Send(new PropertyChanged<Package>(i, "has been activated", 5));
                         //IngredientList.Where(j => j.IngredientId == i.IngredientId).Select(k => k).First().Available = true;
                         Refresh(new RefreshMessage(GetType()));
                     }
                 },
                (i) =>
                {
                    //only allow updates when connected and an ingredient is selected
                    return SimpleIoc.Default.GetInstance<MainViewModel>().ConnectStatus;
                });
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
