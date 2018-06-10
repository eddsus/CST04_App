using DataAgent;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
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
    public class CreationsVm : ViewModelBase, ISynchronizable
    {
        //todo: Generate regions for fields and properties
        #region Properties
        public RelayCommand BtnCreationDetails { get; set; }
        private ViewModelBase creationDetailView;   

        public ViewModelBase CreationDetailView
        {
            get { return creationDetailView; }
            set { creationDetailView = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<string> States { get; set; }
        public ObservableCollection<CreationItemVm> ListOfCreations { get; set; }
        public string CurrentState { get; set; }
        public CreationItemVm CurrentCreationItem { get; set; }

        #endregion

        public CreationsVm()
        {
            InitializeCreationsList();
            InitStateList();
            Messenger.Default.Register<RefreshMessage>(this, Refresh);
            BtnCreationDetails = new RelayCommand(()=> { ShowCreationdetails(); });
        }

        private void InitStateList()
        {
            States = new ObservableCollection<string>() {"In Work", "Paused","Removed"};
        }

        private void ShowCreationdetails()
        {
            CreationDetailView = SimpleIoc.Default.GetInstance<CreationDetailsVm>();
        }

        private void Refresh(RefreshMessage obj)
        {
            if (GetType() == obj.View)
            {
                InitializeCreationsList();
            }
        }
        public void ViewSynchronized()
        {
            InitializeCreationsList();
        }


        private void InitializeCreationsList()
        {
            ListOfCreations = new ObservableCollection<CreationItemVm>();
            ListOfCreations.Add(new CreationItemVm("TestChoco1", "Max Mustermann", 3,"In Work"));
            ListOfCreations.Add(new CreationItemVm("TestChoco2", "Sabine Bergmann", 5, "Paused"));
            ListOfCreations.Add(new CreationItemVm("TestChoco3", "Appolo Testmann", 4, "Removed"));
            ListOfCreations.Add(new CreationItemVm("TestChoco4", "Ingrid Mueller", 2, "In Work"));

        }
    }

}
