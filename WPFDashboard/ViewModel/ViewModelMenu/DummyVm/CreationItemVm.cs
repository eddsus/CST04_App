using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDashboard.ViewModel.ViewModelMenu.DummyVm
{
    public class CreationItemVm
    {
        public CreationItemVm(string chocoName, string createdBy, int rating, string state)
        {
            ChocoName = chocoName;
            CreatedBy = createdBy;
            
            Rating = rating;
            State = state;
        }

        public string ChocoName { get; set; }
        public string CreatedBy { get; set; }
       
        public int Rating { get; set; }
        public string State { get; set; }
    }
}
