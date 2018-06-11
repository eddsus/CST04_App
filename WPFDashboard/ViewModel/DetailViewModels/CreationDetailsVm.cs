using GalaSoft.MvvmLight;
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
        public ObservableCollection<IngredientTestVm> IngredientsList { get; set; }

        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public CreationDetailsVm()
        {
            Ingredients = new ObservableCollection<Ingredient>();
            IngredientsList = new ObservableCollection<IngredientTestVm>();
            IngredientsList.Add(new IngredientTestVm("Cocoa 70%", "mg",75));
            IngredientsList.Add(new IngredientTestVm("Strawberry extract", "mg",5));
            IngredientsList.Add(new IngredientTestVm("Almond", "pieces", 10));
            IngredientsList.Add(new IngredientTestVm("Caramel syrup", "mg", 15));

        }
    
    }
}
