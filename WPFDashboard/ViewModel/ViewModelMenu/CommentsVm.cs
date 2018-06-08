using DataAgent;
using GalaSoft.MvvmLight;
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

        //testing wraps
        //todo: Generate regions for fields and properties
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
            Wraps = new ObservableCollection<Wrapping>();

            //foreach (var item in DataAgent.QueryWrappings())
            //{
            //    Wraps.Add(item);
            //}
        }


    }
}
