using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cal4U_Sisa
{
    internal class RaakaAineKalorit
    {
        public string Nimi; // raaka-aineen nimi
        public double Kalorit; // raaka-aineen kalorimäärä
        public RaakaAineKalorit(string nimi, double kalorit)
        {
            Nimi = nimi;
            Kalorit = kalorit;
        }
    }
}
