using DataAgent;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;
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
        private MainViewModel MainViewModel { get; set; }
        public ObservableCollection<Ingredient> IngredientList
        {
            get { return ingredientList; }
            set
            {
                ingredientList = value;
                RaisePropertyChanged();
            }
        }
        public RelayCommand BtnRefresh { get; set; }
        #endregion

        public IngredientsVm()
        {
            //LEAVE ME EMPTY AND USE initializevm instead!!!
        }

        public void InitializeVm()
        {
            IngredientList = new ObservableCollection<Ingredient>(DataAgent.QueryIngredients());
            

            BtnRefresh = new RelayCommand(() =>
            {
                IngredientList = new ObservableCollection<Ingredient>(DataAgent.QueryIngredients());
                MainViewModel = SimpleIoc.Default.GetInstance<MainViewModel>();
                MainViewModel.ConnectStatus = DataAgent.GetSynchronizerStatus();
            });
        }

        public void IngredientsSynchronized()
        {
            IngredientList = new ObservableCollection<Ingredient>(DataAgent.QueryIngredients());
        }
    }
}
