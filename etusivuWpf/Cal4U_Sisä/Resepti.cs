using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cal4U_Sisa
{
    internal class Resepti
    {
        public string Ohjeet { get; set; }
        public int Annokset { get; set; }
        public List<string> Tagit { get; set; }
        public List<RaakaAine> RaakaAineLista { get; set; }

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
