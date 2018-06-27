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
        private RelayCommand<Chocolate> btnPublish;

        public RelayCommand<Chocolate> BtnPublish
        {
            get { return btnPublish; }
            set { btnPublish = value;
               
                RaisePropertyChanged();
            }
        }

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
            InitStateList();
            Messenger.Default.Register<RefreshMessage>(this, Refresh);
            BtnPublish = new RelayCommand<Chocolate>(
                 (i) =>
                 {
                     if (i.Available)
                     {
                         SelectedChocolate = i;
                         SelectedChocolate.Available = false;
                         //Update the databases
                         DataAgentUnit.GetInstance().UpdateChocolate(i);
                         ShowCreationdetails(i);
                         //and inform the infobar
                         Messenger.Default.Send(new PropertyChanged<Chocolate>(i, " has been unpublished", 5));
                         Refresh(new RefreshMessage(GetType()));
                     }
                     else
                     {
                         i.CreatedBy = new Customer() {
                             CustomerId = new Guid("c9c1017b-e655-47c4-9d89-31dff469c130")
                         };
                         SelectedChocolate = i;
                         SelectedChocolate.Available = true;
                         //Update the databases
                         DataAgentUnit.GetInstance().UpdateChocolate(i);
                         ShowCreationdetails(i);
                         //and inform the infobar
                         Messenger.Default.Send(new PropertyChanged<Chocolate>(i, " has been published", 5));
                         Refresh(new RefreshMessage(GetType()));
                     }
                 },
                (i) =>
                {
                    //only allow updates when connected and an ingredient is selected
                    return SimpleIoc.Default.GetInstance<MainViewModel>().ConnectStatus;
                });
            InitializeChocolateList();
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
            var temp = DataAgentUnit.GetInstance().QueryCreations();
            ListOfChocolates = new ObservableCollection<Chocolate>(temp.OrderBy(x => x.Available).ToList());
        }
    }

}
