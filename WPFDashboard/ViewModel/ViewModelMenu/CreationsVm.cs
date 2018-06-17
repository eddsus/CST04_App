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
    public class CreationsVm : ViewModelBase, ISynchronizable
    {
        //todo: Generate regions for fields and properties
        #region FIELDS
        private ViewModelBase creationDetailView;
        #endregion
        #region Properties
        public RelayCommand BtnCreationDetails { get; set; }

        public ViewModelBase CreationDetailView
        {
            get { return creationDetailView; }
            set { creationDetailView = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<string> States { get; set; }
        public ObservableCollection<Chocolate> ListOfChocolates { get; set; }
        public string CurrentState { get; set; }
        public Chocolate CurrentChocolate { get; set; }

        #endregion

        public CreationsVm()
        {
            InitializeChocolateList();
            InitStateList();
            Messenger.Default.Register<RefreshMessage>(this, Refresh);
            BtnCreationDetails = new RelayCommand(()=> { ShowCreationdetails(); });
        }

        private void InitStateList()
        {
            States = new ObservableCollection<string>() {};
        }

        private void ShowCreationdetails()
        {
            CreationDetailView = SimpleIoc.Default.GetInstance<CreationDetailsVm>();
        }

        private void Refresh(RefreshMessage obj)
        {
            if (GetType() == obj.View)
            {
                InitializeChocolateList();
            }
        }
        public void ViewSynchronized()
        {
            InitializeChocolateList();
        }


        private void InitializeChocolateList()
        {
            ListOfChocolates = new ObservableCollection<Chocolate>(DataAgentUnit.GetInstance().QueryCreations());
            //ListOfCreations.Add(new CreationItemVm("TestChoco1", "Max Mustermann", 3,"In Work"));
            //ListOfCreations.Add(new CreationItemVm("TestChoco2", "Sabine Bergmann", 5, "Paused"));
            //ListOfCreations.Add(new CreationItemVm("TestChoco3", "Appolo Testmann", 4, "Removed"));
            //ListOfCreations.Add(new CreationItemVm("TestChoco4", "Ingrid Mueller", 2, "In Work"));

        }
    }

}
