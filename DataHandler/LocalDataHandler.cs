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

        #endregion

        #region INSERTS

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
    }
}
