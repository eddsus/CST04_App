using DataAgent;
using DataHandler;
using SharedDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocalSynchronization
{
    public class LocalSynchronizer
    {
        private readonly LocalDataHandler dataHandler;
        private readonly ServiceHandler serviceHandler;

        public LocalSynchronizer()
        {
            dataHandler = new LocalDataHandler();
            serviceHandler = new ServiceHandler("http://localhost:8733/AppServiceService/");
            Task.Factory.StartNew(StartSynchronizing);
        }

        private void StartSynchronizing()
        {
            while (true)
            {
                SyncronizeIngrdients();
                Thread.Sleep(5000);
            }
        }

        private void SyncronizeIngrdients()
        {
            //Get latest Date when the list was synchronized
            DateTime LastIngredientUpdate = dataHandler.QueryIngredients().OrderByDescending(i => i.DatedModified).Select(j => j.DatedModified).First();
            List<Ingredient> ServerIngredients = (List<Ingredient>)serviceHandler.CallService<List<Ingredient>>(@"QueryIngredients");
            List<Ingredient> newIngredients = ServerIngredients.Where(i => i.DatedModified > LastIngredientUpdate).Select(j => j).ToList();
            if (newIngredients.Count > 0)
            {
                foreach (var item in newIngredients)
                {
                    dataHandler.AddIngredient(item);
                }
            }

        }
    }
}
