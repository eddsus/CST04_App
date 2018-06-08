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

namespace WPFDashboard.ViewModel.ViewModelMenu
{

    public class IngredientsVm : ViewModelBase, ISynchronizable
    {
        #region FIELDS
        private ObservableCollection<Ingredient> ingredientList;
        #endregion

        #region PROPERTIES
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
            InitializeIngredientList();

            //Register for Refresh Message to know when to refresh
            Messenger.Default.Register<RefreshMessage>(this, Refresh);
        }


        private void Refresh(RefreshMessage obj)
        {
            if (GetType() == obj.View)
            {
                InitializeIngredientList();
            }
        }

        //this message will be called after the synchronizer has succefulyy modified the ingredients list
        public void ViewSynchronized()
        {
            InitializeIngredientList();
        }

        private void InitializeIngredientList()
        {
            IngredientList = new ObservableCollection<Ingredient>(DataAgentUnit.GetInstance().QueryIngredients());
        }

    }
}
