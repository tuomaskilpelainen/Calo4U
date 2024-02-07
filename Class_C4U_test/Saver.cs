using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using Microsoft.VisualBasic.FileIO;

namespace Class_C4U_test
{


    internal class Saver
    {
        private const string RESEPTI_TIEDOSTO = "resepti_testi.json";
        private const string RAAKAAINE_TIEDOSTO = "calorit_testi.json";
        public static void SaveJsonRecepty(Recepty saveRecepty)
        {
            List<Recepty> allRecepties = new List<Recepty>();
            allRecepties = Saver.LoadAllJsonRecepty();
            var option = new JsonSerializerOptions();
            option.WriteIndented = true;
            bool save = Saver.CheckReceptyDublicates(saveRecepty);

            if (save)
            {
                allRecepties.Add(saveRecepty);
                string saveJson = JsonSerializer.Serialize<List<Recepty>>(allRecepties, option);
                File.WriteAllText(RESEPTI_TIEDOSTO, saveJson);
                Console.WriteLine("Tallennettu resepti");
            }
            else
            {
                Console.WriteLine(saveRecepty.Name + " ei talenettu! Saman niminen resepti on jo olemassa.");
            }
        }
        public static void LoadRecepty(string hakuSana)
        {
            bool match = false;
            List<Recepty> allRecepties = Saver.LoadAllJsonRecepty();
            foreach (Recepty x in allRecepties)
            {
                if (x.Name == hakuSana)
                {
                    Recepty recepty = x;
                    if (recepty != null) 
                    {
                        List< IngridienseCalories> caloryList = Saver.LoadAllJsonCalories();
                        Recepty.TulostaResepti(recepty, caloryList);
                        match = true;
                        break;
                    }
                }
                if (match == false) { Console.WriteLine($"Hakusanalla '{hakuSana}' ei löytynyt reseptejä :(."); }
            }


        }
        public static bool CheckReceptyDublicates(Recepty recepty)
        {
            List<Recepty> allRecepties = Saver.LoadAllJsonRecepty();
            foreach (Recepty x in allRecepties)
            {
                if (x.Name == recepty.Name) { return false; }
            }
            return true;

        }
        public static List<Recepty> LoadAllJsonRecepty()
        {
            string json = File.ReadAllText(Saver.RESEPTI_TIEDOSTO);
            List<Recepty> allRecepties;
            try
            {
                allRecepties = JsonSerializer.Deserialize<List<Recepty>>(json);
            }
            catch (JsonException)
            {
                allRecepties = new List<Recepty>();
            }
            return allRecepties;
        }
        public static List<IngridienseCalories> LoadAllJsonCalories()
        {
            string json = File.ReadAllText(RAAKAAINE_TIEDOSTO);
            List<IngridienseCalories> allIngridienses = new List<IngridienseCalories>();
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
        public static void SaveJsonCalories(IngridienseCalories ingridient)
        {
            bool save = true;
            List<IngridienseCalories> allCalories = new List<IngridienseCalories>();
            allCalories = LoadAllJsonCalories();
            foreach (IngridienseCalories x in allCalories)
            {
                if (x.Name == ingridient.Name)
                {
                    Console.WriteLine($"{ingridient.Name} on jo talenettu");
                    save = false;
                    break;
                }

            }
            if (save)
            {
                var option = new JsonSerializerOptions();
                option.WriteIndented = true;
                allCalories.Add(ingridient);
                string json = JsonSerializer.Serialize<List<IngridienseCalories>>(allCalories, option);
                File.WriteAllText(RAAKAAINE_TIEDOSTO, json);

            }

        }
    }
}
