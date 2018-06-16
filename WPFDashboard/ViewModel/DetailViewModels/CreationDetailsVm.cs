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
        #region PROPERTIES
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        #endregion


        public CreationDetailsVm()
        {
            InitIngredients();
           
            

        }

        private void InitIngredients()
        {
            Ingredients = new ObservableCollection<Ingredient>();
        }
    }
}
