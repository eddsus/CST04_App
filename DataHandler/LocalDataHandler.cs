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

        #region QUERIES

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

        public bool AddOrderStatus(SharedDataTypes.OrderStatus toBeAdded)
        {
            localDb.OrderStatus.Add(new Local_Database.OrderStatus()
            {
                ID_OrderStatus = toBeAdded.OrderStatusId,
                StatusDescription = toBeAdded.Decription
            });
            return localDb.SaveChanges() > 0;
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

        #endregion

        #region REMOVES

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
    }
}
