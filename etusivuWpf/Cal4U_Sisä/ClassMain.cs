namespace Cal4U_Sisa
{
    public class Main
    {
        internal static RaakaAineLista raakaAineLista = new RaakaAineLista(); //Lista joka pitää sisällään kaikki reseptin raakaianeet
        internal static TagLista tagLista = new TagLista(); // Lista, joka pitää sisällään kaikki reseptin tagit.


        public static void Clear()
        {
            raakaAineLista.tyhjenna();
            tagLista.tyhjenna();
        }
        public static void TalennaResepti(string nimi, string ohjeet, int annokset)
        {
            Resepti uusiResepti = new Resepti(nimi, ohjeet, annokset);
            uusiResepti.RaakaAineLista = raakaAineLista.Palauta();
            uusiResepti.Tagit = tagLista.palauta();


        }
        public string LisaaTagi(string tag)
        {
            tagLista.lisaa(tag);

            return tag;
        }
        public string TalennaRaakaAine(string nimi, double maara, double kalorit)
        {
            Resepti.RaakaAine uusiRaakaAine = new Resepti.RaakaAine(nimi, maara);
            RaakaAineKalorit uusiRaakaAineKalorit = new RaakaAineKalorit(nimi, kalorit);
            raakaAineLista.lisaa(uusiRaakaAine);

            return nimi;
        }
        public string LataaResepti(string nimi)
        {
            string reseptiTeksti = "";
            return reseptiTeksti;
        }
    }
}
