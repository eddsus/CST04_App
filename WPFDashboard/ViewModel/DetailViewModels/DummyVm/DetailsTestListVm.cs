using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDashboard.ViewModel.DetailViewModels
{
    public class DetailsTestListVm
    {
        public DetailsTestListVm(int amount, string name)
        {
            Amount = amount;
            Name = name;
        }

       
        public int Amount { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

    }
}
