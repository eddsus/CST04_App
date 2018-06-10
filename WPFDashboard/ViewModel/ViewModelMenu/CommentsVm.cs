using DataAgent;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using SharedDataTypes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDashboard.ViewModel.ViewModelMenu.DummyVm;

namespace WPFDashboard.ViewModel.ViewModelMenu
{
   public  class CommentsVm:ViewModelBase
    {

        //testing wraps
        //todo: Generate regions for fields and properties
        #region Properties
        public RelayCommand<CommentItemVm> BtnDelete{ get; set; }
        public ObservableCollection<CommentItemVm> ListOfComments { get; set; }
        #endregion
        private ObservableCollection<Wrapping> wraps;

        public ObservableCollection<Wrapping> Wraps
        {
            get { return wraps; }
            set { wraps = value;
                RaisePropertyChanged();
            }
        }

        public CommentsVm()
        {
            InitializeCommentsList();
            Messenger.Default.Register<RefreshMessage>(this, Refresh);
            BtnDelete = new RelayCommand<CommentItemVm>((p)=> { DeleteItem(p); });
        }

        private void DeleteItem(CommentItemVm p)
        {
            ListOfComments.Remove(p);
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
            //Wraps = new ObservableCollection<Wrapping>();

            //foreach (var item in DataAgent.QueryWrappings())
            //{
            //    Wraps.Add(item);
            //}

            ListOfComments = new ObservableCollection<CommentItemVm>();
            ListOfComments.Add(new CommentItemVm("Max Mustermann","Birthday Box","Deliicous!"));
            ListOfComments.Add(new CommentItemVm("Stan Marsh", "Xmas Box", "Won't order again") {});
            ListOfComments.Add(new CommentItemVm("Angry Bird", "Birthday Box", "Tastes like shit"));
          
        }


    }
}
