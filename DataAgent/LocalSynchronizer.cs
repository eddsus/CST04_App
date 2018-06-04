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
        private bool connected = false;
        #endregion
        public LocalSynchronizer(Action orderInformer, Action packageInformer, Action creationInformer, Action ingredientInformer, Action commentsInformer)
        {
            dataHandler = new LocalDataHandler();
            serviceHandler = new ServiceHandler("http://localhost:8733/AppServiceService/");
            OrderInformer = orderInformer;
            PackageInformer = packageInformer;
            CreationInformer = creationInformer;
            IngredientInformer = ingredientInformer;
            CommentsInformer = commentsInformer;

        }

        #region INITIALIZATION
        private bool Connected()
        {
            try
            {
                connected = (bool)serviceHandler.CallService<bool>("IsAlive");
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
            while (true)
            {
                if (Connected())
                {
                    //SyncronizeOrders();
                    //SyncronizePackages();
                    //SyncronizeCreations();
                    SyncronizeIngrdients();
                    //SynchronizeComments();
                }
                Thread.Sleep(20000);
            }
        }
        #endregion

        #region SYNC LOCAL DB AND UPDATE GUI
        private void SyncronizeOrders()
        {
            OrderInformer.Invoke();
        }
        private void SyncronizePackages()
        {
            PackageInformer.Invoke();
        }
        private void SyncronizeCreations()
        {
            CreationInformer.Invoke();
        }
        private void SyncronizeIngrdients()
        {
            //Get latest Date when the list was synchronized
            //DateTime LastIngredientUpdate = dataHandler.QueryIngredients().OrderByDescending(i => i.DatedModified).Select(j => j.DatedModified).First();
            //List<Ingredient> newIngredients = ServerIngredients.Where(i => i.DatedModified > LastIngredientUpdate).Select(j => j).ToList
            List<Ingredient> ServerIngredients = (List<Ingredient>)serviceHandler.CallService<List<Ingredient>>(@"QueryIngredients");
            if (ServerIngredients != null)
            {
                foreach (var item in ServerIngredients)
                {
                    if (dataHandler.QueryIngredients().Where(p => p.IngredientId.Equals(item.IngredientId)).Count() == 0)
                    {
                        if (dataHandler.AddIngredient(item))
                            IngredientInformer.Invoke();
                    }
                }
            }
        }
        private void SynchronizeComments()
        {
            CommentsInformer.Invoke();
        }
        #endregion
    }
}
