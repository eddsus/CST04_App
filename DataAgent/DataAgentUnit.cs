using SharedDataTypes;
using System;
using System.Collections.Generic;
using DataHandler;
using LocalSynchronization;

namespace DataAgent
{
    public class DataAgentUnit
    {
        private bool connected = false;
        private ServiceHandler serviceHandler;
        private LocalDataHandler localDH = new LocalDataHandler();

        public DataAgentUnit()
        {
            serviceHandler = new ServiceHandler("http://localhost:8733/AppServiceService/");
            GetSynchronizerStatus();
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

        public List<Ingredient> QueryIngredients()
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return (List<Ingredient>)serviceHandler.CallService<List<Ingredient>>(@"QueryIngredients");
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
                return (List<Shape>)serviceHandler.CallService<List<Shape>>(@"QueryShapes");
            }
            else
            {
                return new List<Shape>();
            }
        }

        public List<Wrapping> QueryWrappings()
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return (List<Wrapping>)serviceHandler.CallService<List<Wrapping>>(@"QueryWrappings");
            }
            else
            {
                return new List<Wrapping>();
            }
        }

        public List<Order> QueryOrders()
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return (List<Order>)serviceHandler.CallService<List<Order>>(@"QueryOrders");
            }
            else
            {
                return new List<Order>();
            }
        }

    }
}
