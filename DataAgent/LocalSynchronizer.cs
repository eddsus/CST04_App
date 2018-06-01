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
        private readonly LocalDataHandler dataHandler;
        private readonly ServiceHandler serviceHandler;
        private Action JustSyncronized;

        public LocalSynchronizer(Action informer)
        {
            dataHandler = new LocalDataHandler();
            serviceHandler = new ServiceHandler("http://localhost:8733/AppServiceService/");
            this.JustSyncronized = informer;
        }

        internal void StartSyncing()
        {
            Task.Factory.StartNew(StartSynchronizing);
        }

        private void StartSynchronizing()
        {
            while (true)
            {
                SyncronizeIngrdients();
                //SynchronizeComments();
                Thread.Sleep(5000);
            }
        }

        private void SynchronizeComments()
        {
            throw new NotImplementedException();
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
                            JustSyncronized.Invoke();
                    }
                }
            }
        }
    }
}
