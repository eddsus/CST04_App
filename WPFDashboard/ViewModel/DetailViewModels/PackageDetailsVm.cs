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

namespace WPFDashboard.ViewModel.DetailViewModels
{
    public class PackageDetailsVm:ViewModelBase
    {

        #region FIELDS
        private ViewModelBase currentChocolateView;
        private Package currentPackageContent;
        private ObservableCollection<string> availabilityStates;
        private bool selectedOrderPackageState;
        private string currentState;
        private ObservableCollection<Chocolate> chocolates;
        private RelayCommand<Chocolate> btnShowChocolateDetails;
        private Chocolate selectedChocolate;
        #endregion


        #region PROPERTIES
        public Chocolate SelectedChocolate
        {
            get { return selectedChocolate; }
            set { selectedChocolate = value;
                if (SelectedChocolate != null)
                    ShowChocolateDetails(value);
                RaisePropertyChanged();

                RaisePropertyChanged();
            }
        }

        public RelayCommand<Chocolate> BtnShowChocolateDetails
        {
            get { return btnShowChocolateDetails; }
            set
            {
                btnShowChocolateDetails = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Chocolate> Chocolates
        {
            get { return chocolates; }
            set { chocolates = value;
                RaisePropertyChanged();
            }
        }

        public string CurrentState
        {
            get { return currentState; }
            set
            {
                currentState = value;
                RaisePropertyChanged();
            }
        }
        public bool SelectedOrderPackageState
        {
            get { return selectedOrderPackageState; }
            set
            {
                selectedOrderPackageState = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<string> AvailabilityStates
        {
            get { return availabilityStates; }
            set
            {
                availabilityStates = value;
                RaisePropertyChanged();
            }
        }

        public ViewModelBase CurrentChocolateView
        {
            get { return currentChocolateView; }
            set { currentChocolateView = value;
                RaisePropertyChanged();
            }
        }


        public RelayCommand BtnShowDetails { get; set; }
        

       

        public Package CurrentContentPackage
        {
            get { return currentPackageContent; }
            set { currentPackageContent = value;
                RaisePropertyChanged();
            }
        }


        #endregion




        public PackageDetailsVm()
        {

            Messenger.Default.Register<Package>(this, DisplayPackageInfo);
            //BtnShowDetails = new RelayCommand(()=> {CurrentChocolateView = SimpleIoc.Default.GetInstance<CreationDetailsVm>(); });
            BtnShowChocolateDetails = new RelayCommand<Chocolate>((p)=> { ShowChocolateDetails(p); });
            
        }

        private void ShowChocolateDetails(Chocolate p)
        {
            Messenger.Default.Send(p);
            CurrentChocolateView = SimpleIoc.Default.GetInstance<CreationDetailsVm>();
        }

        private void DisplayPackageInfo(Package currentPackadeContent)
        {
            CurrentContentPackage = currentPackadeContent;
            SelectedOrderPackageState = CurrentContentPackage.Available;
            DefineStates();
            RaisePropertyChanged("CurrentOrderPackage");
            RaisePropertyChanged("SelectedOrderPackageState");
            Chocolates = new ObservableCollection<Chocolate>(CurrentContentPackage.Chocolates);
        }

        private void DefineStates()
        {
            AvailabilityStates = new ObservableCollection<string>() { "Available", "Not Available" };
            if (SelectedOrderPackageState == true)
            {
                CurrentState = "Available";
            }
            else
            {
                CurrentState = "Not Available";
            }
        }
    }
}
