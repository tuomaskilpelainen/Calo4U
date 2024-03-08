using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Calo4U_Sisa
{
    public class Viikkotallennus
    {
        public void CheckJson()
        {
            string kayttajaJsonFilePath = "kayttajaKirjasto.json";
            string viikkoJsonFilePath = "viikkoKalorit.json";

            List<Entry> entries = new List<Entry>();
            if (File.Exists(kayttajaJsonFilePath))
            {
                string kayttajaJsonData = File.ReadAllText(kayttajaJsonFilePath);
                entries = JsonSerializer.Deserialize<List<Entry>>(kayttajaJsonData);
            }

            List<Week> weeks = new List<Week>();
            if (File.Exists(viikkoJsonFilePath))
            {
                string viikkoJsonData = File.ReadAllText(viikkoJsonFilePath);
                weeks = JsonSerializer.Deserialize<List<Week>>(viikkoJsonData);
            }

            // Päivitä syodytKalorit arvo viikkoKalorit.json tiedostossa kayttajaKirjasto.json tietojen mukaan
            foreach (var entry in entries)
            {
                var existingWeek = weeks.FirstOrDefault(w => w.Viikko == entry.Viikko);
                if (existingWeek != null)
                {
                    existingWeek.SyodytKalorit = entry.SyodytKalorit;
                }
                else
                {
                    // Luo viikon jos sitä ei ole
                    weeks.Add(new Week
                    {
                        Viikko = entry.Viikko,
                        PvaKalorit = entry.PvaKalorit,
                        SyodytKalorit = entry.SyodytKalorit
                    });
                }
            }

            string updatedViikkoJsonData = JsonSerializer.Serialize(weeks, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(viikkoJsonFilePath, updatedViikkoJsonData);
        }
    }

    class Entry
    {
        public int Viikko { get; set; }
        public int PvaKalorit { get; set; }
        public int SyodytKalorit { get; set; }
    }

    class Week
    {
        public int Viikko { get; set; }
        public int PvaKalorit { get; set; }
        public int SyodytKalorit { get; set; }
    }
}
