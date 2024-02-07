using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace Class_C4U_test
{
    internal class IngridienseCalories
    {
        public string Name { get; set; }
        public double Calories {  get; set; }
        public IngridienseCalories(string name, double calories)
        {
            Name = name;
            Calories = calories;
        }

        public static void SaveJson(IngridienseCalories saveIngridianse)
        {
            Saver.SaveJsonCalories(saveIngridianse);
        
        }
        public static List<IngridienseCalories> LoadJson()
        {

            List<IngridienseCalories> allCalories = Saver.LoadAllJsonCalories();
            return allCalories;

        }
    }

}
