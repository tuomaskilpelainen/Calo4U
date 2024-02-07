using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    internal class ainesosat
    {
        public string aine {  get; set; } // raaka-aineen nimi
        public int maara { get; set; } // raaka-aineen määrä grammoina
        public int kalori { get; set; } // raaka-aineen kalorimäärä


    public ainesosat(string aine, int maara, int kalori)
    {
        this.aine = aine;
        this.maara = maara;
        this.kalori = kalori;
    }

    }
}
