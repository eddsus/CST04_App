using DataAgent.SR_Synchronizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

        public List<SharedDataTypes.Ingredient> QueryAllIngredients()
        {
            GetSynchronizerStatus();
            if (connected)
            {
                return synchronizer.QueryAllIngredients().Select(i => new SharedDataTypes.Ingredient() {
                    IngredientId = i.IngredientId,
                    Name = i.Name,
                    Description = i.Description,
                    Available = i.Available,
                    Price = i.Price,
                    Type = i.Type,
                    UnitType = i.UnitType
                }).ToList();
            }
            else
            {
                throw new NotImplementedException();
            }
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
    }
}
