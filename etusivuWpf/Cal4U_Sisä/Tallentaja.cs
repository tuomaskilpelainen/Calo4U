using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Cal4U_Sisa
{
    internal class Tallentaja
    {
        private const string KALORI_TIEDOSTO = "raakaAineKirjasto.json"; //Tallenus kansion on Cal4U/WpfApp2/bin/Debug/net.8.0-windows/ainesosaKirjasto.json.
        private const string RESEPTI_TIEDOSTO = "resptiKirjasto.json"; //Tallenus kansion on Cal4U/WpfApp2/bin/Debug/net.8.0-windows/reseptiKirjasto.json.

        public static List<RaakaAineKalorit> LataaKaikkiRaakaAineKalorit()
        {
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            List<RaakaAineKalorit> kaikkiRaakaAineKalorit = new List<RaakaAineKalorit>();
            try
            {
                string json_test = File.ReadAllText(Tallentaja.KALORI_TIEDOSTO);
            }
            catch (Exception ex)
            {
                kaikkiRaakaAineKalorit = new List<RaakaAineKalorit>();
                return kaikkiRaakaAineKalorit;
            }
            string json = File.ReadAllText(Tallentaja.KALORI_TIEDOSTO);
            try
            {
                kaikkiRaakaAineKalorit = JsonSerializer.Deserialize<List<RaakaAineKalorit>>(json);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                kaikkiRaakaAineKalorit = new List<RaakaAineKalorit>();
            }
            return kaikkiRaakaAineKalorit;
        }
        public static void TalennaRaakaineKirjastoon(RaakaAineKalorit uusiAine)
        {
            bool lisaa = true;
            List<RaakaAineKalorit> kaikkiainekset = LataaKaikkiRaakaAineKalorit();
            if (!kaikkiainekset.Any())
            {
                kaikkiainekset = new List<RaakaAineKalorit>();
            }
            foreach (RaakaAineKalorit vertailtavaAine in kaikkiainekset)
                if (vertailtavaAine.Nimi == uusiAine.Nimi)
                {
                    lisaa = false;
                    break;
                }
            if (lisaa)
            {
                kaikkiainekset.Add(uusiAine);
                var option = new JsonSerializerOptions();
                option.WriteIndented = true; //Tämä optio tekee json Filesta helposti luettavaa.

                string json = JsonSerializer.Serialize<List<RaakaAineKalorit>>(kaikkiainekset, option);
                File.WriteAllText(Tallentaja.KALORI_TIEDOSTO, json);
            }




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
            List<Resepti> kaikkiReseptit = Tallentaja.LaataakaikkiReseptit();
            foreach (Resepti resepti in kaikkiReseptit)
            {
                if (resepti.Nimi == uusiResepti.Nimi)
                {
                    return;
                }
            }
            kaikkiReseptit.Add(uusiResepti);
            string json = JsonSerializer.Serialize<List<Resepti>>(kaikkiReseptit, option);
            File.WriteAllText(RESEPTI_TIEDOSTO, json);
        }
    }
}

