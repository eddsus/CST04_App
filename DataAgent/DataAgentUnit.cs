using Newtonsoft.Json;
using SharedDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.ServiceModel;

namespace DataAgent
{
    public class DataAgentUnit
    {
        bool connected = false;
        ServiceHandler serviceHandler;


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
                throw new NotImplementedException();
            }
        }

        //public List<Shape> QueryShapes()
        //{
        //    GetSynchronizerStatus();
        //    if (connected)
        //    {
        //        return sync.QueryShapes();
        //    }
        //    else
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public List<Wrapping> QueryWrappings()
        //{
        //    GetSynchronizerStatus();
        //    if (connected)
        //    {
        //        return sync.QueryWrappings();
        //    }
        //    else
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

        //public List<Order> QueryOrders()
        //{
        //    GetSynchronizerStatus();
        //    if (connected)
        //    {
        //        return sync.QueryOrders();
        //    }
        //    else
        //    {
        //        throw new NotImplementedException();
        //    }
        //}



    }
}
