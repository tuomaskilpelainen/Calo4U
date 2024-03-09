using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Calo4U_Sisa
{
    internal class Tallentaja
    {
        private const string KALORI_TIEDOSTO = "kaloriKirjasti.json"; //Tallenus kansion on Cal4U/WpfApp2/bin/Debug/net.8.0-windows/ainesosaKirjasto.json.
        private const string RESEPTI_TIEDOSTO = "resptiKirjasto.json"; //Tallenus kansion on Cal4U/WpfApp2/bin/Debug/net.8.0-windows/reseptiKirjasto.json.
        private const string KAYTTAJA_TIEDOSTO = "kayttajaKirjasto.json";
        private const string VIIKKO_TIEDOSTO = "viikkoKalorit.json";

        public static List<Kayttaja> LataaKaikkiKayttajat()
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            List<Kayttaja> kaikkiKayttajat = new List<Kayttaja>();
            string json;
            try
            {
                json = File.ReadAllText(Tallentaja.KAYTTAJA_TIEDOSTO);
            }
            catch 
            {
                kaikkiKayttajat = new List<Kayttaja>();
                return kaikkiKayttajat;
            }
            try
            {
                kaikkiKayttajat = JsonSerializer.Deserialize<List<Kayttaja>>(json);
            }
            catch (Exception e)
            {
                kaikkiKayttajat = new List<Kayttaja>();
            }
            return kaikkiKayttajat;

        }
        public static void TallennaKayttaja(Kayttaja kayttaja)
        {
            var options = new JsonSerializerOptions();
            options.WriteIndented = true;
            List<Kayttaja> kaikkiKayttajat = LataaKaikkiKayttajat(); // Lataa Jsonfilesta kaikki käyttäjät jos tätä ei ole palauttaa tyhjän lsitan
            kaikkiKayttajat.Clear();           
            kaikkiKayttajat.Add(kayttaja);
            string json = JsonSerializer.Serialize<List<Kayttaja>>(kaikkiKayttajat, options);
            File.WriteAllText(Tallentaja.KAYTTAJA_TIEDOSTO, json);
            
        }
        public static List<RaakaAineKalorit> LataaKaikkiKalorit()
        {
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            List<RaakaAineKalorit> kaikkiAinesosat = new List<RaakaAineKalorit>();
            try
            {
                string json_test = File.ReadAllText(Tallentaja.KALORI_TIEDOSTO);
            }
            catch (Exception ex)
            {
                kaikkiAinesosat = new List<RaakaAineKalorit>();
                return kaikkiAinesosat;
            }
            string json = File.ReadAllText(Tallentaja.KALORI_TIEDOSTO);
            try
            {
                kaikkiAinesosat = JsonSerializer.Deserialize<List<RaakaAineKalorit>>(json);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                kaikkiAinesosat = new List<RaakaAineKalorit>();
            }
            return kaikkiAinesosat;
        }
        public static void TalennaKalorit(RaakaAineKalorit uusiAine)
        {
            bool lisaa = true;
            List<RaakaAineKalorit> kaikkiainekset = LataaKaikkiKalorit(); // Lataa kaikki kalorit Jsonista jos tätä ei ole palauttaa tyhjän listan
            if (!kaikkiainekset.Any())
            {
                kaikkiainekset = new List<RaakaAineKalorit>();
            }
            RaakaAineKalorit mAine = kaikkiainekset.FirstOrDefault(obj => obj.Nimi == uusiAine.Nimi);
            if (mAine != null)
            {
                mAine.Kalorit = uusiAine.Kalorit;
            }
            else
            {
                kaikkiainekset.Add(uusiAine);
            }
            var option = new JsonSerializerOptions();
            option.WriteIndented = true; //Tämä optio tekee json Filesta helposti luettavaa. Poistettava ennen julkaisua, vie ennemän tilaa

            string json = JsonSerializer.Serialize<List<RaakaAineKalorit>>(kaikkiainekset, option);
            File.WriteAllText(Tallentaja.KALORI_TIEDOSTO, json);





        }
        public static List<Resepti> LaataakaikkiReseptit()
        {
            List<Resepti> kaikkiReseptit = new List<Resepti>();
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            try
            {
                string json = File.ReadAllText(RESEPTI_TIEDOSTO);
                try
                {
                    kaikkiReseptit = JsonSerializer.Deserialize<List<Resepti>>(json);
                    return kaikkiReseptit;
                }
                catch { return kaikkiReseptit; }

            }
            catch (Exception ex)
            {
                return kaikkiReseptit;
            }

        }
        public static void TallennaResepti(Resepti uusiResepti)
        {
            var option = new JsonSerializerOptions();
            option.WriteIndented = true; //Tämä optio tekee json Filesta helposti luettavaa.
            List<Resepti> kaikkiReseptit = Tallentaja.LaataakaikkiReseptit(); // Lataa kaikki Reseptit Jsonista jos tätä ei ole palauttaa tyhjän listan
            Resepti mResepti = kaikkiReseptit.FirstOrDefault(obj => obj.Nimi == uusiResepti.Nimi);

            if (mResepti != null)
            {
                mResepti.Annokset = uusiResepti.Annokset;
                mResepti.RaakaAineLista = uusiResepti.RaakaAineLista;
                mResepti.Ohjeet = uusiResepti.Ohjeet;
                mResepti.Tags = uusiResepti.Tags;
            }
            else
            {
                kaikkiReseptit.Add(uusiResepti);
            }
            string json = JsonSerializer.Serialize<List<Resepti>>(kaikkiReseptit, option);
            File.WriteAllText(RESEPTI_TIEDOSTO, json);
        }
        public Resepti LataaResepti(string nimi)
        {
            List<Resepti> kaikkiReseptit = Tallentaja.LaataakaikkiReseptit();// Lataa kaikki reseptit Jsonista jos tätä ei ole palauttaa tyhjän listan
            foreach (Resepti haettuResepti in kaikkiReseptit)
            {
                if (haettuResepti.Nimi == nimi) { return haettuResepti; }
            }
            string Nimi = "";
            int annokset = 0;
            string ohjeet = "";
            Resepti resepti = new Resepti(Nimi, ohjeet, annokset);
            return resepti;
        }
        public List<Entry> entries()
        {
            List<Entry> entriesList = new List<Entry>();
            if (File.Exists(KAYTTAJA_TIEDOSTO))
            {
                string kayttajaJsonData = File.ReadAllText(KAYTTAJA_TIEDOSTO);
                try
                {
                    entriesList = JsonSerializer.Deserialize<List<Entry>>(kayttajaJsonData);
                }
                catch { entriesList = new List<Entry>(); }
                
            }
            return entriesList;
        }
        public List<Week> LataaVikkoTeodot()
        {
            List<Week> weeks = new List<Week>();
            if (File.Exists(VIIKKO_TIEDOSTO))
            {
                string viikkoJsonData = File.ReadAllText(VIIKKO_TIEDOSTO);
                weeks = JsonSerializer.Deserialize<List<Week>>(viikkoJsonData);

            }
            return weeks;
        }

        public static void TalennaViikot(List<Week> viikkoTiedot)
        {
            string updatedViikkoJsonData = JsonSerializer.Serialize(viikkoTiedot, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(VIIKKO_TIEDOSTO, updatedViikkoJsonData);
        }
        public void TallennnaReseptiLista(List<Resepti> kaikki)
        {
            var option = new JsonSerializerOptions();
            option.WriteIndented = true; //Tämä optio tekee json Filesta helposti luettavaa.
            if (kaikki.Count <= 0)
            {
                return;
            }
            else
            {
                string json = JsonSerializer.Serialize<List<Resepti>>(kaikki, option);
                File.WriteAllText(RESEPTI_TIEDOSTO, json);
            }
;
        }
    }
}

