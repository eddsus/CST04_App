/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:WPFDashboard"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Xml.Linq;
using WPFDashboard.ViewModel.DetailViewModels;
using WPFDashboard.ViewModel.ViewModelMenu;
using System.Configuration;
using System.Collections.Generic;
using DataHandler;

namespace WPFDashboard.ViewModel
{

    public class ViewModelLocator
    {
      
        public ViewModelLocator()
        {

            ChangeConnectionString();
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
         

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<OrdersVm>(true);
            SimpleIoc.Default.Register<CommentsVm>(true);
            SimpleIoc.Default.Register<CreationsVm>(true);
            SimpleIoc.Default.Register<PackagesVm>(true);
            SimpleIoc.Default.Register<IngredientsVm>(true);

            //Order has inner Views
            //Make a detail views for all pages
            SimpleIoc.Default.Register<OrderDetailsVm>(true);
            SimpleIoc.Default.Register<PackageDetailsVm>(true);
            SimpleIoc.Default.Register<CreationDetailsVm>(true);

        }

        private static void ChangeConnectionString()
        {
            LocalDataHandler ldh = new LocalDataHandler(); ;
            AppDomain.CurrentDomain.SetData("DataDirectory", ldh.GetDataDirectory());
            //try
            //{
            //    XDocument doc = XDocument.Load(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            //    var query = from p in doc.Descendants("connectionStrings").Descendants()
            //                select p;
            //    foreach (var child in query)
            //    {
            //        foreach (var atr in child.Attributes())
            //        {
            //            if (atr.NextAttribute != null && atr.NextAttribute.Name == "connectionString")
            //            {
            //                Dictionary<string, string> dictionary = new Dictionary<string, string>();
            //                string[] items = atr.NextAttribute.Value.TrimEnd(';').Split(';');
            //                foreach (string item in items)
            //                {
            //                    if (item.Contains("="))
            //                    {
            //                        string[] keyValue = item.Split('=');
            //                        dictionary.Add(keyValue[0], keyValue[1]);
            //                    }
            //                }
            //                SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            //                connectionStringBuilder.AttachDBFilename = @"C:\Users\aidan\source\repos\CaseStudy4\GitHub\AppUpdated\DataHandler\LocalDb.mdf;";
            //                connectionStringBuilder.ApplicationName = "LocalDbEntities";
            //                //connectionStringBuilder. = dictionary["provider"];
            //                //connectionStringBuilder. = dictionary["metadata"];
            //                //connectionStringBuilder["provider connection string"] = dictionary["provider connection string"];
            //                connectionStringBuilder.DataSource = @"(LocalDB)\MSSQLLocalDB;";
            //                connectionStringBuilder.MultipleActiveResultSets = true;
            //                connectionStringBuilder.IntegratedSecurity = true;


            //                EntityConnectionStringBuilder entityBuilder = new EntityConnectionStringBuilder(atr.NextAttribute.Value);
            //                entityBuilder.ProviderConnectionString = connectionStringBuilder.ToString();
            //                atr.NextAttribute.Value = entityBuilder.ToString();

            //            }
            //        }

            //    }
            //    doc.Save(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            //    ConfigurationManager.RefreshSection("connectionString");
            //}
            //catch (Exception x)
            //{
            //}
        }

        //Creation Details View
        public CreationDetailsVm CreationDetail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CreationDetailsVm>();
            }
        }
        //Package Details Views
        public PackageDetailsVm PackageDetail
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PackageDetailsVm>();
            }
        }

        //Order Details View
        public OrderDetailsVm OrderDetails
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OrderDetailsVm>();
            }
        }

        //Main Page Views
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public OrdersVm Orders
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OrdersVm>();
            }
        }
        public CommentsVm Comments
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CommentsVm>();
            }
        }

        public CreationsVm Creations
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CreationsVm>();
            }
        }

        public PackagesVm Packages
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PackagesVm>();
            }
        }

        public IngredientsVm Ingredients
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IngredientsVm>();
            }
        }
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}