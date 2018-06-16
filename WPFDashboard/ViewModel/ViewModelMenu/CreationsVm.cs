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
            ListOfChocolates = new ObservableCollection<Chocolate>();
            

        }
    }

}
