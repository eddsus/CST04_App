using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataHandler.Local_Database;
using SharedDataTypes;

namespace DataHandler
{
    public class SharedConverter
    {
        public SharedDataTypes.Address ConvertToSharedAddress(Local_Database.Address a)
        {
            return new SharedDataTypes.Address()
            {
                AdressId = a.ID_Address,
                City = a.City,
                HouseNumber = a.HouseNumber,
                StreetName = a.StreetName,
                Zip = a.ZIP,
                Modified = a.ModifyDate
            };
        }

        public SharedDataTypes.Chocolate ConvertToSharedChocolate(Local_Database.Chocolate choco)
        {
            if (choco != null)
            {
                return new SharedDataTypes.Chocolate()
                {
                    ChocolateId = choco.ID_Chocolate,
                    Name = choco.Name,
                    Description = choco.Description,
                    Shape = ConvertToSharedShape(choco.Shape),
                    Image = choco.Image,
                    Ingredients = new List<SharedDataTypes.Ingredient>(),
                    Available = choco.Available,
                    CustomStyle = ConvertToSharedCustomStyle(choco.CustomStyle),
                    Wrapping = ConvertToSharedWrapping(choco.Wrapping),
                    Modified = choco.ModifyDate,
                    Ratings = ConvertToSharedRatings(choco.Rating),
                    CreatedBy = ConvertToSharedCustomer(choco.Customer)
                };
            }
            else
            {
                return null;
            }
        }

        public SharedDataTypes.Customer ConvertToSharedCustomer(Local_Database.Customer c)
        {
            return new SharedDataTypes.Customer()
            {
                CustomerId = c.ID_Customer,
                FirstName = c.FirstName,
                LastName = c.LastName,
                //Address = ConvertToSharedAddress(c.Customer_has_Address.Select(a => a.Address).First()),
                Mail = c.Mail,
                PhoneNumber = c.PhoneNumber,
                Modified = c.ModifyDate
            };
        }


        private SharedDataTypes.CustomStyle ConvertToSharedCustomStyle(Local_Database.CustomStyle cs)
        {
            return new SharedDataTypes.CustomStyle
            {
                CustomStyleId = cs.ID_CustomStyle,
                Name = cs.Name,
                Description = cs.Description,
                Modified = cs.ModifyDate
            };
        }

        public SharedDataTypes.Ingredient ConvertToSharedIngredient(Local_Database.Ingredients i)
        {
            return new SharedDataTypes.Ingredient
            {
                IngredientId = i.ID_Ingredients,
                Name = i.Name,
                Description = i.Description,
                Available = i.Availability,
                Type = i.Type,
                Price = i.Price,
                UnitType = i.UnitType,
                Modified = i.ModifyDate
            };
        }

        public SharedDataTypes.Order ConvertToSharedOrder(Local_Database.Order o)
        {
            return new SharedDataTypes.Order()
            {
                OrderId = o.ID_Order,
                DateOfOrder = o.DateOfOrder,
                DateOfDelivery = o.DateOfDelivery,
                Customer = ConvertToSharedCustomer(o.Customer),
                Status = ConvertToSharedOrderStatus(o.OrderStatus),
                Note = o.Note,
                Modified = o.ModifyDate
                //Content = this has to stay empty or else everything will be fucked up
            };
        }


        internal List<SharedDataTypes.OrderContentChocolate> ConvertOrdersContentChocolate(List<Local_Database.OrderContent> dbOrderContent)
        {
            List<SharedDataTypes.OrderContentChocolate> tempSharedOrderContent = new List<SharedDataTypes.OrderContentChocolate>();
            foreach (var item in dbOrderContent)
            {
                if (item.OrderContent_has_Chocolate.Count > 0)
                    tempSharedOrderContent.Add(new OrderContentChocolate()
                    {
                        OrderContentId = item.ID_OrderContent,
                        Chocolate = ConvertToSharedChocolate(item.OrderContent_has_Chocolate.First().Chocolate),
                        Amount = item.OrderContent_has_Chocolate.First().Amount,
                        Modified = item.ModifyDate
                    });
            }
            return tempSharedOrderContent;
        }

        internal List<SharedDataTypes.OrderContentPackage> ConvertOrdersContentPackage(List<Local_Database.OrderContent> dbOrderContent)
        {
            List<SharedDataTypes.OrderContentPackage> tempSharedOrderContent = new List<SharedDataTypes.OrderContentPackage>();
            foreach (var item in dbOrderContent)
            {
                if (item.OrderContent_has_Package.Count > 0)
                    tempSharedOrderContent.Add(new OrderContentPackage()
                    {

                        OrderContentId = item.ID_OrderContent,
                        Package = ConvertToSharedPackage(item.OrderContent_has_Package.First().Package),
                        Amount = item.OrderContent_has_Package.First().Amount,
                        Modified = item.ModifyDate

                    });
            }
            return tempSharedOrderContent;
        }


