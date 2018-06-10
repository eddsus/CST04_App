using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDashboard.ViewModel.ViewModelMenu.DummyVm
{
    public class PackageItemVm
    {
        public PackageItemVm(string packageName, string createdBy, string state, int rating)
        {
            PackageName = packageName;
            CreatedBy = createdBy;
            State = state;
            Rating = rating;
        }

        public string PackageName { get; set; }
        public string CreatedBy { get; set; }
        public string State { get; set; }
        public int Rating { get; set; }
   
    }
}
