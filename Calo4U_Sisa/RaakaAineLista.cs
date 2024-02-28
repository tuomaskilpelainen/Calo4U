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
        public bool Tarkista()
        {
            bool ok;
            if (raakaAineLista.Count > 0) { ok = true; }
            else { ok = false; }
            return ok;
        }
        public void PoistaRaakaAine(string nimi)
        {
            bool liisaa = false;
            string nimi1 = "";
            int maara = 0;
            Resepti.RaakaAine raakaAine = new Resepti.RaakaAine(nimi1, maara);
            foreach (Resepti.RaakaAine x in raakaAineLista)
            {
                if (nimi == x.Nimi)
                {
                    raakaAine = x;
                    liisaa = true;
                }
            }
            if (liisaa)
            {
                raakaAineLista.Remove(raakaAine);
            }
        }
    }
}
