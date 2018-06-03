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

namespace WPFDashboard.ViewModel.ViewModelMenu
{
    
    public class IngredientsVm:ViewModelBase
    {
        #region FIELDS
        private ObservableCollection<Ingredient> ingredientList;
        #endregion

        #region PROPERTIES
        public DataAgentUnit DataAgent { get; set; }
        public ObservableCollection<Ingredient> IngredientList
        {
            get { return ingredientList; }
            set
            {
                ingredientList = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        public IngredientsVm()
        {
            //LEAVE ME EMPTY AND USE initializevm instead!!!
        }

        public void InitializeVm()
        {
            IngredientList = new ObservableCollection<Ingredient>(DataAgent.QueryIngredients());

            //Register for Refresh Message to know when to refresh
            Messenger.Default.Register<RefreshMessage>(this, RefreshView);
        }

        private void RefreshView(RefreshMessage obj)
        {
            IngredientList = new ObservableCollection<Ingredient>(DataAgent.QueryIngredients());
        }

        //this message will be called after the synchronizer has succefulyy modified the ingredients list
        public void IngredientsSynchronized()
        {
            IngredientList = new ObservableCollection<Ingredient>(DataAgent.QueryIngredients());
        }

    }
}
