using SharedDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDashboard.Helpers.MessengerWrappers
{
    class IngredientDeactivatedMessage
    {

        public Ingredient DeactivatedIngredient { get; }


        public IngredientDeactivatedMessage(Ingredient value)
        {
            DeactivatedIngredient = value;
        }
    }
}
