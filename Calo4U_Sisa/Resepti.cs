﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo4U_Sisa
{
    internal class Resepti
    {
        public string Nimi { get; set; } // Reseptin nimi
        public string Ohjeet { get; set; } // Reseptin Ohjeet
        public int Annokset { get; set; } // Kuinka monta annosta reseptistä tulee
        public double kokoKalorit { get; set; }
        public double annoksenKalorit { get; set; }
        public List<string> Tags { get; set; }
        public List<RaakaAine> RaakaAineLista { get; set; } = new List<RaakaAine>(); //Sisältää kaikki reseptin raaka-aineet ja niiden tiedot.
        public Resepti(string nimi, string ohjeet, int annokset)
        {
            this.Nimi = nimi;
            this.Ohjeet = ohjeet;
            this.Annokset = annokset;
        }
        public static Resepti LuoTyhja()
        {
            return new Resepti(null, null, 0);
        }
        internal class RaakaAine
        {
            public string Nimi { get; set; } //Raaka-aineen nimi
            public int Maara { get; set; } //Raaka-aineen määrä g tai desi.
            public RaakaAine(string nimi, int maara)
            {
                this.Nimi = nimi;
                this.Maara = maara;
            }
        }
    }
}

