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
        private ObservableCollection<Chocolate> listOfChocolates;
        private Chocolate selectedChocolate;
        #endregion

        #region PROPERTIES
        public Chocolate SelectedChocolate
        {
            get { return selectedChocolate; }
            set { selectedChocolate = value;
                if (SelectedChocolate != null)
                    ShowCreationdetails(value);
                RaisePropertyChanged();
            }
        }

        public ViewModelBase CreationDetailView
        {
            get { return creationDetailView; }
            set
            {
                creationDetailView = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<string> States { get; set; }



        public ObservableCollection<Chocolate> ListOfChocolates
        {
            get { return listOfChocolates; }
            set
            {
                listOfChocolates = value;
                RaisePropertyChanged();
            }
        }

        public string CurrentState { get; set; }


        #endregion

        public CreationsVm()
        {
            InitializeChocolateList();
            InitStateList();
            Messenger.Default.Register<RefreshMessage>(this, Refresh);
        }

        private void InitStateList()
        {
            States = new ObservableCollection<string>() { };
        }

        private void ShowCreationdetails(Chocolate p)
        {
            Messenger.Default.Send(p);
            CreationDetailView = SimpleIoc.Default.GetInstance<CreationDetailsVm>();
            RaisePropertyChanged("CreationDetailView");
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
        }
    }

}
