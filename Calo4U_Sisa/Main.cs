using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;


namespace Calo4U_Sisa
{
    public class Main
    {
        static RaakaAineLista raakaAineLista = new RaakaAineLista();
        static TagLista tagLista = new TagLista();
        static Bmr tBmr = new Bmr();
        internal class Bmr
        {
            private double Numero;

            public Bmr()
            {
                Numero = new double();
            }
            public void Lisaa(double numero) { Numero = numero; }
            public double Hae() { return Numero; }

        }
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
        public string[] LataaResepti(string nimi, int annokset)
        {
            bool loytyi = false;
            double kokoKalorit = 0;
            double kokogrammat = 0;
            string[] reseptiText = new string[5];//0 Nimi, 1, Ohjeet, 2 RaakaAineet, 3 Kalorit, 4 Annokset

            Tallentaja lataaja = new Tallentaja();
            Resepti tResepti = lataaja.LataaResepti(nimi);
            if (!string.IsNullOrEmpty(tResepti.Nimi))
            {
                List<RaakaAineKalorit> kaikkiKalorit = Tallentaja.LataaKaikkiKalorit();
                reseptiText[0] = $"{tResepti.Nimi}";
                reseptiText[1] = $"{tResepti.Ohjeet}";
                foreach (Resepti.RaakaAine rAine in tResepti.RaakaAineLista)
                {
                    foreach (RaakaAineKalorit kalorit in kaikkiKalorit)
                    {
                        if (rAine.Nimi == kalorit.Nimi)
                        {
                            loytyi = true;
                            reseptiText[2] += $"\n{rAine.Nimi} {rAine.Maara / tResepti.Annokset * annokset}g {kalorit.Kalorit}kc/100g";
                            kokoKalorit += kalorit.Kalorit;
                            kokogrammat += rAine.Maara / tResepti.Annokset * annokset;
                            break;
                        }
                    }
                    if (!loytyi) { reseptiText[3] += $"\n{rAine.Nimi} {rAine.Maara / tResepti.Annokset * annokset}g (Kalori tietoja ei löytynyt!)"; }
                    
                }
                kokoKalorit = kokoKalorit / 100 * kokogrammat;
                double annosKalorit = kokoKalorit / annokset;
                reseptiText[3] += $"Koko ruuan kalorit: {kokoKalorit}\nAnnoksen kalorit: {annosKalorit}";
                reseptiText[4] = annokset.ToString();
                return reseptiText;
            }
            reseptiText[0] = "Reseptiä Ei löytynyt";
            return reseptiText;

        }
        public bool TarkistaRaakaAineetLista()
        {
            bool ok;
            ok = raakaAineLista.Tarkista();
            return ok;
        }
        public static void TyhjennaListat()
        {
            raakaAineLista.Tyhjenna();
            tagLista .Tyhjenna();
        }
        public List<string> ReseptitS()
        {
            List<string> list = new List<string>();
            List<Resepti> kaikkiReseptit = new List<Resepti>();
            kaikkiReseptit = Tallentaja.LaataakaikkiReseptit();
            foreach (Resepti resepti in kaikkiReseptit)
            {
                string s_resepti = resepti.Nimi;
                list.Add(s_resepti);

            }
            return list;
        }


        public string bmr(double paino, double pituus, int ikä, string sukupuoli, string aktiivisuus, string tavoite)
        {
            KaloriLaskuri kaloriLaskuri = new KaloriLaskuri();
            double bmr = kaloriLaskuri.Bmr(paino, pituus, ikä, sukupuoli, aktiivisuus, tavoite);
            tBmr.Lisaa(bmr);
            string bmrText = $"Kaloritarve päivässä: {bmr} cal";
            return bmrText;

        }

        public static void tallennaKayttaja()
        {
            List<Kayttaja> kaikki = Tallentaja.LataaKaikkiKayttajat();
            if (kaikki.Count >= 0)
            {
                Kayttaja kayttaja = new Kayttaja();
                kayttaja.Nimi = "Käyttäjä";
                kayttaja.PvaKalorit = tBmr.Hae();
                Tallentaja.TallennaKayttaja(kayttaja);
            }
            else
            {
                foreach (Kayttaja x in kaikki)
                {
                    if (x.Nimi == "Käyttäjä")
                    {
                        x.PvaKalorit = tBmr.Hae();
                        Tallentaja.TallennaKayttaja(x);
                    }
                }
            }
        }
    }
}
