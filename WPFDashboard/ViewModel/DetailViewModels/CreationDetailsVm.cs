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

        private string selectedOrderChocolateState;

        public string SelectedOrderChocolateState
        {
            get { return selectedOrderChocolateState; }
            set { selectedOrderChocolateState = value;
                RaisePropertyChanged();
            }
        }


        #endregion


        public CreationDetailsVm()
        {
            InitIngredients();
            Messenger.Default.Register<OrderContentChocolate>(this,DisplayChocolateInfo);
            

        }

        private void DisplayChocolateInfo(OrderContentChocolate currentOrderChocolate)
        {
            CurrentOrderChocolate = currentOrderChocolate;
            
        }

        private void InitIngredients()
        {
            Ingredients = new ObservableCollection<Ingredient>();
        }
    }
}
