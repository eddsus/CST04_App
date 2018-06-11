using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDashboard.ViewModel.DetailViewModels
{
    public class CreationTestVm : DetailsTestListVm
    {
        
        public CreationTestVm(int amount, string name) : base(amount, name)
        {
            Type = "creation";
        }
    }
}
