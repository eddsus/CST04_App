using DataAgent;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDashboard.ViewModel.ViewModelMenu
{
    public class CreationsVm:ViewModelBase
    {
        public DataAgentUnit DataAgent { get; set; }
        public CreationsVm()
        {
            //LEAVE ME EMPTY AND USE initializevm instead!!!
        }

        public void InitializeVm()
        {

        }

        public void CreationsSynchronized()
        {
            throw new NotImplementedException();
        }

    }

}
