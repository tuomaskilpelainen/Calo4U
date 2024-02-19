using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo4U_Sisa
{
    internal class RaakaAineLista
    {
        private List<Resepti.RaakaAine> raakaAineLista;
        public RaakaAineLista()
        {
            raakaAineLista = new List<Resepti.RaakaAine>();
        }
        public void Lisaa(Resepti.RaakaAine raakaAine)
        {
            raakaAineLista.Add(raakaAine);
        }
        public void Tyhjenna()
        {
            raakaAineLista.Clear();
        }
        public List<Resepti.RaakaAine> Hae()
        {
            return raakaAineLista;
        }
    }
}
