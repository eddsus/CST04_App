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
        private Ingredient currentIngredient;
        #endregion

        #region PROPERTIES
        public Ingredient CurrentIngredient
        {
            get { return currentIngredient; }
            set
            {
                currentIngredient = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<Ingredient> IngredientList
        {
            get { return ingredientList; }
            set
            {
                ingredientList = value;
                RaisePropertyChanged();
            }
        }
        public RelayCommand<Ingredient> BtnActivate { get; set; }
        public RelayCommand<Ingredient> BtnDeactivate { get; set; }
        #endregion

        public IngredientsVm()
        {
            CurrentIngredient = null;
            InitializeIngredientList();

            //Register for Refresh Message to know when to refresh
            Messenger.Default.Register<RefreshMessage>(this, Refresh);


            BtnActivate = new RelayCommand<Ingredient>(
                (i) =>
                {
                    CurrentIngredient = i;
                    CurrentIngredient.Available = true;
                    //Update the databases
                    DataAgentUnit.GetInstance().UpdateIngredient(i);
                    //and inform the infobar
                    Messenger.Default.Send(new PropertyChanged<Ingredient>(i, "has been activated", 5));
                    //IngredientList.Where(j => j.IngredientId == i.IngredientId).Select(k => k).First().Available = true;
                    Refresh(new RefreshMessage(GetType()));
                },
                (i) =>
                {
                    //only allow updates when connected and an ingredient is selected
                    return DataAgentUnit.GetInstance().GetSynchronizerStatus();
                });
            BtnDeactivate = new RelayCommand<Ingredient>(
                (i) =>
                {
                    CurrentIngredient = i;
                    CurrentIngredient.Available = false;
                    //Update the databases
                    DataAgentUnit.GetInstance().UpdateIngredient(i);
                    //and inform the infobar
                    Messenger.Default.Send(new PropertyChanged<Ingredient>(i, "has been deactivated", 5));
                    //IngredientList.Where(j => j.IngredientId == i.IngredientId).Select(k => k).First().Available = false;
                    Refresh(new RefreshMessage(GetType()));
                },
                (i) =>
                {
                    //only allow updates when connected and an ingredient is selected
                    return DataAgentUnit.GetInstance().GetSynchronizerStatus();
                });
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
