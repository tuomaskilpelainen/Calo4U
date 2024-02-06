using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Class_C4U_test
{
    internal class Recepty
    {
        public string Name { get; set; }
        public string Instructions { get; set; }
        public int Servings { get; set; }
        public List<Ingridiense> Ingridienses { get; set; } = new List<Ingridiense>();

        internal class Ingridiense
        {
            public string Name { get; set; }
            public int Grams { get; set; }
        }
        public static void TulostaResepti(Recepty newRecepty, List<IngridienseCalories> caloryList)
        {
            Console.WriteLine(newRecepty.Name + "\nAnnoksien määrä: " + newRecepty.Servings + "\nOhjeet:\n" + newRecepty.Instructions);
            double totalCalories = 0;
            foreach(Ingridiense x in newRecepty.Ingridienses)
            {
                IngridienseCalories y = caloryList.FirstOrDefault(cal => cal.Name == x.Name);
                if ( y != null)
                {
                    Console.WriteLine(x.Name + ": " + x.Grams + " grammaa. " + y.Calories + " kaloria 100 grammassa.");

                    totalCalories += y.Calories * x.Grams / 100;

                }
                else
                {
                    Console.WriteLine(x.Name + ". Kaloritietoja ei löytynyt anna raaka-aineen kalorimäärä 100 gramssa");
                    string kalorit = Console.ReadLine();
                    IngridienseCalories newCalory = new IngridienseCalories();
                    newCalory.Name = x.Name;
                    newCalory.Calories = int.Parse(kalorit);
                    Console.WriteLine(x.Name + ": " + x.Grams + " grammaa. " + newCalory.Calories + " kaloria 100 grammassa.");
                    totalCalories += newCalory.Calories * x.Grams / 100;
                }
            }
            Console.WriteLine("Ruuan kokonais kalorimäärä on " + totalCalories + "\nYhden annoksen kalori määrä on " + totalCalories / newRecepty.Servings);
        }
        
    }
}
