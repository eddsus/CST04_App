using DataAgent;
using DataAgent.SR_Synchronizer;
using GalaSoft.MvvmLight;
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
        DataAgentUnit dataagent = new DataAgentUnit();

        private ObservableCollection<SharedDataTypes.Ingredient> ingredientList;

        public ObservableCollection<SharedDataTypes.Ingredient> IngredientList
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
            IngredientList = new ObservableCollection<SharedDataTypes.Ingredient>(dataagent.QueryAllIngredients().Select(i => new SharedDataTypes.Ingredient() {
               IngredientId = i.IngredientId,
               Name = i.Name,
               Description = i.Description,
               Available = i.Available,
               Price = i.Price,
               Type = i.Type,
               UnitType = i.UnitType
            }).ToList());
            //IngredientList=dataagent.QueryAllIngredients();

            //foreach (var item in dataagent.QueryAllIngredients())
            //{
            //    IngredientList.Add(item);
            //}
        }
    }
}
