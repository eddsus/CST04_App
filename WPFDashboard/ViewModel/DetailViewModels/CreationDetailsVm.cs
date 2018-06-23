using GalaSoft.MvvmLight;
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
    public class CreationDetailsVm : ViewModelBase
    {
        #region FIELDS
        private Chocolate currentOrderChocolate;
        private bool selectedOrderChocolateState;
        private ObservableCollection<Rating> comments;
        private ObservableCollection<string> availabilityStates;
        private string currentState;
        #endregion

        #region PROPERTIES

        public string CurrentState
        {
            get { return currentState; }
            set
            {
                currentState = value;
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
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public Chocolate CurrentOrderChocolate
        {
            get { return currentOrderChocolate; }
            set
            {
                currentOrderChocolate = value;
                RaisePropertyChanged();
            }
        }

        public bool SelectedOrderChocolateState
        {
            get { return selectedOrderChocolateState; }
            set
            {
                selectedOrderChocolateState = value;
                RaisePropertyChanged();
            }
        }

        private double price;

        public double Price
        {
            get { return price; }
            set
            {
                price = value;
                RaisePropertyChanged();
            }
        }



        public ObservableCollection<Rating> Comments
        {
            get { return comments; }
            set
            {
                comments = value;
                RaisePropertyChanged();
            }
        }





        #endregion


        public CreationDetailsVm()
        {

            Messenger.Default.Register<Chocolate>(this, DisplayChocolateInfo);

        }

        private void DisplayChocolateInfo(Chocolate currentOrderChocolate)
        {
            CurrentOrderChocolate = currentOrderChocolate;
            SelectedOrderChocolateState = CurrentOrderChocolate.Available;
            DefineStates();
            RaisePropertyChanged("CurrentOrderChocolate");
            RaisePropertyChanged("SelectedOrderChocolateState");
            Ingredients = new ObservableCollection<Ingredient>(CurrentOrderChocolate.Ingredients.OrderBy(x=> x.Available));
            Comments = new ObservableCollection<Rating>(CurrentOrderChocolate.Ratings.Where(x => x.Published == true).ToList().OrderBy(x => x.Value));
        }

        private void DefineStates()
        {
            AvailabilityStates = new ObservableCollection<string>() { "Available", "Not Available" };
            if (SelectedOrderChocolateState == true)
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
