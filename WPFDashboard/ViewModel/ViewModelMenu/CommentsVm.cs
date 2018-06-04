using DataAgent;
using GalaSoft.MvvmLight;
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

        public DataAgentUnit DataAgent { get; set; }

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
            //LEAVE ME EMPTY AND USE initializevm instead!!!
        }

        public void InitializeVm()
        {
            Wraps = new ObservableCollection<Wrapping>();

            //foreach (var item in DataAgent.QueryWrappings())
            //{
            //    Wraps.Add(item);
            //}
        }

        public void CommentsSynchronized()
        {
            throw new NotImplementedException();
        }


    }
}
