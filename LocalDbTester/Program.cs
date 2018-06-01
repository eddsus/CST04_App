using DataHandler;
using SharedDataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDbTester
{
    class Program
    {
        static void Main(string[] args)
        {
            LocalDataHandler dh = new LocalDataHandler();
            AddIngredient(dh);

            var temp = dh.QueryIngredients();


            Console.ReadLine();
        }

        private static void AddIngredient(LocalDataHandler dh)
        {
            if (dh.AddIngredient(new Ingredient()
            {
                IngredientId = Guid.NewGuid(),
                Available = true,
                DatedModified = DateTime.Now,
                Description = "We rock mothafucker!! Yeeaaaahhh",
                Name = "The great Banana is back",
                Price = 9999,
                Type = "Treasury",
                UnitType = "Extreme"
            }))
            {
                Console.WriteLine("Ingrdient added");
            }
        }
    }
}
