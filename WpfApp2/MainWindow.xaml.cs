using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WpfApp2
{
    /// <summary> 
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Resepti.RaakaAine> raakaAineLista = new List<Resepti.RaakaAine>();


        public MainWindow()
        {
            InitializeComponent();


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string aines = "";
            int maarat;
            int kalorit;

            if (!string.IsNullOrEmpty(aine.Text))
                aines = aine.Text;
        int.TryParse(maara.Text, out maarat);
        int.TryParse(kalori.Text, out kalorit);

            if (!string.IsNullOrEmpty(aines) && maarat > 0)
            {
                var uusiRaakaAine = new Resepti.RaakaAine(aines, maarat);
                raakaAineLista.Add(uusiRaakaAine);
                var uusiAines = new ainesosat(aines, kalorit);
                Tallentaja.TalennaAinesosa(uusiAines);
                var Tulostus = new Tulostaja();
                string ainesstring = Tulostus.PaivitaAinekset(uusiRaakaAine);
                ainekset.Text = ainesstring;

            }


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string nimi;
            string ohjeet;
            int annokset;

            string maara = ReseptiAnnokset.Text.ToString();
            nimi = ReseptiNimi.Text.ToString();
            ohjeet = ReseptiOhjeet.Text.ToString();
            int.TryParse(maara, out annokset);
            if (!string.IsNullOrEmpty(nimi) && !string.IsNullOrEmpty(ohjeet))
            {
                var Tulostus = new Tulostaja();
                Resepti uusiResepti = new Resepti(nimi, ohjeet, annokset);
                uusiResepti.RaakaAineLista = raakaAineLista;
                string tulostus = Tulostus.ReseptiTulostus(uusiResepti);
                ReseptiNaytto.Text = tulostus;
                Tallentaja.TallennaResepti(uusiResepti);
                raakaAineLista.Clear();


            }
        }
    }
    internal partial class Tulostaja : MainWindow // Ideana että tämä class hoitaa kaikki tulostukseen liittyvät asiat :).
    {
        public string PaivitaAinekset(Resepti.RaakaAine aine)
        {
            var newString = "";
            List<ainesosat> kaikkiAinesosat = Tallentaja.LataakaikkiAinesosat();
            foreach (ainesosat tieto in kaikkiAinesosat)
                if (aine.Nimi == tieto.aine)
                    newString = $"{aine.Nimi} {aine.Maara} g {tieto.kalori} kcal \n";
                    return newString ;
        }
        public string ReseptiTulostus(Resepti newRecepty)
        {
            int kokoKalorit = 0;

            List<ainesosat> kaikkiAinesosat = Tallentaja.LataakaikkiAinesosat();
            var newstring = $"{newRecepty.Nimi}. Annoskisa: {newRecepty.Annokset}";
            foreach (Resepti.RaakaAine raakaAine in newRecepty.RaakaAineLista)
            {
                foreach(ainesosat aine in kaikkiAinesosat)
                {
                    if (aine.aine ==  raakaAine.Nimi)
                    {
                        ainesosat kalorit = aine;
                        newstring += $"\n{raakaAine.Nimi} {raakaAine.Maara}g {kalorit.kalori} kc/100g";
                        kokoKalorit += kalorit.kalori * raakaAine.Maara / 100;
                    }
                }
            }
            
            int annosKalorit = kokoKalorit / newRecepty.Annokset;
            newstring += $"\nOhjeet: \n{newRecepty.Ohjeet}\nKokonais kalori määrä on: {kokoKalorit}\nYhden annoksen kalorit ovat: {annosKalorit}";
            
            return newstring ;


        }

    }
}