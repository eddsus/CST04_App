using DataAgent;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDashboard.Helpers;

namespace WPFDashboard.ViewModel.ViewModelMenu
{
    public class CreationsVm : ViewModelBase, ISynchronizable
    {
        //todo: Generate regions for fields and properties

        public CreationsVm()
        {
            InitializeCreationsList();
            Messenger.Default.Register<RefreshMessage>(this, Refresh);
        }


        private void Refresh(RefreshMessage obj)
        {
            if (GetType() == obj.View)
            {
                InitializeCreationsList();
            }
        }
        public void ViewSynchronized()
        {
            InitializeCreationsList();
        }


        private void InitializeCreationsList()
        {
            //throw new NotImplementedException();
        }
    }

}
