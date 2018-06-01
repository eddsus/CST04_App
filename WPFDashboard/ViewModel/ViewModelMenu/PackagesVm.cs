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
    public class PackagesVm : ViewModelBase
    {

        //testing shapes

        DataAgentUnit dataAgent;

        private ObservableCollection<Shape> shapes;

        public ObservableCollection<Shape> Shapes
        {
            get { return shapes; }
            set
            {
                shapes = value;
                RaisePropertyChanged();
            }
        }

        public PackagesVm()
        {
            Shapes = new ObservableCollection<Shape>();
            dataAgent = new DataAgentUnit(DataSynchronized);

            //foreach (var item in dataAgent.QueryShapes())
            //{
            //    Shapes.Add(item);
            //}
        }

        private void DataSynchronized()
        {
            throw new NotImplementedException();
        }
    }
}
