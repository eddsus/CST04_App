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
                Zip = a.ZIP
            };
        }

        public SharedDataTypes.Chocolate ConvertToSharedChocolate(Local_Database.Chocolate choco)
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

        public SharedDataTypes.Customer ConvertToSharedCustomer(Local_Database.Customer c)
        {
            return new SharedDataTypes.Customer()
            {
                CustomerId = c.ID_Customer,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Address = ConvertToSharedAddress(c.Address.First()),
                Mail = c.Mail,
                PhoneNumber = c.PhoneNumber
            };
        }
  
        public SharedDataTypes.OrderContent ConvertToSharedChocolateOrderContentChocolate(Local_Database.Chocolate choco)
        {
            return new OrderContentChocolate
            {
                OrderContentId = choco.ID_Chocolate,
                Chocolate = ConvertToSharedChocolate(choco),
                Amount = choco.OrderContent_has_Chocolate.Select(p => p.Amount).First()
            };
        }

        private SharedDataTypes.CustomStyle ConvertToSharedCustomStyle(Local_Database.CustomStyle cs)
        {
            return new SharedDataTypes.CustomStyle
            {
                CustomStyleId = cs.ID_CustomStyle,
                Name = cs.Name,
                Description = cs.Description
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
                Content = new List<SharedDataTypes.OrderContent>()
            };
        }

        public SharedDataTypes.OrderContent ConvertToSharedPackageOrderContentPackage(Local_Database.Package p)
        {
            return new OrderContentPackage
            {
                OrderContentId = p.ID_Package,
                Package = ConvertToSharedPackage(p),
                Amount = p.OrderContent_has_Package.Select(q => q.Amount).First()
            };
        }

        public SharedDataTypes.OrderStatus ConvertToSharedOrderStatus(Local_Database.OrderStatus os)
        {
            return new SharedDataTypes.OrderStatus()
            {
                OrderStatusId = os.ID_OrderStatus,
                Decription = os.StatusDescription
            };
        }

        public SharedDataTypes.Package ConvertToSharedPackage(Local_Database.Package p)
        {
            return new SharedDataTypes.Package
            {
                PackageId = p.ID_Package,
                Name = p.Name,
                Description = p.Descripton,
                Image = p.Image,
                Customer = new SharedDataTypes.Customer(),
                Modified = p.ModifyDate,
                Ratings = new List<SharedDataTypes.Rating>(),
                Wrapping = ConvertToSharedWrapping(p.Wrapping1),
                Available = p.Availability,
                Chocolates = new List<SharedDataTypes.Chocolate>()
            };

        }

        public List<SharedDataTypes.Rating> ConvertToSharedRatings(ICollection<Local_Database.Rating> r)
        {
            List<SharedDataTypes.Rating> tempRatings = new List<SharedDataTypes.Rating>();

            foreach (var item in r)
            {
                tempRatings.Add(new SharedDataTypes.Rating
                {
                    RatingId = item.ID_Rating,
                    Value = item.Value,
                    Date = item.Date,
                    Chocolate = ConvertToSharedChocolate(item.Chocolate),
                    Comment = item.Comment,
                    Customer = ConvertToSharedCustomer(item.Customer),
                    Package = ConvertToSharedPackage(item.Package),
                    Published = item.Published
                });
            }
            return tempRatings;
        }

        public SharedDataTypes.Shape ConvertToSharedShape(Local_Database.Shape s)
        {
            return new SharedDataTypes.Shape
            {
                ShapeId = s.ID_Shape,
                Name = s.Name,
                Image = s.Image
            };
        }

        public SharedDataTypes.Wrapping ConvertToSharedWrapping(Local_Database.Wrapping w)
        {
            return new SharedDataTypes.Wrapping
            {
                WrappingId = w.ID_Wrapping,
                Name = w.Name,
                Price = w.Price,
                Image = w.Image
            };
        }

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
                ModifyDate = c.Modified.GetValueOrDefault(),
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
                Availability = p.Available,
                Customer_ID = p.Customer.CustomerId,
                Image = p.Image,
                ModifyDate = p.Modified.GetValueOrDefault(),
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
                ModifyDate = i.Modified
            };
        }

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
