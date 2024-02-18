using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cal4U_Sisa
{
    internal class Resepti
    {
        public string Nimi { get; set; } // Reseptin nimi
        public string Ohjeet { get; set; } // Reseptin Ohjeet
        public int Annokset { get; set; } // Kuinka monta annosta reseptistä tulee
        public List<string> Tagit { get; set; } //Lista reseptin tageista.
        public List<RaakaAine> RaakaAineLista { get; set; } //Sisältää kaikki reseptin raaka-aineet ja niiden tiedot.
        public Resepti(string nimi, string ohjeet, int annokset)
        {
            this.Nimi = nimi;
            this.Ohjeet = ohjeet;
            this.Annokset = annokset;
        }
        internal class RaakaAine
        {
            public string Nimi;
            public double Maara;
            public RaakaAine(string nimi, double maara)
            {
                Nimi = nimi;
                Maara = maara;
            }
        }
    }
}
