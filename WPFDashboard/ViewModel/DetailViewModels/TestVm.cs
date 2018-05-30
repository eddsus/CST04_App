using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDashboard.ViewModel.DetailViewModels
{
    public class TestVm
    {
        public TestVm(int amount, string name)
        {
            Amount = amount;
            Name = name;
        }

        public int Amount { get; set; }
        public string Name { get; set; }
    }
}