        //public SharedDataTypes.OrderContent ConvertToSharedChocolateOrderContentChocolate(Local_Database.Chocolate choco)
        //{
        //    return new OrderContentChocolate
        //    {
        //        OrderContentId = choco.ID_Chocolate,
        //        Chocolate = ConvertToSharedChocolate(choco),
        //        Amount = choco.OrderContent_has_Chocolate.Select(p => p.Amount).First()
        //    };
        //}
        //public SharedDataTypes.OrderContent ConvertToSharedPackageOrderContentPackage(Local_Database.Package p)
        //{
        //    return new OrderContentPackage
        //    {
        //        OrderContentId = p.ID_Package,
        //        Package = ConvertToSharedPackage(p),
        //        Amount = p.OrderContent_has_Package.Select(q => q.Amount).First()
        //    };
        //}

        public SharedDataTypes.OrderStatus ConvertToSharedOrderStatus(Local_Database.OrderStatus os)
        {
            return new SharedDataTypes.OrderStatus()
            {
                OrderStatusId = os.ID_OrderStatus,
                Decription = os.StatusDescription,
                Modified = os.ModifyDate
            };
        }

        private List<SharedDataTypes.Chocolate> ConvertToSharedChocolateList(List<Package_has_Chocolate> list)
        {
            var temp = new List<SharedDataTypes.Chocolate>();
            foreach (var item in list)
            {
                temp.Add(ConvertToSharedChocolate(item.Chocolate));
            }
            return temp;
        }

        public SharedDataTypes.Package ConvertToSharedPackage(Local_Database.Package p)
        {
            if (p != null)
            {
                return new SharedDataTypes.Package
                {
                    PackageId = p.ID_Package,
                    Name = p.Name,
                    Description = p.Descripton,
                    Image = p.Image,
                    Customer = ConvertToSharedCustomer(p.Customer),
                    Modified = p.ModifyDate,
                    Ratings = ConvertToSharedRatings(p.Rating),
                    Wrapping = ConvertToSharedWrapping(p.Wrapping1),
                    Available = p.Availability,
                    Chocolates = ConvertToSharedChocolateList(p.Package_has_Chocolate.Select(c => c).ToList())
                };
            }
            else
            {
                return null;
            }

        }

        public List<SharedDataTypes.Rating> ConvertToSharedRatings(ICollection<Local_Database.Rating> r)
        {
            List<SharedDataTypes.Rating> tempRatings = new List<SharedDataTypes.Rating>();

            foreach (var item in r)
            {

                if (item.Chocolate != null)
                {
                    tempRatings.Add(new SharedDataTypes.Rating
                    {
                        RatingId = item.ID_Rating,
                        Value = item.Value,
                        Date = item.Date,
                        //Chocolate = ConvertToSharedChocolate(item.Chocolate),
                        ProductName = item.Chocolate.Name,
                        Comment = item.Comment,
                        Customer = ConvertToSharedCustomer(item.Customer),
                        Published = item.Published
                    });
                }
                else if (item.Package != null)
                {
                    tempRatings.Add(new SharedDataTypes.Rating
                    {
                        RatingId = item.ID_Rating,
                        Value = item.Value,
                        Date = item.Date,
                        Comment = item.Comment,
                        Customer = ConvertToSharedCustomer(item.Customer),
                        //Package = ConvertToSharedPackage(item.Package),
                        ProductName = item.Package.Name,
                        Published = item.Published
                    });
                }
            }
            return tempRatings;
        }

        public SharedDataTypes.Shape ConvertToSharedShape(Local_Database.Shape s)
        {
            return new SharedDataTypes.Shape
            {
                ShapeId = s.ID_Shape,
                Name = s.Name,
                Image = s.Image,
                Modified = s.ModifyDate
            };
        }

        public SharedDataTypes.Wrapping ConvertToSharedWrapping(Local_Database.Wrapping w)
        {
            return new SharedDataTypes.Wrapping
            {
                WrappingId = w.ID_Wrapping,
                Name = w.Name,
                Price = w.Price,
                Image = w.Image,
                Modified = w.ModifyDate
            };
        }






        #region ToDBObject
        public Local_Database.Chocolate ConvertToDBChoco(SharedDataTypes.Chocolate c)
        {
            return new Local_Database.Chocolate
            {
                ID_Chocolate = c.ChocolateId,
                Name = c.Name,
                Description = c.Description,
                Available = c.Available,
                Shape_ID = c.Shape.ShapeId,
                CustomStyle_ID = c.CustomStyle.CustomStyleId,
                Image = c.Image,
                Creator_Customer_ID = c.CreatedBy.CustomerId,
                ModifyDate = DateTime.Now,
                WrappingID = c.Wrapping.WrappingId
            };
        }

        public Local_Database.Package ConvertToDBPackage(SharedDataTypes.Package p)
        {
            return new Local_Database.Package
            {
                ID_Package = p.PackageId,
                Name = p.Name,
                Descripton = p.Description,
                WrappingID = p.Wrapping.WrappingId,
                Wrapping = p.Wrapping.Name,
                Availability = p.Available,
                Customer_ID = p.Customer.CustomerId,
                Image = p.Image,
                
                ModifyDate = DateTime.Now,
            };
        }

