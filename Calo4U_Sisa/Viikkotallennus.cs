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
            Tallentaja tallentaja = new Tallentaja();

            List<Entry> entries = new List<Entry>();
            List<Week> weeks = new List<Week>();

            entries = tallentaja.entries();
            weeks = tallentaja.LataaVikkoTeodot();

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

            Tallentaja.TalennaViikot(weeks);
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
