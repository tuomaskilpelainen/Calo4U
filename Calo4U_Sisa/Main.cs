using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo4U_Sisa
{
    public class Main
    {
        static RaakaAineLista raakaAineLista = new RaakaAineLista();
        static TagLista tagLista = new TagLista();
        public static void Hello()
        {
            Console.WriteLine("Hello");
        }
        public string LisaaRaakaAine(string nimi, int maara, double kalorit)
        {

            Resepti.RaakaAine uusiRaakaAine = new Resepti.RaakaAine(nimi, maara);
            RaakaAineKalorit uusiKalori = new RaakaAineKalorit(nimi, kalorit);
            raakaAineLista.Lisaa(uusiRaakaAine);
            Tallentaja.TalennaKalorit(uusiKalori);
            string raakaAineText = $"{uusiRaakaAine.Nimi} määrä: {uusiRaakaAine.Maara}g kalorit: {uusiKalori.Kalorit}kc/100g";
            return raakaAineText;
        }

        public string LisaaTag(string tag)
        {
            tagLista.Lisaa(tag);
            string tagText = $"#{tag}";
            return tagText;
        }
        public static void LisaaResepti(string nimi, string ohjeet, int annokset)
        {
            Resepti uusiResepti = new Resepti(nimi, ohjeet, annokset);
            uusiResepti.RaakaAineLista = raakaAineLista.Hae();
            uusiResepti.Tags = tagLista.Hae();
            Tallentaja.TallennaResepti(uusiResepti);

        }
        public string LataaResepti(string nimi, int annokset)
        {
            bool loytyi = false;
            double kokoKalorit = 0;
            double kokogrammat = 0;
            string reseptiText = string.Empty;
            Tallentaja lataaja = new Tallentaja();
            Resepti tResepti = lataaja.LataaResepti(nimi);
            if (!string.IsNullOrEmpty(tResepti.Nimi))
            {
                List<RaakaAineKalorit> kaikkiKalorit = Tallentaja.LataaKaikkiKalorit();
                reseptiText += $"{tResepti.Nimi}\nAnnoksia: {annokset}\n";
                foreach (Resepti.RaakaAine rAine in tResepti.RaakaAineLista)
                {
                    foreach (RaakaAineKalorit kalorit in kaikkiKalorit)
                    {
                        if (rAine.Nimi == kalorit.Nimi)
                        {
                            loytyi = true;
                            reseptiText += $"\n{rAine.Nimi} {rAine.Maara / tResepti.Annokset * annokset}g";
                            kokoKalorit += kalorit.Kalorit;
                            kokogrammat += rAine.Maara / tResepti.Annokset * annokset;
                            break;
                        }
                    }
                    if (!loytyi) { reseptiText += $"\n{rAine.Nimi} {rAine.Maara / tResepti.Annokset * annokset}g (Kalori tietoja ei löytynyt!)"; }
                    
                }
                kokoKalorit = kokoKalorit / 100 * kokogrammat;
                double annosKalorit = kokoKalorit / annokset;
                reseptiText += $"\n\nOhjeet:\n{tResepti.Ohjeet}\nKoko ruuan kalorit: {kokoKalorit}\nAnnoksen kalorit: {annosKalorit}";
                raakaAineLista.Tyhjenna();
                tagLista.Tyhjenna();
                return reseptiText;
            }
            return reseptiText;

        }
        public bool TarkistaRaakaAineetLista()
        {
            bool ok;
            ok = raakaAineLista.Tarkista();
            return ok;
        }
    }
}
