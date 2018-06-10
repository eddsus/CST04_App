using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDashboard.ViewModel.ViewModelMenu.DummyVm
{
    public class CommentItemVm
    {
        public CommentItemVm(string customerName, string product, string comment)
        {
            CustomerName = customerName;
            Product = product;
            Comment = comment;
        }

        public string CustomerName { get; set; }
        public string Product { get; set; }
        public string Comment { get; set; }
       
    }
}
