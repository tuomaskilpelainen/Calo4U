using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json;

namespace WpfApp2
{
    internal class Tallentaja
    {
        private const string TIEDOSTON_NIMI = "ainesosaKirjasto.json"; //Tallenus kansion on Cal4U/WpfApp2/bin/Debug/net.8.0-windows/ainesosaKirjasto.json.

        public static List<ainesosat> LataakaikkiAinesosat()
        {
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;

            List<ainesosat> kaikkiAinesosat = new List<ainesosat>();
            try
            {
                string json_test = File.ReadAllText(Tallentaja.TIEDOSTON_NIMI); 
            }
            catch (Exception ex)
            {
                kaikkiAinesosat = new List<ainesosat>();
                return kaikkiAinesosat;
            }
            string json = File.ReadAllText(Tallentaja.TIEDOSTON_NIMI);
            try
            {
                kaikkiAinesosat = JsonSerializer.Deserialize<List<ainesosat>>(json);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); //Tää menee aina Expetioniin

                kaikkiAinesosat = new List<ainesosat>();
            }
            return kaikkiAinesosat;
        }
        public static void TalennaAinesosa(ainesosat uusiAine)
        {
            bool lisaa = true;
            List<ainesosat> kaikkiainekset = LataakaikkiAinesosat();
            if (!kaikkiainekset.Any())
            {
                kaikkiainekset = new List<ainesosat>();
            }
            foreach (ainesosat vertailtavaAine in kaikkiainekset)
                if (vertailtavaAine.aine == uusiAine.aine)
                {
                    lisaa = false;
                    break;
                }
            if (lisaa)
                {
                kaikkiainekset.Add(uusiAine);
                var option = new JsonSerializerOptions();
                option.WriteIndented = true; //Tämä optio tekee json Filesta helposti luettavaa.

                string json = JsonSerializer.Serialize<List<ainesosat>>(kaikkiainekset, option);
                File.WriteAllText(Tallentaja.TIEDOSTON_NIMI, json);
                }

                            


        }
    }
}
