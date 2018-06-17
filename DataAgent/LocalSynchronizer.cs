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
            serviceHandler = new ServiceHandler("http://localhost:8733/AppServiceService/");
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
            IntitializeBaseData();
            while (true)
            {
                if (Connected())
                {
                    SetConnectionStatus(true);
                    //SyncronizeOrders();
                    //SyncronizePackages();
                    //SyncronizeCreations();
                    SyncronizeIngrdients();
                    //SynchronizeComments();
                    DisplayInformation.Invoke("Last sync: "+ DateTime.Now);
                } else
                {
                    SetConnectionStatus(false);
                }
                Thread.Sleep(10000);
            }
        }
        #region INIT BASE DATA
        private void IntitializeBaseData()
        {
            InitializeOrederStatus();
        }

        private void InitializeOrederStatus()
        {
            //Get list from server
            var serverTemplist = serviceHandler.CallService<List<OrderStatus>>(@"QueryOrderStates");
            //get local list
            var localTempList = dataHandler.QueryOrderStates();
            if(serverTemplist != null && localTempList != null)
            {
                if (serverTemplist.Count != localTempList.Count)
                {
                    //Empty local list
                    dataHandler.ClearOrderStatus();
                    //And refill it with the fresh ones
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
        //::TODO::IMPLEMENT
        private void SyncronizeOrders()
        {
            OrderInformer.Invoke();
        }
        //::TODO::IMPLEMENT
        private void SyncronizePackages()
        {
            PackageInformer.Invoke();
        }

        private void SyncronizeCreations()
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
                        if (dataHandler.AddCreation(item))
                            CreationInformer.Invoke();
                    }
                    //incase an existing has been updated
                    else
                    {
                        //delete modified item from local db
                        if (dataHandler.RemoveCreationById(item.ChocolateId))
                        {
                            //and add the new version
                            if (dataHandler.AddCreation(item))
                                CreationInformer.Invoke();
                        }
                    }
                }
            }
        }
        /**
         * Updates local db using the Modiefied date in case of new or updated ingredients
         * */
        private void SyncronizeIngrdients()
        {
            //set date to the past in case of empty table
            LastUpdate = new DateTime(1983, 11, 20);
            //get latest Date when the list was synchronized
            if (dataHandler.QueryIngredients().Count > 0)
            {
                LastUpdate = dataHandler.QueryIngredients().OrderByDescending(i => i.Modified).Select(j => j.Modified.GetValueOrDefault()).First();
            }
            List<Ingredient> ServerIngredients = serviceHandler.CallService<List<Ingredient>>(@"QueryIngredients");
            if (ServerIngredients != null)
            {
                //save all ingredients which are new or have been updated to newIngredients List
                List<Ingredient> newIngredients = ServerIngredients.Where(i => i.Modified > LastUpdate).Select(j => j).ToList();

                foreach (var item in newIngredients)
                {
                    //in case of new ingredients
                    if (dataHandler.QueryIngredients().Where(p => p.IngredientId.Equals(item.IngredientId)).Count() == 0)
                    {
                        if (dataHandler.AddIngredient(item))
                            IngredientInformer.Invoke();
                    }
                    //incase an existing ingredient has been updated
                    else
                    {
                        //delete modified item from local db
                        if (dataHandler.RemoveIngredientById(item.IngredientId))
                        {
                            //and add the new version
                            if (dataHandler.AddIngredient(item))
                                IngredientInformer.Invoke();
                        }
                    }
                }
            }
        }
        /**
         * ::TODO::IMPLEMENT
         * */
        private void SynchronizeComments()
        {
            CommentsInformer.Invoke();
        }
        #endregion
    }
}
