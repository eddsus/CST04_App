using DataAgent;
using DataAgent.SR_Synchronizer;
using GalaSoft.MvvmLight;
using SharedDataTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDashboard.ViewModel.ViewModelMenu
{
    
    public class IngredientsVm:ViewModelBase
    {
        DataAgentUnit DataAgent = new DataAgentUnit();

        private ObservableCollection<Ingredient> ingredientList;

        public ObservableCollection<Ingredient> IngredientList
        {
            get { return ingredientList; }
            set
            {
                ingredientList = value;
                RaisePropertyChanged();
            }
        }

        //public ObservableCollection<Ingredient> IngredientList { get; set; }

        public IngredientsVm()
        {
            IngredientList = new ObservableCollection<SharedDataTypes.Ingredient>(DataAgent.QueryIngredients());
        }
    }
}
