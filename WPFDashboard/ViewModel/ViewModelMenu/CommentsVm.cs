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
   public  class CommentsVm:ViewModelBase
    {
        
        #region FIELDS
        private ObservableCollection<Rating> listOfRatings;
        private RelayCommand<Rating> btnPublish;
        private Rating currentRating;
        #endregion

        #region PROPERTIES
        public Rating CurrentRating
        {
            get { return currentRating; }
            set { currentRating = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand<Rating> BtnPublish
        {
            get { return btnPublish; }
            set { btnPublish = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<Rating> ListOfRatings
        {
            get { return listOfRatings; }
            set { listOfRatings = value;
                RaisePropertyChanged();
            }
        }
        #endregion


       
        public CommentsVm()
        {
            InitializeCommentsList();
            Messenger.Default.Register<RefreshMessage>(this, Refresh);
            BtnPublish = new RelayCommand<Rating>(
                (i)=>
                {
                    if (i.Published)
                    {
                        CurrentRating = i;
                        CurrentRating.Published = false;
                        //Update the databases
                        DataAgentUnit.GetInstance().UpdateRating(i);
                        //and inform the infobar
                        Messenger.Default.Send(new PropertyChanged<Rating>(i, "has been unpublished", 5));
                        //IngredientList.Where(j => j.IngredientId == i.IngredientId).Select(k => k).First().Available = false;
                        Refresh(new RefreshMessage(GetType()));
                    }
                    else
                    {
                        CurrentRating = i;
                        CurrentRating.Published = true;
                        //Update the databases
                        DataAgentUnit.GetInstance().UpdateRating(i);
                        //and inform the infobar
                        Messenger.Default.Send(new PropertyChanged<Rating>(i, "has been published", 5));
                        //IngredientList.Where(j => j.IngredientId == i.IngredientId).Select(k => k).First().Available = true;
                        Refresh(new RefreshMessage(GetType()));
                    }
                },
                (i)=> 
                {
                    return SimpleIoc.Default.GetInstance<MainViewModel>().ConnectStatus;
                });
            

        }


        private void Refresh(RefreshMessage obj)
        {
            if (GetType() == obj.View)
            {
                InitializeCommentsList();
            }
        }

        public void ViewSynchronized()
        {
            InitializeCommentsList();
        }
        public void InitializeCommentsList()
        {

            var temp  = DataAgentUnit.GetInstance().QueryRatings();
            ListOfRatings = new ObservableCollection<Rating>(temp.OrderBy(x => x.Published).ToList());

        }


    }
}
