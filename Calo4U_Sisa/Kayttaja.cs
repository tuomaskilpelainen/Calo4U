using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo4U_Sisa
{
    internal class Kayttaja
    {
        public string Nimi {  get; set; }
        public  double PvaKalorit {  get; set; }
        public List<Resepti> ValmiitRuuat { get; set; } = new List<Resepti>();
        public double SyodytKalorit { get; set; } = 0;
        public int Viikko {  get; set; } = 0;
        public double HaeKayttajanKalorit()
        {
            return PvaKalorit;
        }
        public void lisaaRuoka(Resepti ruoka)
        {
            ValmiitRuuat.Add(ruoka);
        }
        public void PoistaRuoka(Resepti ruoka)
        {
            ValmiitRuuat.Remove(ruoka);
        }
        public void TyhjennaRuuat()
        {
            ValmiitRuuat.Clear();
        }
        public void SyoKalori(double kalorit)
        {
            if (SyodytKalorit + kalorit < 0)
            {
                SyodytKalorit = 0;
            }
            else
            {
                SyodytKalorit += kalorit;
            }
            
        }

    }
}
