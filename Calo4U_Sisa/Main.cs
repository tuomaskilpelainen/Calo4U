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
            raakaAineLista.Tyhjenna();
            tagLista.Tyhjenna();
        }
        public string LisaaRaakaAine(string nimi, int maara, double kalorit)
        {

            Resepti.RaakaAine uusiRaakaAine = new Resepti.RaakaAine(nimi.ToLower(), maara);
            RaakaAineKalorit uusiKalori = new RaakaAineKalorit(nimi.ToLower(), kalorit);
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
            double kokoKalorit = 0;

            Resepti uusiResepti = new Resepti(nimi.ToLower(), ohjeet, annokset);
            uusiResepti.RaakaAineLista = raakaAineLista.Hae();
            List<RaakaAineKalorit> kalorit = Tallentaja.LataaKaikkiKalorit();

            foreach (Resepti.RaakaAine x in uusiResepti.RaakaAineLista)
            {
                foreach (RaakaAineKalorit y in kalorit)
                {
                    if (x.Nimi == y.Nimi)
                    {
                        kokoKalorit += y.Kalorit * x.Maara / 100;
                    }
                }
            }
            uusiResepti.kokoKalorit = kokoKalorit;
            uusiResepti.annoksenKalorit = kokoKalorit / annokset;


            uusiResepti.Tags = tagLista.Hae();
            Tallentaja.TallennaResepti(uusiResepti); // Vie Talentajaan joka talentaa reseptin Json fileen

        }
        public string[] LataaResepti(string nimi, int annokset)
        {
            bool loytyi = false;
            double kokoKalorit = 0;
            int oAnnokset;// On Reseptin annokset jos annoksia ei ole erikseen vaihdettu
            string[] reseptiText = new string[5];//0 Nimi, 1, Ohjeet, 2 RaakaAineet, 3 Kalorit, 4 Annokset

            Tallentaja lataaja = new Tallentaja();
            Resepti tResepti = lataaja.LataaResepti(nimi.ToLower()); // lataa reseptin "nimi" muuttajan perusteella Json filesta

            if (!string.IsNullOrEmpty(tResepti.Nimi))
            {
                talenettuResepti = tResepti;
                if (annokset == 0)
                {
                    reseptiText[4] = tResepti.Annokset.ToString();
                    oAnnokset = tResepti.Annokset;
                }
                else
                {
                    reseptiText[4] = annokset.ToString();
                    oAnnokset = annokset;
                }

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
                            double maara = (double)rAine.Maara / tResepti.Annokset;
                            maara = maara * oAnnokset;
                            reseptiText[2] += $"{rAine.Nimi} {Math.Round(maara,2)}g {Math.Round(kalorit.Kalorit,2)}kc/100g\n";
                            kokoKalorit += maara * kalorit.Kalorit / 100;
                            raakaAineLista.Tyhjenna();
                            foreach (Resepti.RaakaAine x in tResepti.RaakaAineLista)
                            {
                                
                                raakaAineLista.Lisaa(x);
                            }
                            
                            break;
                        }
                    }
                    if (!loytyi) { reseptiText[3] += $"\n{rAine.Nimi} {rAine.Maara / tResepti.Annokset * oAnnokset}g (Kalori tietoja ei löytynyt!)"; }
                    
                }
                double annosKalorit = kokoKalorit / oAnnokset;
                reseptiText[3] += $"Koko kalorit: {Math.Round(kokoKalorit,2)}\nAnnoksen kalorit: {Math.Round(annosKalorit,2)}";

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
            tagLista.Tyhjenna();
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
            string bmrText = $"Kaloritarve päivässä: \n{Math.Round(bmr,2)} cal";
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
            bool aloituspaiva = false;
            if (kaikki.Count >= 0)
            {
                List<Kayttaja> kaikk = Tallentaja.LataaKaikkiKayttajat();//Hakee talentajasta kaikki käyttäjät.
                Kayttaja kayttaja = new Kayttaja();
                Viikkohakija.GetCurrentWeekAndYear(out int weekNumber, out int year);
                foreach (Kayttaja k in kaikk)
                {
                    kayttaja.ValmiitRuuat = k.ValmiitRuuat;
                    kayttaja.AloitusPaiva = k.AloitusPaiva;
                }
                if (kaikki.Count <= 0) { aloituspaiva = true; }
                kayttaja.Nimi = "Käyttäjä"; // ainut käyttäjä nimi on Käyttäjä
                kayttaja.PvaKalorit = Math.Round(tBmr.Hae()); // Hakee tBmr arvon 

                if (aloituspaiva)
                {
                    DateTime tänään = DateTime.Today;
                    int poistettavatPaivat = (int)tänään.DayOfWeek - (int)DayOfWeek.Monday;
                    if (poistettavatPaivat < 0)
                    {
                        kayttaja.AloitusPaiva = tänään;
                    }
                    else
                    {
                        DateTime maanantai = tänään.AddDays(-poistettavatPaivat);
                        kayttaja.AloitusPaiva = maanantai;
                    }
                    kayttaja.TarkistaViikko();
                }

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
                tiedot[1] = Math.Round(x.PvaKalorit * 7).ToString();
                tiedot[2] = Math.Round(x.SyodytKalorit).ToString();
                tiedot[3] = Math.Round(x.PvaKalorit * 7 - x.SyodytKalorit).ToString();
            }
            return tiedot;
        }
        public static void LisaaReseptiKayttajalle(string nimi, int annokset)
        {
            List<Resepti> kaikki = Tallentaja.LaataakaikkiReseptit();
            if (kaikki.Count > 0)
            {
                foreach (Resepti x in kaikki)
                {
                    if (x.Nimi == nimi)
                    {
                        List<Kayttaja> kaikkiK = Tallentaja.LataaKaikkiKayttajat();
                        if (kaikkiK.Count > 0)
                        {
                            foreach (Kayttaja k in kaikkiK)
                            {
                                Resepti uusiResepti = x;
                                uusiResepti.Annokset = annokset;
                                uusiResepti.kokoKalorit = uusiResepti.annoksenKalorit * annokset;
                                bool on = k.ValmiitRuuat.Any(x => x.Nimi == nimi);
                                if (on)
                                    foreach(Resepti r in k.ValmiitRuuat)
                                    {
                                        if (r.Nimi == nimi)
                                        {
                                            r.Annokset += annokset;
                                            Tallentaja.TallennaKayttaja(k);
                                            return;
                                        }
                                    }
                                k.lisaaRuoka(uusiResepti);
                                Tallentaja.TallennaKayttaja(k);
                            }
                        }
                        else
                        {
                            return;
                        }

                    }
                }
            }
            else { return; }
        }

        public static List<string> KayttajanReseptitS() //Lataa ja muuttaa käyttän reseptit string muootoon nimen perusteella
        {
            List<string> reseptit = new List<string>();
            List<Kayttaja> kaikk = Tallentaja.LataaKaikkiKayttajat();
            foreach (Kayttaja x in kaikk)
            {
                foreach (Resepti r in x.ValmiitRuuat)
                {
                    reseptit.Add($"{r.Nimi}, Annokset: {r.Annokset}");
                }
            }
            return reseptit;
        }
        public static void SyoResepti(string nimi, bool miinus)
        {
            Resepti haettuResepti = Resepti.LuoTyhja();
            List<Kayttaja> kaikkiK = Tallentaja.LataaKaikkiKayttajat();
            foreach (Kayttaja k in kaikkiK)
            {
                foreach (Resepti r in k.ValmiitRuuat)
                {
                    if (r.Nimi == nimi)
                    {
                        if (miinus)
                        {

                            if ((k.SyodytKalorit - r.annoksenKalorit) <= 0 && k.SyodytKalorit > 0)
                            {
                                r.Annokset += 1;
                                k.SyoKalori(0 - r.annoksenKalorit);


                            }
                            else if (k.SyodytKalorit - r.annoksenKalorit < 0 && k.SyodytKalorit <= 0)
                            { 
                                r.Annokset = r.Annokset;
                            }
                            else
                            {
                                r.Annokset += 1;
                                k.SyoKalori(0 - r.annoksenKalorit);
                            }

                        }
                        else
                        {
                            r.Annokset -= 1;
                            k.SyoKalori(r.annoksenKalorit);
                        }
                        haettuResepti = r;


                    }
                }
                if (haettuResepti.Annokset <= 0)
                {
                    k.ValmiitRuuat.Remove(haettuResepti);
                }
                Tallentaja.TallennaKayttaja(k);
            }
        }

        public string HaeKalorit(string nimi)
        {
            string kalorit = string.Empty;
            List<RaakaAineKalorit> kaikki = Tallentaja.LataaKaikkiKalorit();
            foreach(RaakaAineKalorit k in kaikki) 
            {
                if (k.Nimi == nimi)
                {
                    kalorit = k.Kalorit.ToString();
                    return kalorit;
                }
            }
            return kalorit;
        }

        public int HaeAnnokset(string nimi) // Palauttaa reseptin alkuperäisen annosmäärän
        {
            int annokset = 5;
            List<Resepti> kaikki = Tallentaja.LaataakaikkiReseptit();
            foreach(Resepti k in kaikki)
            {
                if (k.Nimi == nimi)
                {
                    annokset = k.Annokset;
                    return annokset;
                }
            }
            return annokset;

        }
        public void TarkistaViikko()
        {
            bool tallenna = false;
            Kayttaja kayttaja = new Kayttaja();
            List<Kayttaja> kaikki = Tallentaja.LataaKaikkiKayttajat();
            if (kaikki.Count > 0)
            {
                foreach (Kayttaja k in kaikki)
                {
                    tallenna = k.TarkistaViikko();
                    kayttaja = k;
                }
            }
            if (tallenna)
            {
                Tallentaja.TallennaKayttaja(kayttaja);
            }

        }
        public bool TarkistaResepti(string nimi) // Tarkistaa onko reseptiä jo resepti kirjastossa
        {
            List<Resepti> kaikki = Tallentaja.LaataakaikkiReseptit();
            foreach (Resepti k in kaikki)
            {
                if (k.Nimi == nimi)
                {
                    return true;
                }
            }
            return false;
        }
        public void PostaResepti(string nimi)
        {
            Resepti x = Resepti.LuoTyhja();
            Tallentaja talentaja = new Tallentaja();
            List<Resepti> kaikki = Tallentaja.LaataakaikkiReseptit();
            foreach (Resepti k in kaikki)
            {
                if (k.Nimi == nimi)
                {
                    x = k;break;
                    
                }
            }
            kaikki.Remove(x);
            talentaja.TallennnaReseptiLista(kaikki);
        }
        public void PoistaKayttajanResepti(string nimi)
        {
            Kayttaja kayttaja = new Kayttaja();
            Resepti poista = Resepti.LuoTyhja();
            List<Kayttaja> kaikki = Tallentaja.LataaKaikkiKayttajat();
            foreach(Kayttaja k in kaikki)
            {
                foreach(Resepti r in k.ValmiitRuuat)
                {
                    if (r.Nimi == nimi)
                    {
                        kayttaja = k;
                        poista = r; break;  
                    }
                }
            }
            kayttaja.ValmiitRuuat.Remove(poista);
            Tallentaja.TallennaKayttaja(kayttaja);

        }

    }

}
