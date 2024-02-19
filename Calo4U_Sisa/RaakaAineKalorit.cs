using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo4U_Sisa
{
    internal class RaakaAineKalorit
    {
        public string Nimi {  get; set; }
        public double Kalorit { get; set; }
        public RaakaAineKalorit(string nimi, double kalorit) 
        {
            this.Nimi = nimi;
            this.Kalorit = kalorit;
        }
    }
}
