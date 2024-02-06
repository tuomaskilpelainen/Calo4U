using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

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
        public static void SaveJson(Recepty saveRecepty)
        {
            bool save = true;
            var option = new JsonSerializerOptions();
            option.WriteIndented = true;
            string json = File.ReadAllText("resepti_testi.json");
            List<Recepty> allRecepties;
            try
            {
                allRecepties = JsonSerializer.Deserialize<List<Recepty>>(json);
            }
            catch (JsonException) 
            {
                allRecepties = new List<Recepty>();
            }
            foreach(Recepty x in allRecepties)
            {
                if (x.Name == saveRecepty.Name) { save = false; }
            }
            if (save)
            {
                allRecepties.Add(saveRecepty);
                string saveJson = JsonSerializer.Serialize<List<Recepty>>(allRecepties, option);
                File.WriteAllText("resepti_testi.json", saveJson);
                Console.WriteLine("TAlenettu resepti");
            }
            else
            {
                Console.WriteLine(saveRecepty.Name + " ei talenettu! Saman niminen resepti on jo olemassa.");
            }

        }
        public static void LataaResepti(string name)
        {
            Recepty loadRecepty = new Recepty();
            string json = File.ReadAllText("resepti_testi.json");
            List<Recepty> allRecepties = JsonSerializer.Deserialize<List<Recepty>>(json);
            foreach (Recepty x in allRecepties)
            {
                if (x.Name == name)
                {
                    loadRecepty = x;
                    break;
                }
            }
            if (loadRecepty != null)
            {
                List<IngridienseCalories> caloryList = new List<IngridienseCalories>();
                caloryList = IngridienseCalories.LoadJson();
                Console.WriteLine("\n\n\nLadattu resepti:\n");
                TulostaResepti(loadRecepty, caloryList);
            }
            else { Console.WriteLine("Hakusanalla ei löytynyt reseptiä"); }
        }
    }
}
