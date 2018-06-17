using SharedDataTypes;
using System;
using System.Collections.Generic;
using DataHandler;
using LocalSynchronization;
using System.Collections.ObjectModel;

namespace DataAgent
{
    public class DataAgentUnit
    {
        #region FIELDS
        private bool connected = false;
        private ServiceHandler serviceHandler;
        private LocalDataHandler localDH;
        private static DataAgentUnit dataAgent;
        #endregion

        private DataAgentUnit()
        {
            serviceHandler = new ServiceHandler("http://localhost:8733/AppServiceService/");
            localDH = new LocalDataHandler();
            GetSynchronizerStatus();
        }

        #region DataAgentHandling
        public static DataAgentUnit GetInstance()
        {
            if (dataAgent == null)
            {
                dataAgent = new DataAgentUnit();
            }
            return dataAgent;
        }

        public bool GetSynchronizerStatus()
        {
            try
            {
                connected = ((bool)serviceHandler.CallService<bool>(@"IsAlive"));
            }
            catch (Exception e)
            {
                connected = false;
            }
            return connected;
        }
        #endregion

        #region QUERIES
        public List<Ingredient> QueryIngredients()
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return serviceHandler.CallService<List<Ingredient>>(@"QueryIngredients");
            }
            else
            {
                return localDH.QueryIngredients();
            }
        }

        public List<Shape> QueryShapes()
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return serviceHandler.CallService<List<Shape>>(@"QueryShapes");
            }
            else
            {
                return localDH.QueryShapes();
            }
        }


        public List<Wrapping> QueryWrappings()
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return serviceHandler.CallService<List<Wrapping>>(@"QueryWrappings");
            }
            else
            {
                return localDH.QueryWrappings();
            }
        }

        public List<Chocolate> QueryCreations()
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return serviceHandler.CallService<List<Chocolate>>(@"QueryChocolatesWithIngredients");
            }
            else
            {
                return localDH.QueryCreations();
            }
        }

        public List<Order> QueryOrders()
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return serviceHandler.CallService<List<Order>>(@"QueryOrders");
            }
            else
            {
                return localDH.QueryOrders();
            }
        }

        public List<OrderStatus> QueryOrderStates()
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return serviceHandler.CallService<List<OrderStatus>>(@"QueryOrderStates");
            }
            else
            {
                return localDH.QueryOrderStates();
            }
        }
        #endregion

        #region UPDATE METHODS
        public bool UpdateIngredient<T>(T item)
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return serviceHandler.CallUpdateService(@"UpdateIngredient", item);
            }
            else
            {
                //In a future version of this project we would update the local db here but since offline updates are
                //not in the scope of this project this code ends here
                return false;
            }
        }
        #endregion


    }
}
