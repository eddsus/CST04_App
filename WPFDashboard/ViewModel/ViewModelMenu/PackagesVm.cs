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
        public DataAgentUnit DataAgent { get; set; }

        //testing shapes

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
            //LEAVE ME EMPTY AND USE initializevm instead!!!
        }

        public void InitializeVm()
        {
            Shapes = new ObservableCollection<Shape>();

            //foreach (var item in DataAgent.QueryShapes())
            //{
            //    Shapes.Add(item);
            //}
        }

        public void PackagesSynchronized()
        {
            throw new NotImplementedException();
        }
    }
}
