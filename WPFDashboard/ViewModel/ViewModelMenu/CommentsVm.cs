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

        DataAgentUnit dataAgent = new DataAgentUnit();

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
            Wraps = new ObservableCollection<Wrapping>();

            foreach (var item in dataAgent.QueryWrappings())
            {
                Wraps.Add(item);
            }
        }


    }
}
