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
        public static void SaveJson(IngridienseCalories saveIngridianse)
        {
            bool lisaa = true;
            var option = new JsonSerializerOptions();
            option.WriteIndented = true;
            string json = File.ReadAllText("calorit_testi.json");
            List<IngridienseCalories> allIngridienses;
            try
            {
                allIngridienses = JsonSerializer.Deserialize<List<IngridienseCalories>>(json);
            }
            catch (JsonException) 
            {
                allIngridienses = new List<IngridienseCalories>();
            }
            foreach(IngridienseCalories x in allIngridienses)
            {
                if (x.Name == saveIngridianse.Name) { lisaa = false; }
            }
            if (lisaa) 
            {
                
                allIngridienses.Add(saveIngridianse);
                string updateJson = JsonSerializer.Serialize(allIngridienses, option);
                File.WriteAllText("calorit_testi.json", updateJson);
            }
            else
            {
                Console.WriteLine(saveIngridianse.Name + " ei talenettu! Saman niminen raaka-aine on jo olemassa.");
            }
        
        }
        public static List<IngridienseCalories> LoadJson()
        {

            string json = File.ReadAllText("calorit_testi.json");
            List<IngridienseCalories> allIngridienses;
            try
            {
                allIngridienses = JsonSerializer.Deserialize<List<IngridienseCalories>>(json);

            }
            catch (JsonException)
            {
                allIngridienses = new List<IngridienseCalories>();
            }
            return allIngridienses;

        }
    }

}
