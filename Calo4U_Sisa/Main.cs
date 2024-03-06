using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Calo4U_Sisa
{
    public class Main
    {
        static Resepti talenettuResepti; // Resepti muutoksessa talenettava resepti
        static RaakaAineLista raakaAineLista = new RaakaAineLista(); // Tallenettavat raaka-aineet reseptin luonissa
        static TagLista tagLista = new TagLista(); // Tallenettavat tagit reseptin luonissa
        static Bmr tBmr = new Bmr(); // Bmr kalorin muuttujan tallenus
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
        public static void TalenettuReseptiTyhjennys()
        {
            talenettuResepti = Resepti.LuoTyhja();
        }
        public string LisaaRaakaAine(string nimi, int maara, double kalorit)
        {

            Resepti.RaakaAine uusiRaakaAine = new Resepti.RaakaAine(nimi, maara);
            RaakaAineKalorit uusiKalori = new RaakaAineKalorit(nimi, kalorit);
            raakaAineLista.Lisaa(uusiRaakaAine);
            Tallentaja.TalennaKalorit(uusiKalori); // Vie Tallentajaan joka tallentaa kalorit Json fileen
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
            Tallentaja.TallennaResepti(uusiResepti); // Vie Talentajaan joka talentaa reseptin Json fileen

        }
        public string[] LataaResepti(string nimi, int annokset)
        {
            bool loytyi = false;
            double kokoKalorit = 0;
            double kokogrammat = 0;
            string[] reseptiText = new string[5];//0 Nimi, 1, Ohjeet, 2 RaakaAineet, 3 Kalorit, 4 Annokset

            Tallentaja lataaja = new Tallentaja();
            Resepti tResepti = lataaja.LataaResepti(nimi); // lataa reseptin "nimi" muuttajan perusteella Json filesta
            if (!string.IsNullOrEmpty(tResepti.Nimi))
            {
                talenettuResepti = tResepti;
                List<RaakaAineKalorit> kaikkiKalorit = Tallentaja.LataaKaikkiKalorit(); // Hakee kaikki jo tallenetut kalorit Tallentajasta
                reseptiText[0] = $"{tResepti.Nimi}";
                reseptiText[1] = $"{tResepti.Ohjeet}";
                foreach (Resepti.RaakaAine rAine in tResepti.RaakaAineLista)
                {
                    foreach (RaakaAineKalorit kalorit in kaikkiKalorit)
                    {
                        if (rAine.Nimi == kalorit.Nimi)
                        {
                            loytyi = true;
                            reseptiText[2] += $"{rAine.Nimi} {rAine.Maara / tResepti.Annokset * annokset}g {kalorit.Kalorit}kc/100g\n";
                            kokoKalorit += kalorit.Kalorit;
                            kokogrammat += rAine.Maara / tResepti.Annokset * annokset;
                            raakaAineLista.Tyhjenna();
                            foreach (Resepti.RaakaAine x in tResepti.RaakaAineLista)
                            {
                                
                                raakaAineLista.Lisaa(x);
                            }
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
            ok = raakaAineLista.Tarkista(); // Tarkisttaa onko raaka ainelista tyhjä vai ei
            return ok;
        }
        public static void TyhjennaListat() // Tyhjentää kaikki listat reseptin tekoon liittyen
        {
            raakaAineLista.Tyhjenna();
            tagLista .Tyhjenna();
        }
        public List<string> ReseptitS() // Luo string listan kaikkista jo tallennetuista resepteistä
        {
            List<string> list = new List<string>();
            List<Resepti> kaikkiReseptit = new List<Resepti>();
            kaikkiReseptit = Tallentaja.LaataakaikkiReseptit(); // Hakee Tallentajasta kaikki reseptit jotka ovat jsonissa
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
            double bmr = kaloriLaskuri.Bmr(paino, pituus, ikä, sukupuoli, aktiivisuus, tavoite); // Käy Bmr clasissa ja laskee kalorintarpeen muuttujien perusteella
            tBmr.Lisaa(bmr);
            string bmrText = $"Kaloritarve päivässä: \n{bmr} cal";
            return bmrText;

        }

        public static void ManuaaliTallennus(double kalorit)
        {
            Main main = new Main();
            tBmr.Lisaa(kalorit); // Muuttaa tBmr muuttujan arvon
        }

        public static void tallennaKayttaja()
        {
            List<Kayttaja> kaikki = Tallentaja.LataaKaikkiKayttajat(); // Lataa kaikki käyttäjät (tällä hetkellä vain yksi käyttäjä mahdollinen)
            if (kaikki.Count >= 0)
            {
                Kayttaja kayttaja = new Kayttaja();
                kayttaja.Nimi = "Käyttäjä"; // ainut käyttäjä nimi on Käyttäjä
                kayttaja.PvaKalorit = tBmr.Hae(); // Hakee tBmr arvon 
                Tallentaja.TallennaKayttaja(kayttaja); // Muokkaa ja talentaa käyttäjän Jsoniin
            }
            else
            {
                foreach (Kayttaja x in kaikki) // Jos tulevaisuudessa lisätään monien käyttäjien luonti tallenus ominaisuus jo tässä mahdollinen
                {
                    if (x.Nimi == "Käyttäjä")
                    {
                        x.PvaKalorit = tBmr.Hae();
                        Tallentaja.TallennaKayttaja(x);
                    }
                }
            }
        }
        public static void PoistaRaakaAine(string nimi)
        {
            raakaAineLista.PoistaRaakaAine(nimi);
        }
        public string[] MuokkaaReseptia()
        {
            string[] reseptiString = new string[3]; //0 nimi, 1 ohjeet, 2 annokset
            Resepti resepti = talenettuResepti; // Hakee valitun reseptin jota muokataan muuttujasta
            if (resepti != null)
            {
                reseptiString[0] = resepti.Nimi;
                reseptiString[1] = resepti.Ohjeet;
                reseptiString[2] = resepti.Annokset.ToString();
                talenettuResepti = Resepti.LuoTyhja();//Luo tyhjän reseptin
                return reseptiString;
            }
            else
            {
                reseptiString[0] = string.Empty;
                reseptiString[1] = string.Empty;
                reseptiString[2] = string.Empty;
                return reseptiString;
            }
        }
        public List<string> HaeRaakaAineLista() // Luo string listan kaikista reseptin raaka-aineista reseptin luonissa
        {
            List<Resepti.RaakaAine> lista;
            if (talenettuResepti != null) { lista = talenettuResepti.RaakaAineLista; } // jos resepti on tyhjä palauttaa se tyhjän listan
            else { lista = new List<Resepti.RaakaAine>();}
            List<string> stringLista = new List<string>();
          
            foreach(Resepti.RaakaAine r in lista)
            {
                stringLista.Add(r.Nimi);
            }
            return stringLista;
        }

        public bool SyoKalorit(double kalorit) // Lisää tai poistaa käyttäjältä kalorit syötteen mukaan paluttaa True jos onnistunut tallenus
        {
            bool loytyi;
            Kayttaja kayttaja = new Kayttaja();
            List<Kayttaja> x = Tallentaja.LataaKaikkiKayttajat(); // Hakee Tallentajasta kaikki käyttäjät (tällä hetkellä vain yksi)
            if (x.Count <= 0)
            {
                loytyi = false;
                return loytyi;
            }
            else
            {
                foreach (Kayttaja y in x)
                {
                    kayttaja = y;
                }
                if (kayttaja != null)
                {
                    loytyi=true;
                    if (kayttaja.SyodytKalorit + kalorit < 0) // Tällä kalorit eivät voi mennä miinukselle jos menee niin kalorit ovat 0
                    {
                        kalorit = 0 - kayttaja.SyodytKalorit;
                        kayttaja.SyoKalori(kalorit);
                        Tallentaja.TallennaKayttaja(kayttaja); //Talentaa päivitetyn käyttäjän tiedot jsoniin
                        return loytyi;
                    }
                    else
                    {
                        kayttaja.SyoKalori(kalorit);
                        Tallentaja.TallennaKayttaja(kayttaja); //Talentaa päivitetyn käyttäjän tiedot jsoniin
                        return loytyi;
                    }

                }
                else
                {
                    loytyi = false;
                    return loytyi;
                }

            }


        }

        public string[] LataaKayttajanTiedot()
        {
            string[] tiedot = new string[4]; //0 viikko, 1 Tavoite, 2 Saavutettu,3 kaloreita jäljellä

            List<Kayttaja> kaikki = Tallentaja.LataaKaikkiKayttajat();
            foreach (Kayttaja x in kaikki)
            {
                tiedot[0] = x.Viikko.ToString();
                tiedot[1] = (x.PvaKalorit).ToString();
                tiedot[2] = x.SyodytKalorit.ToString();
                tiedot[3] = (x.PvaKalorit - x.SyodytKalorit).ToString();
            }
            return tiedot;
        }

    }
}
