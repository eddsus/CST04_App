using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Ioc;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFDashboard.ViewModel.DetailViewModels;

namespace WPFDashboard.ViewModel.OrderVModels
{

    public class OrderDetailsVm:ViewModelBase

    {
        #region Properties
        
        private ViewModelBase currrentDetail;

        public ViewModelBase CurrentDetail
        {
            get { return currrentDetail; }
            set {
                currrentDetail = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<PackageTestVm> PackageTestList { get; set; }
        public ObservableCollection<CreationTestVm> CreationTestList { get; set; }
        public ObservableCollection<DetailsTestListVm> OrderContentDetailsList { get; set; }
        public RelayCommand<DetailsTestListVm> BtnDelete { get; set; }
        public RelayCommand<DetailsTestListVm> BtnDetails { get; set; }
        #endregion

        public OrderDetailsVm()
        {
            //PackageDetail = SimpleIoc.Default.GetInstance<PackageDetailVm>();
            OrderContentDetailsList = new ObservableCollection<DetailsTestListVm>();
            InitPackage();
            InitCreation();
            AddItemsToDetailsList();
            BtnDelete = new RelayCommand<DetailsTestListVm>((p)=> { DeleteItem(p); });
            BtnDetails = new RelayCommand<DetailsTestListVm>((p)=> {ShowItemDetails(p);});
        }

        private void ShowItemDetails(DetailsTestListVm p)
        {       if (p.Type.Contains("package"))
                {
                    CurrentDetail = SimpleIoc.Default.GetInstance<PackageDetailsVm>();
                }
                else
                {
                    CurrentDetail = SimpleIoc.Default.GetInstance<CreationDetailsVm>();
                }
         }

        

        private void AddItemsToDetailsList()
        {
            foreach (var item in CreationTestList)
            {
                OrderContentDetailsList.Add(item);
            }

            foreach (var item in PackageTestList)
            {
                OrderContentDetailsList.Add(item);
            }
        }

        private void InitCreation()
        {
            CreationTestList = new ObservableCollection<CreationTestVm>();
            CreationTestList.Add(new CreationTestVm(4, "Strawberry dream"));
            CreationTestList.Add(new CreationTestVm(2,"Marshmellow pillow"));
        }

        private void InitPackage()
        {
            PackageTestList = new ObservableCollection<PackageTestVm>();
            PackageTestList.Add(new PackageTestVm(5, "Chokos"));
            PackageTestList.Add(new PackageTestVm(10, "Nice packages"));
        }

        private void DeleteItem(DetailsTestListVm item)
        {
            OrderContentDetailsList.Remove(item); 
        }
    }
}
