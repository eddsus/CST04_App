﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFDashboard.Views
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : UserControl
    {
        public OrderView()
        {
            InitializeComponent();
        }

       

        //private void BtnSearch_Click(object sender, RoutedEventArgs e)
        //{
        //    OrderListbox.SelectedItems.Clear();
          
        //    foreach (var item in OrderListbox.ItemsSource)
        //    {
        //        if (item.ToString().ToLower().Contains(searchBox.Text.ToLower()))
        //        {
        //            OrderListbox.SelectedItem=item;
        //        }
        //    }
        //}
    }
}
