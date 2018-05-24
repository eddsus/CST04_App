using DataAgent.SR_Synchronizer;
using SharedDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataAgent
{
    public class DataAgentUnit
    {
        bool connected = false;
        AppServiceServiceClient synchronizer = new AppServiceServiceClient();


        public DataAgentUnit()
        {
            GetSynchronizerStatus();
        }
        public bool GetSynchronizerStatus()
        {
            try
            {
                connected = synchronizer.IsAlive();
            }
            catch (Exception)
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
                return synchronizer.QueryIngredients();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public List<Shape> QueryShapes()
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return synchronizer.QueryShapes();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public List<Wrapping> QueryWrappings()
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return synchronizer.QueryWrappings();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public List<Order> QueryOrders()
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return synchronizer.QueryOrders();
            }
            else
            {
                throw new NotImplementedException();
            }
        }



    }
}
