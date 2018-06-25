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
            //serviceHandler = new ServiceHandler("http://localhost:8090/AppServiceService/");
            serviceHandler = new ServiceHandler("http://wi-gate.technikum-wien.at:60935/AppServiceService/");
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

        public List<Customer> QueryCustomers()
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return serviceHandler.CallService<List<Customer>>(@"QueryCustomers");
            }
            else
            {
                return localDH.QueryCustomers();
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

        public List<Ingredient> QueryIngredientsByChocolateId(Guid chocolateId)
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return serviceHandler.CallService<List<Ingredient>>(@"QueryIngredientsByChocolateIdasstring/" + chocolateId.ToString());
            }
            else
            {
                return localDH.QueryIngredientsByChocolateId(chocolateId);
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
                    try
                    {
                        item.Content = new List<OrderContent>();
                        item.Content.AddRange(QueryOrdersContentChocolate(item.OrderId));
                        item.Content.AddRange(QueryOrdersContentPackage(item.OrderId));
                    }
                    catch (Exception)
                    {
                    }
                }
                return temp;
            }
            else
            {
                var temp = localDH.QueryOrders();
                foreach (var item in temp)
                {
                    item.Content = new List<OrderContent>();
                    item.Content.AddRange(localDH.QueryOrdersContentChocolate(item.OrderId));
                    item.Content.AddRange(localDH.QueryOrdersContentPackage(item.OrderId));
                }

                return temp;
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
                return localDH.QueryOrdersContentChocolate(orderId);
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
                return localDH.QueryOrdersContentPackage(orderId);
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

        public bool DeleteOrderContent<OrderContent>(string ocId, string type)
        {
            GetSynchronizerStatus();
            if (connected)
            {
                string param = ocId + "+" + type;
                return serviceHandler.CallService<bool>(@"DeleteOrderContentByContentId/" + param);
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

        public bool UpdateChocolate<Chocolate>(Chocolate item)
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return serviceHandler.CallUpdateService(@"UpdateChocolate", item);
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

        public bool UpdatePackage<Package>(Package item)
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return serviceHandler.CallUpdateService(@"UpdatePackage", item);
            }
            else
            {
                //In a future version of this project we would update the local db here but since offline updates are
                //not in the scope of this project this code ends here
                return false;
            }
        }

        public bool UpdateOrder(Order order)
        {
            GetSynchronizerStatus();
            if (connected)
            {
                Order orderLight = new Order()
                {
                    OrderId = order.OrderId,
                    Customer = order.Customer,
                    DateOfDelivery = order.DateOfDelivery,
                    DateOfOrder = order.DateOfOrder,
                    Note = order.Note,
                    Status = order.Status
                };
                return serviceHandler.CallUpdateService(@"UpdateOrder", orderLight);
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

