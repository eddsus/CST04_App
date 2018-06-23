﻿using SharedDataTypes;
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
            //serviceHandler = new ServiceHandler("http://wi-gate.technikum-wien.at:60935/AppServiceService/");
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
                var temp = serviceHandler.CallService<List<Order>>(@"QueryOrders");
                foreach (var item in temp)
                {
                    item.Content = new List<OrderContent>();
                    item.Content.AddRange(QueryOrdersContentChocolate(item.OrderId));
                    item.Content.AddRange(QueryOrdersContentPackage(item.OrderId));
                }
                return temp;
            }
            else
            {
                return localDH.QueryOrders();
            }
        }

        public List<OrderContentChocolate> QueryOrdersContentChocolate(string orderId)
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return serviceHandler.CallService<List<OrderContentChocolate>>(@"QueryOrdersContentChocolate/" + orderId);
            }
            else
            {
                return new List<OrderContentChocolate>();
            }
        }
        public List<OrderContentPackage> QueryOrdersContentPackage(string orderId)
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return serviceHandler.CallService<List<OrderContentPackage>>(@"QueryOrdersContentPackage/" + orderId);
            }
            else
            {
                return new List<OrderContentPackage>();
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

        public List<Package> QueryPackagesWithChocolatesAndIngredients()
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return serviceHandler.CallService<List<Package>>(@"QueryPackagesWithChocolatesAndIngredients");
            }
            else
            {
                return localDH.QueryPackagesWithChocolatesAndIngredients();
            }
        }

        public List<Rating> QueryRatings()
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return serviceHandler.CallService<List<Rating>>(@"QueryRatings");
            }
            else
            {
                return localDH.QueryRatings();
            }
        }

        #endregion

        #region UPDATE METHODS

        public bool DeleteOrderContent<OrderContent>(OrderContent item)
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return serviceHandler.CallUpdateService(@"DeleteOrderContentByContentId", item);
            }
            else
            {
                //In a future version of this project we would update the local db here but since offline updates are
                //not in the scope of this project this code ends here
                return false;
            }
        }
        public bool UpdateRating<Rating>(Rating item)
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return serviceHandler.CallUpdateService(@"UpdateRating", item);
            }
            else
            {
                //In a future version of this project we would update the local db here but since offline updates are
                //not in the scope of this project this code ends here
                return false;
            }
        }

        public bool UpdateIngredient<Ingredient>(Ingredient item)
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

