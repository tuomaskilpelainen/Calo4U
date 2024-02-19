using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo4U_Sisa
{
    public class Main
    {
        RaakaAineLista raakaAineLista = new RaakaAineLista();
        TagLista tagLista = new TagLista();
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
            string raakaAineText = $"{uusiRaakaAine.Nimi} määrä: {uusiRaakaAine.Maara}g kalorit: {uusiKalori.Kalorit}/100g";
            return raakaAineText;
        }
    }
}
