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
        private readonly DataAgentUnit DataAgent = new DataAgentUnit();
        private MainViewModel mainViewModel { get; set; }

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

        public RelayCommand BtnRefresh { get; set; }

        //public ObservableCollection<Ingredient> IngredientList { get; set; }

        public IngredientsVm()
        {
            IngredientList = new ObservableCollection<Ingredient>(DataAgent.QueryIngredients());
            mainViewModel = SimpleIoc.Default.GetInstance<MainViewModel>();
            BtnRefresh = new RelayCommand(() => {
                IngredientList = new ObservableCollection<Ingredient>(DataAgent.QueryIngredients());
                mainViewModel.ConnectStatus = DataAgent.GetSynchronizerStatus();
                DataAgent.Syncronize();
            });
        }
    }
}
