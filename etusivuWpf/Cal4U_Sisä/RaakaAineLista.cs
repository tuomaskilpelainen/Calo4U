using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cal4U_Sisa
{
    internal class RaakaAineLista
    {
        private List<Resepti.RaakaAine> raakaAineLista;
        public RaakaAineLista()
        {
            raakaAineLista = new List<Resepti.RaakaAine> ();
        }
        public void lisaa(Resepti.RaakaAine raakaAine)
        {
            raakaAineLista.Add (raakaAine);
        }
        public void tyhjenna()
        {
            raakaAineLista.Clear ();
        }
        public List<Resepti.RaakaAine> Palauta()
        {
            return raakaAineLista;
        }
    }
}
