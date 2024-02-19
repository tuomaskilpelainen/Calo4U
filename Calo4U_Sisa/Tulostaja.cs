using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo4U_Sisa
{
    internal class Tulostaja
    {
        public string PaivitaAinekset(Resepti.RaakaAine aine)
        {
            var newString = "";
            List<RaakaAineKalorit> kaikkiAinesosat = Tallentaja.LataaKaikkiKalorit();
            foreach (RaakaAineKalorit tieto in kaikkiAinesosat)
                if (aine.Nimi == tieto.Nimi)
                    newString = $"{aine.Nimi} {aine.Maara} g {tieto.Kalorit} kcal \n";
            return newString;
        }
        public string ReseptiTulostus(Resepti newRecepty)
        {
            double kokoKalorit = 0;

            List<RaakaAineKalorit> kaikkiAinesosat = Tallentaja.LataaKaikkiKalorit();
            var newstring = $"{newRecepty.Nimi}. Annoskisa: {newRecepty.Annokset}";
            foreach (Resepti.RaakaAine raakaAine in newRecepty.RaakaAineLista)
            {
                foreach (RaakaAineKalorit aine in kaikkiAinesosat)
                {
                    if (aine.Nimi == raakaAine.Nimi)
                    {
                        RaakaAineKalorit kalorit = aine;
                        newstring += $"\n{raakaAine.Nimi} {raakaAine.Maara}g {kalorit.Kalorit} kc/100g";
                        kokoKalorit += kalorit.Kalorit * raakaAine.Maara / 100;
                    }
                }
            }

            double annosKalorit = kokoKalorit / newRecepty.Annokset;
            newstring += $"\nOhjeet: \n{newRecepty.Ohjeet}\nKokonais kalori määrä on: {kokoKalorit}\nYhden annoksen kalorit ovat: {annosKalorit}";

            return newstring;


        }

    }
}

