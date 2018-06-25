using DataAgent;
using DataHandler;
using SharedDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LocalSynchronization
{
    public class LocalSynchronizer
    {
        #region FIELDS
        private readonly LocalDataHandler dataHandler;
        private readonly ServiceHandler serviceHandler;
        private readonly Action OrderInformer;
        private readonly Action PackageInformer;
        private readonly Action CreationInformer;
        private readonly Action IngredientInformer;
        private readonly Action CommentsInformer;
        private readonly Action<string> DisplayInformation;
        private readonly Action<bool> SetConnectionStatus;
        private bool connected = false;
        private DateTime LastUpdate;
        #endregion
        public LocalSynchronizer(Action orderInformer, Action packageInformer, Action creationInformer, Action ingredientInformer, Action commentsInformer, Action<string> displayInformation, Action<bool> setConnectionStatus)
        {
            dataHandler = new LocalDataHandler();
            //serviceHandler = new ServiceHandler("http://localhost:8090/AppServiceService/");
            serviceHandler = new ServiceHandler("http://wi-gate.technikum-wien.at:60935/AppServiceService/");
            OrderInformer = orderInformer;
            PackageInformer = packageInformer;
            CreationInformer = creationInformer;
            IngredientInformer = ingredientInformer;
            CommentsInformer = commentsInformer;
            DisplayInformation = displayInformation;
            SetConnectionStatus = setConnectionStatus;
        }

        #region INITIALIZATION
        private bool Connected()
        {
            try
            {
                connected = serviceHandler.CallService<bool>("IsAlive");
            }
            catch (Exception)
            {
                connected = false;
            }
            return connected;
        }
        public void StartSyncing()
        {
            Task.Factory.StartNew(StartSynchronizing);
        }
        private void StartSynchronizing()
        {
            while (!connected)
            {
                if (Connected())
                {
                    SetConnectionStatus(true);
                    break;
                }
                SetConnectionStatus(false);
                DisplayInformation.Invoke("waiting for connection");
                Thread.Sleep(10000);
            }
            DisplayInformation.Invoke("connected, waiting for sync");

            IntitializeBaseData();

            while (true)
            {
                if (Connected())
                {
                    SetConnectionStatus(true);
                    SynchronizeIngredients();
                    SynchronizeCustomers();
                    SynchronizeCreations();
                    SynchronizePackages();
                    SynchronizeComments();
                    SynchronizeOrders();
                    DisplayInformation.Invoke("Last sync @ " + DateTime.Now);
                }
                else
                {
                    SetConnectionStatus(false);
                }
                Thread.Sleep(60000);
            }
        }



        #region INIT BASE DATA
        private void IntitializeBaseData()
        {
            InitializeShapes();
            InitializeCustomStyles();
            InitializeOrderStatus();
            InitializeWrappings();

        }

        private void InitializeShapes()
        {
            //Get list from server
            var serverTemplist = serviceHandler.CallService<List<Shape>>(@"QueryShapes");
            //get local list
            var localTempList = dataHandler.QueryShapes();
            if (serverTemplist != null && localTempList != null)
            {
                if (serverTemplist.Count != localTempList.Count)
                {
                    //Empty local list
                    dataHandler.ClearShapes();
                    //And refill it with the fresh ones
                    foreach (var item in serverTemplist)
                    {
                        dataHandler.InsertShape(item);
                    }
                }
            }
        }

        private void InitializeWrappings()
        {
            var serverTemplist = serviceHandler.CallService<List<Wrapping>>(@"QueryWrappings");
            var localTempList = dataHandler.QueryWrappings();


            if (serverTemplist != null && localTempList != null)
            {
                if (serverTemplist.Count != localTempList.Count)
                {
                    dataHandler.ClearWrappings();
                    foreach (var item in serverTemplist)
                    {
                        dataHandler.InsertWrapping(item);
                    }
                }
            }
        }

        private void InitializeCustomStyles()
        {
            var serverTemplist = serviceHandler.CallService<List<CustomStyle>>(@"QueryCustomStyles");
            var localTempList = dataHandler.QueryCustomStyles();


            if (serverTemplist != null && localTempList != null)
            {
                if (serverTemplist.Count != localTempList.Count)
                {
                    dataHandler.ClearCustomStyles();
                    foreach (var item in serverTemplist)
                    {
                        dataHandler.InsertCustomStyle(item);
                    }
                }
            }
        }

        private void InitializeOrderStatus()
        {
            var serverTemplist = serviceHandler.CallService<List<OrderStatus>>(@"QueryOrderStates");
            var localTempList = dataHandler.QueryOrderStates();


            if (serverTemplist != null && localTempList != null)
            {
                if (serverTemplist.Count != localTempList.Count)
                {
                    dataHandler.ClearOrderStatus();
                    foreach (var item in serverTemplist)
                    {
                        dataHandler.AddOrderStatus(item);
                    }
                }
            }
        }
        #endregion

        #endregion

        #region SYNC LOCAL DB AND UPDATE GUI
        private void SynchronizeOrders()
        {

            LastUpdate = new DateTime(1983, 11, 20);

            if (dataHandler.QueryOrders().Count > 0)
            {
                LastUpdate = dataHandler.QueryOrders().OrderByDescending(i => i.Modified).Select(j => j.Modified.GetValueOrDefault()).First();
            }

            List<Order> ServerOrders = serviceHandler.CallService<List<Order>>(@"QueryOrders");
            if (ServerOrders != null)
            {

                List<Order> newOrders = ServerOrders.Where(i => i.Modified > LastUpdate).Select(j => j).ToList();

                foreach (var item in newOrders)
                {
                    List<OrderContentPackage> tempOcPackage = serviceHandler.CallService<List<OrderContentPackage>>(@"QueryOrdersContentPackage/" + item.OrderId);
                    List<OrderContentChocolate> tempOcChoco = serviceHandler.CallService<List<OrderContentChocolate>>(@"QueryOrdersContentChocolate/" + item.OrderId);
                    item.Content = new List<OrderContent>();
                    item.Content.AddRange(tempOcPackage);
                    item.Content.AddRange(tempOcChoco);

                    if (dataHandler.QueryOrders().Where(p => p.OrderId.Equals(item.OrderId)).Count() == 0)
                    {
                        SynchronizeCustomers();
                        dataHandler.InsertOrder(item);

                        foreach (var item2 in item.Content)
                        {
                            dataHandler.InsertOrderContent(item2, item.OrderId);
                        }
                        if (tempOcChoco.Count > 0)
                        {
                            foreach (var item3 in tempOcChoco)
                            {
                                dataHandler.InsertOcHasChoco(item3);
                            }
                        }
                        if (tempOcPackage.Count > 0)
                        {
                            foreach (var item4 in tempOcPackage)
                            {
                                dataHandler.InsertOcHasPackage(item4);
                            }
                        }


                        OrderInformer.Invoke();
                    }
                    else
                    {
                        dataHandler.UpdateOrder(item);

                        foreach (var item2 in item.Content)
                        {
                            dataHandler.UpdateOrderContent(item2, item.OrderId);
                        }
                        if (tempOcChoco.Count > 0)
                        {
                            foreach (var item3 in tempOcChoco)
                            {
                                dataHandler.UpdateOcHasChoco(item3);
                            }
                        }
                        if (tempOcPackage.Count > 0)
                        {
                            foreach (var item4 in tempOcPackage)
                            {
                                dataHandler.UpdateOcHasPackage(item4);
                            }
                        }
                        OrderInformer.Invoke();
                    }
                }
            }
        }

        private void SynchronizeCustomers()
        {
            LastUpdate = new DateTime(1983, 11, 20);

            if (dataHandler.QueryCustomers().Count > 0)
            {
                LastUpdate = dataHandler.QueryCustomers().OrderByDescending(i => i.Modified).Select(j => j.Modified.GetValueOrDefault()).First();
            }

            List<Customer> ServerCustomers = serviceHandler.CallService<List<Customer>>(@"QueryCustomers");
            if (ServerCustomers != null)
            {
                List<Customer> newCustomers = ServerCustomers.Where(i => i.Modified > LastUpdate).Select(j => j).ToList();

                foreach (var item in newCustomers)
                {
                    if (dataHandler.QueryCustomers().Where(p => p.CustomerId.Equals(item.CustomerId)).Count() == 0)
                    {
                        dataHandler.InsertCustomer(item);
                    }
                    else
                    {
                        dataHandler.UpdateCustomer(item);
                    }
                }
            };
        }

        private void SynchronizePackages()
        {

            LastUpdate = new DateTime(1983, 11, 20);

            if (dataHandler.QueryPackagesWithChocolatesAndIngredients().Count > 0)
            {
                LastUpdate = dataHandler.QueryPackagesWithChocolatesAndIngredients().OrderByDescending(i => i.Modified).Select(j => j.Modified.GetValueOrDefault()).First();
            }

            List<Package> ServerPackages = serviceHandler.CallService<List<Package>>(@"QueryPackagesWithChocolatesAndIngredients");
            if (ServerPackages != null)
            {
                List<Package> newPackages = ServerPackages.Where(i => i.Modified > LastUpdate).Select(j => j).ToList();

                foreach (var item in newPackages)
                {
                    if (dataHandler.QueryPackagesWithChocolatesAndIngredients().Where(p => p.PackageId.Equals(item.PackageId)).Count() == 0)
                    {
                        if (dataHandler.InsertPackage(item))
                            PackageInformer.Invoke();
                    }
                    else
                    {
                        if (dataHandler.UpdatePackage(item))
                            PackageInformer.Invoke();
                    }
                }
            }
        }

        private void SynchronizeCreations()
        {
            //set date to the past in case of empty table
            LastUpdate = new DateTime(1983, 11, 20);
            //get latest Date when the list was synchronized
            if (dataHandler.QueryCreations().Count > 0)
            {
                LastUpdate = dataHandler.QueryCreations().OrderByDescending(i => i.Modified).Select(j => j.Modified.GetValueOrDefault()).First();
            }
            List<Chocolate> ServerChocolate = serviceHandler.CallService<List<Chocolate>>(@"QueryChocolatesWithIngredients");
            if (ServerChocolate != null)
            {
                //save all which are new or have been updated to new List
                List<Chocolate> newChocolate = ServerChocolate.Where(i => i.Modified > LastUpdate).Select(j => j).ToList();

                foreach (var item in newChocolate)
                {
                    //in case of new
                    if (dataHandler.QueryCreations().Where(p => p.ChocolateId.Equals(item.ChocolateId)).Count() == 0)
                    {
                        if (dataHandler.InsertChocolate(item))
                            CreationInformer.Invoke();
                    }
                    //in case an existing has been updated
                    else
                    {
                        if (dataHandler.UpdateChocolate(item))
                            CreationInformer.Invoke();
                    }
                }
            }
        }


        /**
         * Updates local db using the Modiefied date in case of new or updated ingredients
         * */
        private void SynchronizeIngredients()
        {
            LastUpdate = new DateTime(1983, 11, 20);

            if (dataHandler.QueryIngredients().Count > 0)
            {
                LastUpdate = dataHandler.QueryIngredients().OrderByDescending(i => i.Modified).Select(j => j.Modified.GetValueOrDefault()).First();
            }

            List<Ingredient> ServerIngredients = serviceHandler.CallService<List<Ingredient>>(@"QueryIngredients");
            if (ServerIngredients != null)
            {
                List<Ingredient> newIngredients = ServerIngredients.Where(i => i.Modified > LastUpdate).Select(j => j).ToList();

                foreach (var item in newIngredients)
                {
                    if (dataHandler.QueryIngredients().Where(p => p.IngredientId.Equals(item.IngredientId)).Count() == 0)
                    {
                        if (dataHandler.InsertIngredient(item))
                            IngredientInformer.Invoke();
                    }
                    else
                    {
                        if (dataHandler.UpdateIngredient(item))
                            IngredientInformer.Invoke();
                    }
                }
            }
        }

        private void SynchronizeComments()
        {
            LastUpdate = new DateTime(1983, 11, 20);

            if (dataHandler.QueryRatings().Count > 0)
            {
                LastUpdate = dataHandler.QueryRatings().OrderByDescending(i => i.Modified).Select(j => j.Modified.GetValueOrDefault()).First();
            }
            List<Rating> ServerRatings = serviceHandler.CallService<List<Rating>>(@"QueryRatings");
            if (ServerRatings != null)
            {
                List<Rating> newRatings = ServerRatings.Where(i => i.Modified > LastUpdate).Select(j => j).ToList();

                foreach (var item in newRatings)
                {
                    if (dataHandler.QueryRatings().Where(p => p.RatingId.Equals(item.RatingId)).Count() == 0)
                    {
                        if (dataHandler.InsertRating(item))
                            CommentsInformer.Invoke();
                    }
                    else
                    {
                        if (dataHandler.UpdateRating(item))
                            CommentsInformer.Invoke();
                    }
                }
            }

        }
        #endregion
    }
}
