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
    public class CreationDetailsVm:ViewModelBase
    {
        #region PROPERTIES
        public ObservableCollection<Ingredient> Ingredients { get; set; }

        private OrderContentChocolate currentOrderChocolate;

        public OrderContentChocolate CurrentOrderChocolate
        {
            get { return currentOrderChocolate; }
            set { currentOrderChocolate = value;
                RaisePropertyChanged();
            }
        }

        private bool selectedOrderChocolateState;

        public bool SelectedOrderChocolateState
        {
            get { return selectedOrderChocolateState; }
            set { selectedOrderChocolateState = value;
                RaisePropertyChanged();
            }
        }

        private ObservableCollection<Rating> comments;

        public ObservableCollection<Rating> Comments
        {
            get { return comments; }
            set { comments = value;
                RaisePropertyChanged();
            }
        }



        #endregion


        public CreationDetailsVm()
        {
            //InitIngredients();
            Messenger.Default.Register<OrderContentChocolate>(this,DisplayChocolateInfo);
            

        }

        private void DisplayChocolateInfo(OrderContentChocolate currentOrderChocolate)
        {
            CurrentOrderChocolate = currentOrderChocolate;
            SelectedOrderChocolateState = CurrentOrderChocolate.Chocolate.Available;
            RaisePropertyChanged("CurrentOrderChocolate");
            RaisePropertyChanged("SelectedOrderChocolateState");
            Ingredients = new ObservableCollection<Ingredient>(CurrentOrderChocolate.Chocolate.Ingredients);
           // Comments = new ObservableCollection<Rating>(CurrentOrderChocolate.Chocolate.Ratings);
        }

        //private void InitIngredients()
        //{
            
        //   // Ingredients = CurrentOrderChocolate.Chocolate.Ingredients;
        //}
    }
}
