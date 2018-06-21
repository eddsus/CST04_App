using DataHandler.Local_Database;
using SharedDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    public class LocalDataHandler
    {
        LocalDbEntities localDb = new LocalDbEntities();
        SharedConverter converter = new SharedConverter();

        #region QUERIES

        /***
         * Not in use at the moment
         * ***/
        public List<SharedDataTypes.Order> QueryOrders()
        {
            var temp = localDb.Order.Select(o => o).ToList();
            var tempShared = new List<SharedDataTypes.Order>();
            foreach (var item in temp)
            {
                tempShared.Add(converter.ConvertToSharedOrder(item));
            }
            //fill OrderContent
            foreach (var item in tempShared)
            {
                item.Content = QueryOrderContentByOrderId(item.OrderId);
            }
            return tempShared;

        }

        private List<SharedDataTypes.OrderContent> QueryOrderContentByOrderId(string orderId)
        {
            throw new NotImplementedException();
        }

        private List<SharedDataTypes.Order> QueryOrder()
        {
            List<SharedDataTypes.Order> tempSharedOrders = new List<SharedDataTypes.Order>();
            foreach (var item in localDb.Order.Select(p => p).ToList())
            {
                tempSharedOrders.Add(converter.ConvertToSharedOrder(item));
            }
            foreach (var tempOrder in tempSharedOrders)
            {
                foreach (var tempOrderContent in localDb.OrderContent.Where(p => p.Order_ID.Equals(tempOrder.OrderId)).Select(p => p).ToList())
                {
                    //cnt++;
                    if (localDb.Chocolate.Where(p => p.OrderContent_has_Chocolate.Count(x => x.OrderContent_ID == tempOrderContent.ID_OrderContent) > 0) == null)
                    {
                        //get DBChocolate and ConvertToSharedChocolateOrderContentChocolate
                        tempOrder.Content.Add(converter.ConvertToSharedChocolateOrderContentChocolate(
                            localDb.Chocolate.Where(p => p.OrderContent_has_Chocolate.Count(x => x.OrderContent_ID == tempOrderContent.ID_OrderContent) > 0).
                            Select(p => p).First()));
                    }
                    else
                    {
                        //get DBPackage and ConvertToSharedPackageOrderContentPackage
                        tempOrder.Content.Add(converter.ConvertToSharedPackageOrderContentPackage(
                            localDb.Package.Where(p => p.OrderContent_has_Package.Count(x => x.OrderContent_ID == tempOrderContent.ID_OrderContent) > 0).
                            Select(p => p).First()));
                    }
                }
            }
            return tempSharedOrders;
        }
    

        public List<SharedDataTypes.Chocolate> QueryCreations()
        {
            var temp = localDb.Chocolate.Select(c =>c).ToList();
            var tempShared = new List<SharedDataTypes.Chocolate>();
            foreach (var item in temp)
            {
                tempShared.Add(converter.ConvertToSharedChocolate(item));
            }
            return tempShared;
        }

        //internal static List<Ingredient> QueryIngredientsByChocolateId(Guid iD_Chocolate)
        //{
        //    throw new NotImplementedException();
        //}

        //internal static List<SharedDataTypes.Rating> QueryRatingsByChocolateId(Guid iD_Chocolate)
        //{
        //    throw new NotImplementedException();
        //}

        internal SharedDataTypes.Customer QueryCustomerById(Guid creator_Customer_ID)
        {
            var temp = localDb.Customer.Where(c => c.ID_Customer == creator_Customer_ID).Select(mc => mc).First();
            return new SharedDataTypes.Customer() {
                CustomerId = temp.ID_Customer,
                FirstName = temp.FirstName,
                LastName = temp.LastName,
                Mail = temp.Mail,
                PhoneNumber = temp.PhoneNumber
                // ::TODO::Adress
            };
        }

        public List<Ingredient> QueryIngredients()
        {
            var temp = localDb.Ingredients.Select(i => new Ingredient()
            {
                IngredientId = i.ID_Ingredients,
                Name = i.Name,
                Description = i.Description,
                Available = i.Availability,
                Price = i.Price,
                Type = i.Type,
                UnitType = i.UnitType,
                Modified = i.ModifyDate
            }).ToList();
            return temp;
        }
        public List<SharedDataTypes.Shape> QueryShapes()
        {
            throw new NotImplementedException();
        }

        public List<SharedDataTypes.Rating> QueryRatings()
        {
            throw new NotImplementedException();
            //writeCode
        }

        public List<SharedDataTypes.Package> QueryPackagesWithChocolatesAndIngredients()
        {
            //throw new NotImplementedException();
            return new List<SharedDataTypes.Package>();
        }

        public List<SharedDataTypes.Wrapping> QueryWrappings()
        {
            //throw new NotImplementedException();
            return new List<SharedDataTypes.Wrapping>();
        }



        public List<SharedDataTypes.OrderStatus> QueryOrderStates()
        {
            var temp = localDb.OrderStatus.Select(i => new SharedDataTypes.OrderStatus()
            {
                OrderStatusId= i.ID_OrderStatus,
                Decription = i.StatusDescription
            }).ToList();
            return temp;
        }


        #endregion

        #region INSERTS
        public bool AddCreation(SharedDataTypes.Chocolate item)
        {
            throw new NotImplementedException();
        }
        public bool AddIngredient(Ingredient toBeAdded)
        {
            localDb.Ingredients.Add(new Ingredients()
            {
                ID_Ingredients = toBeAdded.IngredientId,
                Name = toBeAdded.Name,
                Description = toBeAdded.Description,
                Price = toBeAdded.Price,
                Availability = toBeAdded.Available,
                Type = toBeAdded.Type,
                UnitType = toBeAdded.UnitType,
                ModifyDate = DateTime.Now
            });
            return localDb.SaveChanges() > 0;
        }
        public bool AddOrderStatus(SharedDataTypes.OrderStatus toBeAdded)
        {
            localDb.OrderStatus.Add(new Local_Database.OrderStatus()
            {
                ID_OrderStatus = toBeAdded.OrderStatusId,
                StatusDescription = toBeAdded.Decription
            });
            return localDb.SaveChanges() > 0;
        }
        #endregion

        #region REMOVES
        public bool RemoveCreationById(Guid chocolateId)
        {
            throw new NotImplementedException();
        }
        public bool RemoveIngredientById(Guid ingredientId)
        {
            //get the item that will be removed
            var temp = localDb.Ingredients.Where(i => i.ID_Ingredients == ingredientId).Select(j => j).First();
            if(temp != null)
                localDb.Ingredients.Remove(temp);
            return localDb.SaveChanges() > 0;
        }
        public void ClearOrderStatus()
        {
            //remove all entries
            localDb.OrderStatus.RemoveRange(localDb.OrderStatus.Select(o => o));
            localDb.SaveChanges();
        }
        #endregion

        #region CONVERTERS

        #endregion
    }
}