        public Local_Database.Ingredients ConvertToDBIngredient(SharedDataTypes.Ingredient i)
        {
            return new Local_Database.Ingredients
            {
                ID_Ingredients = i.IngredientId,
                Name = i.Name,
                Description = i.Description,
                Price = i.Price,
                Type = i.Type,
                UnitType = i.UnitType,
                Availability = i.Available,
                ModifyDate = DateTime.Now
            };
        }

        internal Package_has_Chocolate ConvertToDBPackageHasChoco(Guid chocoId, Guid packageId)
        {
            return new Package_has_Chocolate
            {
                Chocolate_ID = chocoId,
                Package_ID = packageId,
                ModifyDate = DateTime.Now
            };
        }

        internal Chocolate_has_Ingridients ConvertToDBChocolateHasIngredients(Guid chocolateId, Guid ingredientId)
        {
            return new Chocolate_has_Ingridients
            {
                Chocolazte_ID = chocolateId,
                Ingerdient_ID = ingredientId,
                ModifyDate = DateTime.Now
            };
        }

        public Local_Database.Shape ConvertToDBShape(SharedDataTypes.Shape s)
        {
            return new Local_Database.Shape
            {
                ID_Shape = s.ShapeId,
                Name = s.Name,
                Image = s.Image,
                ModifyDate = DateTime.Now
            };
        }


        public Local_Database.Wrapping ConvertToDBWrapping(SharedDataTypes.Wrapping w)
        {
            return new Local_Database.Wrapping
            {
                ID_Wrapping = w.WrappingId,
                Name = w.Name,
                Price = w.Price,
                Image = w.Image,
                ModifyDate = DateTime.Now
            };
        }

        internal Local_Database.Rating ConvertToDBRating(SharedDataTypes.Rating r)
        {
            if (r.type)
                return new Local_Database.Rating
                {
                    ID_Rating = r.RatingId,
                    Chocolate_ID = r.ProductId,
                    Customer_ID = r.Customer.CustomerId,
                    Comment = r.Comment,
                    Published = r.Published,
                    Date = r.Date,
                    ModifyDate = DateTime.Now

                };
            else
                return new Local_Database.Rating
                {
                    ID_Rating = r.RatingId,
                    Package_ID = r.ProductId,
                    Customer_ID = r.Customer.CustomerId,
                    Comment = r.Comment,
                    Published = r.Published,
                    Date = r.Date,
                    ModifyDate = DateTime.Now

                };
        }

        internal Local_Database.CustomStyle ConvertToDBCustomStyle(SharedDataTypes.CustomStyle cs)
        {
            return new Local_Database.CustomStyle
            {
                ID_CustomStyle = cs.CustomStyleId,
                Name = cs.Name,
                Description = cs.Description,
                ModifyDate = DateTime.Now
            };
        }

        public Local_Database.Customer ConvertToDBCustomer(SharedDataTypes.Customer c)
        {
            return new Local_Database.Customer
            {
                ID_Customer = c.CustomerId,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Mail = c.Mail,
                PhoneNumber = c.PhoneNumber,
                ModifyDate = DateTime.Now
            };
        }

        #endregion
    }

    //class Converter
    //{
    //    LocalDataHandler ldh = new LocalDataHandler();
    //    internal static SharedDataTypes.Chocolate ConvertToSharedChocolate(Local_Database.Chocolate c)
    //    {
    //        return new SharedDataTypes.Chocolate
    //        {
    //            ChocolateId = c.Creator_Customer_ID,
    //            Description = c.Description,
    //            Name = c.Name,
    //            Available = c.Available,
    //            CreatedBy = ldh.QueryCustomerById(c.Creator_Customer_ID),
    //            Ratings = LocalDataHandler.QueryRatingsByChocolateId(c.ID_Chocolate),
    //            CustomStyle = ConvertToSharedStyle(c.CustomStyle),
    //            Shape = ConvertToSharedShape(c.CustomStyle),
    //            Wrapping = ConvertToSharedWrapper(c.Wrapping),
    //            Ingredients = LocalDataHandler.QueryIngredientsByChocolateId(c.ID_Chocolate),
    //            Modified = c.ModifyDate
    //        };
    //    }

    //    private static SharedDataTypes.Wrapping ConvertToSharedWrapper(Local_Database.Wrapping wrapping)
    //    {
    //        var temp = new SharedDataTypes.Wrapping()
    //        {
    //            WrappingId = wrapping.ID_Wrapping,
    //            Name = wrapping.Name,
    //            Price = wrapping.Price,
    //            ImgPath = wrapping.Image
    //        };
    //        return temp;
    //    }

    //    private static SharedDataTypes.Shape ConvertToSharedShape(Local_Database.CustomStyle customStyle)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    private static SharedDataTypes.CustomStyle ConvertToSharedStyle(Local_Database.CustomStyle customStyle)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
