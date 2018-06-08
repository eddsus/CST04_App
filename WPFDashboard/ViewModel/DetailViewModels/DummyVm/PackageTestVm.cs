using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDashboard.ViewModel.DetailViewModels
{
    public class PackageTestVm : DetailsTestListVm
    {
      
        public PackageTestVm(int amount, string name) : base(amount, name)
        {

            Type = "package";

        }
    }
}
