using DataAgent.SR_Synchronizer;
using SharedDataTypes;
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
                return synchronizer.QueryAllIngredients().Select(i => new Ingredient() {
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



    }
}
