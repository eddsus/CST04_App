using DataHandler.Local_Database;
using SharedDataTypes;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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
            List<SharedDataTypes.Order> tempSharedOrders = new List<SharedDataTypes.Order>();
            foreach (var item in localDb.Order.Select(p => p).ToList())
            {
                tempSharedOrders.Add(converter.ConvertToSharedOrder(item));
            }
            return tempSharedOrders;

        }

        public List<SharedDataTypes.Customer> QueryCustomers()
        {
            List<SharedDataTypes.Customer> tempCustomers = new List<SharedDataTypes.Customer>();
            foreach (var item in localDb.Customer.Select(p => p).ToList())
            {
                tempCustomers.Add(converter.ConvertToSharedCustomer(item));
            }
            return tempCustomers;
        }


        //private List<SharedDataTypes.Order> QueryOrder()
        //{
        //    List<SharedDataTypes.Order> tempSharedOrders = new List<SharedDataTypes.Order>();
        //    foreach (var item in localDb.Order.Select(p => p).ToList())
        //    {
        //        tempSharedOrders.Add(converter.ConvertToSharedOrder(item));
        //    }
        //    foreach (var tempOrder in tempSharedOrders)
        //    {
        //        foreach (var tempOrderContent in localDb.OrderContent.Where(p => p.Order_ID.Equals(tempOrder.OrderId)).Select(p => p).ToList())
        //        {
        //            //cnt++;
        //            if (localDb.Chocolate.Where(p => p.OrderContent_has_Chocolate.Count(x => x.OrderContent_ID == tempOrderContent.ID_OrderContent) > 0) == null)
        //            {
        //                //get DBChocolate and ConvertToSharedChocolateOrderContentChocolate
        //                tempOrder.Content.Add(converter.ConvertToSharedChocolateOrderContentChocolate(
        //                    localDb.Chocolate.Where(p => p.OrderContent_has_Chocolate.Count(x => x.OrderContent_ID == tempOrderContent.ID_OrderContent) > 0).
        //                    Select(p => p).First()));
        //            }
        //            else
        //            {
        //                //get DBPackage and ConvertToSharedPackageOrderContentPackage
        //                tempOrder.Content.Add(converter.ConvertToSharedPackageOrderContentPackage(
        //                    localDb.Package.Where(p => p.OrderContent_has_Package.Count(x => x.OrderContent_ID == tempOrderContent.ID_OrderContent) > 0).
        //                    Select(p => p).First()));
        //            }
        //        }
        //    }
        //    return tempSharedOrders;
        //}

        public List<SharedDataTypes.OrderContentChocolate> QueryOrdersContentChocolate(string orderId)
        {
            var temp = localDb.OrderContent.Where(o => o.Order_ID.Equals(orderId)).Select(oc => oc).ToList();
            return converter.ConvertOrdersContentChocolate(temp);
        }

        public List<SharedDataTypes.OrderContentPackage> QueryOrdersContentPackage(string orderId)
        {
            var temp = localDb.OrderContent.Where(o => o.Order_ID.Equals(orderId)).Select(oc => oc).ToList();
            return converter.ConvertOrdersContentPackage(temp);
        }


        public SharedDataTypes.Customer QueryCustomerByPackageId(Guid packageId)
        {
            //couldnt access customer object from localDb.package table
            Guid customerId = localDb.Package.Where(p => p.ID_Package == packageId).Select(p => p.Customer_ID).First();
            return converter.ConvertToSharedCustomer(localDb.Customer.Where(p => p.ID_Customer.Equals(customerId)).Select(p => p).First());
        }

        public List<SharedDataTypes.Chocolate> QueryCreations()
        {
            List<SharedDataTypes.Chocolate> sharedChocolates = new List<SharedDataTypes.Chocolate>();

            foreach (var choco in localDb.Chocolate.Select(p => p).ToList())
            {
                sharedChocolates.Add(converter.ConvertToSharedChocolate(choco));
            }

            foreach (var tempChoco in sharedChocolates)
            {
                tempChoco.Ingredients = QueryIngredientsByChocolateId(tempChoco.ChocolateId);
            }
            return sharedChocolates;
        }


        public List<SharedDataTypes.Ingredient> QueryIngredientsByChocolateId(Guid chocoId)
        {
            List<SharedDataTypes.Ingredient> tempIngredients = new List<SharedDataTypes.Ingredient>();

            foreach (var item in localDb.Ingredients.Where(p => p.Chocolate_has_Ingridients.Count(x => x.Chocolazte_ID.Equals(chocoId)) > 0).ToList())
            {
                tempIngredients.Add(converter.ConvertToSharedIngredient(item));
            }
            return tempIngredients;
        }

        public List<SharedDataTypes.CustomStyle> QueryCustomStyles()
        {
            List<SharedDataTypes.CustomStyle> tempCs = new List<SharedDataTypes.CustomStyle>();

            foreach (var item in localDb.CustomStyle.Select(p => p))
            {
                tempCs.Add(converter.ConvertToSharedCustomerStyle(item));
            }

            return tempCs;
        }

        internal SharedDataTypes.Customer QueryCustomerById(Guid creator_Customer_ID)
        {
            return converter.ConvertToSharedCustomer(localDb.Customer.Where(p => p.ID_Customer.Equals(creator_Customer_ID)).Select(p => p).First());
        }

        public List<Ingredient> QueryIngredients()
        {
            List<SharedDataTypes.Ingredient> tempIngredients = new List<SharedDataTypes.Ingredient>();

            foreach (Local_Database.Ingredients item in localDb.Ingredients.Select(i => i).ToList())
            {
                tempIngredients.Add(converter.ConvertToSharedIngredient(item));
            }
            return tempIngredients;
        }


        public List<SharedDataTypes.Shape> QueryShapes()
        {
            List<SharedDataTypes.Shape> tempShapes = new List<SharedDataTypes.Shape>();

            foreach (var item in localDb.Shape.Select(p => p).ToList())
            {
                tempShapes.Add(converter.ConvertToSharedShape(item));
            }
            return tempShapes;
        }

        public List<SharedDataTypes.Rating> QueryRatings()
        {
            return converter.ConvertToSharedRatings(localDb.Rating.Select(p => p).ToList());

        }

        public List<SharedDataTypes.Package> QueryPackagesWithChocolatesAndIngredients()
        {
            List<SharedDataTypes.Package> tempSharedPackages = new List<SharedDataTypes.Package>();

            foreach (var item in localDb.Package.Select(p => p).ToList())
            {
                tempSharedPackages.Add(converter.ConvertToSharedPackage(item));
            }

            foreach (var item in tempSharedPackages)
            {
                item.Customer = QueryCustomerByPackageId(item.PackageId);
                item.Chocolates = QueryChocolatesWithIngredientsByPackageId(item.PackageId);
            }
            return tempSharedPackages;
        }


        public List<SharedDataTypes.Chocolate> QueryChocolatesWithIngredientsByPackageId(Guid packageId)
        {
            List<SharedDataTypes.Chocolate> sharedChocolates = new List<SharedDataTypes.Chocolate>();

            foreach (var choco in localDb.Chocolate.Where(p => p.Package_has_Chocolate.Count(q => q.Package_ID.Equals(packageId)) > 0).Select(p => p).ToList())
            {
                sharedChocolates.Add(converter.ConvertToSharedChocolate(choco));
            }


            foreach (var tempChoco in sharedChocolates)
            {
                tempChoco.Ingredients = QueryIngredientsByChocolateId(tempChoco.ChocolateId);
            }
            return sharedChocolates;
        }


        public List<SharedDataTypes.Wrapping> QueryWrappings()
        {
            List<SharedDataTypes.Wrapping> sharedWrappings = new List<SharedDataTypes.Wrapping>();

            foreach (var item in localDb.Wrapping.Select(p => p).ToList())
            {
                sharedWrappings.Add(converter.ConvertToSharedWrapping(item));
            }
            return sharedWrappings;
        }

        public List<SharedDataTypes.OrderStatus> QueryOrderStates()
        {
            List<SharedDataTypes.OrderStatus> tempSharedOrderStates = new List<SharedDataTypes.OrderStatus>();
            foreach (Local_Database.OrderStatus item in localDb.OrderStatus.Select(p => p).ToList())
            {
                tempSharedOrderStates.Add(converter.ConvertToSharedOrderStatus(item));
            }
            return tempSharedOrderStates;
        }

        //internal static List<Ingredient> QueryIngredientsByChocolateId(Guid iD_Chocolate)
        //{
        //    throw new NotImplementedException();
        //}

        //internal static List<SharedDataTypes.Rating> QueryRatingsByChocolateId(Guid iD_Chocolate)
        //{
        //    throw new NotImplementedException();
        //}

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
            if (temp != null)
                localDb.Ingredients.Remove(temp);
            return localDb.SaveChanges() > 0;
        }
        public void ClearOrderStatus()
        {
            //remove all entries
            localDb.OrderStatus.RemoveRange(localDb.OrderStatus.Select(o => o));
            localDb.SaveChanges();
        }

        public void ClearShapes()
        {
            localDb.OrderStatus.RemoveRange(localDb.OrderStatus.Select(p => p));
            localDb.SaveChanges();
        }

        public void ClearWrappings()
        {
            localDb.Wrapping.RemoveRange(localDb.Wrapping.Select(p => p));
            localDb.SaveChanges();
        }

        public void ClearCustomStyles()
        {
            localDb.CustomStyle.RemoveRange(localDb.CustomStyle.Select(p => p));
            localDb.SaveChanges();
        }
        #endregion 

        //not all are implemented
        #region UPDATE METHODS 
        public bool UpdateRating(SharedDataTypes.Rating r)
        {
            var temp = localDb.Rating.Where(p => p.ID_Rating.Equals(r.RatingId)).Select(p => p).First();

            temp.Value = r.Value;
            temp.Date = r.Date;
            if (temp.Package != null)
                temp.Package_ID = r.ProductId;
            if (temp.Chocolate != null)
                temp.Chocolate_ID = r.ProductId;
            temp.Customer_ID = r.Customer.CustomerId;
            temp.Comment = r.Comment;
            temp.Published = r.Published;
            temp.ModifyDate = DateTime.Now;

            return localDb.SaveChanges() == 1;
        }

        public bool UpdateIngredient(SharedDataTypes.Ingredient i)
        {
            var temp = localDb.Ingredients.Where(p => p.ID_Ingredients == i.IngredientId).Select(j => j).First();

            temp.Name = i.Name;
            temp.Description = i.Description;
            temp.Price = i.Price;
            temp.Availability = i.Available;
            temp.Type = i.Type;
            temp.UnitType = i.UnitType;
            temp.ModifyDate = DateTime.Now;
            return localDb.SaveChanges() == 1;
        }

        public bool UpdateChocolate(SharedDataTypes.Chocolate c)
        {

            var temp = localDb.Chocolate.Where(p => p.ID_Chocolate.Equals(c.ChocolateId)).Select(p => p).First();

            temp.Name = c.Name;
            temp.Description = c.Description;
            temp.Available = c.Available;
            temp.Image = c.Image;
            temp.WrappingID = c.Wrapping.WrappingId;
            temp.Shape_ID = c.Shape.ShapeId;
            temp.Creator_Customer_ID = c.CreatedBy.CustomerId;
            temp.ModifyDate = DateTime.Now;

            return localDb.SaveChanges() == 1;
        }

        public bool UpdateOrder(SharedDataTypes.Order o)
        {
            var temp = localDb.Order.Where(p => p.ID_Order.Equals(o.OrderId)).Select(p => p).First();

            temp.DateOfOrder = o.DateOfOrder;
            temp.DateOfDelivery = o.DateOfDelivery;
            temp.Status_ID = o.Status.OrderStatusId;
            temp.Customer_ID = o.Customer.CustomerId;
            temp.Note = o.Note;
            temp.ModifyDate = DateTime.Now;
            //test
            localDb.SaveChanges();

            //foreach (SharedDataTypes.OrderContent item in o.Content)
            //{
            //    var a = localDb.OrderContent.Where(p => p.ID_OrderContent.Equals(item.OrderContentId)).First();
            //    a.ID_OrderContent = item.OrderContentId;
            //    a.Order_ID = o.OrderId;
            //    //test
            //    localDb.SaveChanges();
            //}
            return localDb.SaveChanges() == 1;
        }

        public bool UpdatePackage(SharedDataTypes.Package p)
        {
            var temp = localDb.Package.Where(q => q.ID_Package.Equals(q.ID_Package)).Select(q => q).First();

            temp.Name = p.Name;
            temp.Descripton = p.Description;
            temp.WrappingID = p.Wrapping.WrappingId;
            temp.Availability = p.Available;
            temp.Customer_ID = p.Customer.CustomerId;
            temp.Wrapping = p.Wrapping.Name;
            temp.Image = p.Image;
            temp.ModifyDate = DateTime.Now;

            return localDb.SaveChanges() == 1;


        }
        public bool UpdateCustomer(SharedDataTypes.Customer c)
        {
            var temp = localDb.Customer.Where(p => p.ID_Customer.Equals(c.CustomerId)).Select(p => p).First();

            temp.ID_Customer = c.CustomerId;
            temp.FirstName = c.FirstName;
            temp.LastName = c.LastName;
            temp.Mail = c.Mail;
            temp.PhoneNumber = c.PhoneNumber;
            temp.ModifyDate = DateTime.Now;

            return localDb.SaveChanges() == 1;

        }

        public bool UpdateOrderContent(SharedDataTypes.OrderContent oc, string orderId)
        {
            var temp = localDb.OrderContent.Where(p => p.ID_OrderContent.Equals(oc.OrderContentId)).Select(p => p).First();

            temp.ID_OrderContent = oc.OrderContentId;
            temp.Order_ID = orderId;
            temp.ModifyDate = DateTime.Now;

            return localDb.SaveChanges() == 1;
        }

        public bool UpdateOcHasPackage(OrderContentPackage ocPackage)
        {
            var temp = localDb.OrderContent_has_Package.Where(p => p.OrderContent_ID.Equals(ocPackage.OrderContentId)).Select(p => p).First(); ;

            temp.OrderContent_ID = ocPackage.OrderContentId;
            temp.Package_ID = ocPackage.Package.PackageId;
            temp.ModifyDate = DateTime.Now;

            return localDb.SaveChanges() == 1;
        }


        public bool UpdateOcHasChoco(OrderContentChocolate ocChoco)
        {
            var temp = localDb.OrderContent_has_Chocolate.Where(p => p.OrderContent_ID.Equals(ocChoco.OrderContentId)).Select(p => p).First(); ;

            temp.OrderContent_ID = ocChoco.OrderContentId;
            temp.Chocolate_ID = ocChoco.Chocolate.ChocolateId;
            temp.ModifyDate = DateTime.Now;

            return localDb.SaveChanges() == 1;
        }

        //public void UpdateChocoById(Guid chocolateId)
        //{
        //    throw new NotImplementedException();
        //}

        #endregion


        #region INSERT METHODS


        public bool InsertOcHasPackage(OrderContentPackage ocPackage)
        {
            localDb.OrderContent_has_Package.Add(converter.ConvertToDBOcHasPackage(ocPackage, ocPackage.Package));

            return localDb.SaveChanges() == 1;
        }
        public bool InsertOcHasChoco(OrderContentChocolate ocChoco)
        {
            localDb.OrderContent_has_Chocolate.Add(converter.ConvertToDBOcHasChoco(ocChoco, ocChoco.Chocolate));

            return localDb.SaveChanges() == 1;
        }
        public bool InsertOrderContent(SharedDataTypes.OrderContent oc, string orderId)
        {
            localDb.OrderContent.Add(converter.ConvertToDBOrderContent(oc, orderId));

            return localDb.SaveChanges() == 1;
        }

        public bool InsertOrder(SharedDataTypes.Order o)
        {
            localDb.Order.Add(converter.ConvertToDBOrder(o));

            return localDb.SaveChanges() == 1;
        }

        public bool InsertCustomer(SharedDataTypes.Customer c)
        {
            localDb.Customer.Add(converter.ConvertToDBCustomer(c));
            return localDb.SaveChanges() == 1;
        }

        public bool InsertChocolate(SharedDataTypes.Chocolate c)
        {
            localDb.Chocolate.Add(converter.ConvertToDBChoco(c));
            localDb.SaveChanges();
            int cnt = 1;

            foreach (var item in c.Ingredients)
            {
                localDb.Chocolate_has_Ingridients.Add(converter.ConvertToDBChocolateHasIngredients(c.ChocolateId, item.IngredientId));
                localDb.SaveChanges();
                cnt++;
            }

            return localDb.SaveChanges() == cnt;

        }

        public bool InsertRating(SharedDataTypes.Rating r)
        {
            localDb.Rating.Add(converter.ConvertToDBRating(r));
            return localDb.SaveChanges() == 1;
        }

        public bool InsertPackage(SharedDataTypes.Package p)
        {
            localDb.Package.Add(converter.ConvertToDBPackage(p));
            int cnt = 1;
            foreach (var item in p.Chocolates)
            {
                cnt++;
                localDb.Package_has_Chocolate.Add(converter.ConvertToDBPackageHasChoco(item.ChocolateId, p.PackageId));
            }
            return localDb.SaveChanges() == cnt;
        }

        public bool InsertShape(SharedDataTypes.Shape s)
        {
            localDb.Shape.Add(converter.ConvertToDBShape(s));
            return localDb.SaveChanges() == 1;
        }

        public bool InsertWrapping(SharedDataTypes.Wrapping w)
        {
            localDb.Wrapping.Add(converter.ConvertToDBWrapping(w));
            return localDb.SaveChanges() == 1;
        }

        public bool InsertIngredient(SharedDataTypes.Ingredient i)
        {
            localDb.Ingredients.Add(converter.ConvertToDBIngredient(i));
            return localDb.SaveChanges() == 1;
        }

        public bool InsertCustomStyle(SharedDataTypes.CustomStyle cs)
        {
            localDb.CustomStyle.Add(converter.ConvertToDBCustomStyle(cs));
            return localDb.SaveChanges() == 1;
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

        //public void InsertOrder(SharedDataTypes.Order o)
        //{
        //    //insert into order, oc, oc has package/oc has choco

        //    localDb.Order.Add(converter.ConvertToDBOrder(o));
        //    localDb.SaveChanges();

        //    localDb.OrderContent.Add(converter);
        //    int cnt = 1;


        //    foreach (var item in o.Content)
        //    {
        //        if (item)

        //            cnt++;
        //    }

        //    return localDb.SaveChanges() > 1;
        //}

        #endregion




    }
}
