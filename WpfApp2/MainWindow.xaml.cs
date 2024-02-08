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
            List<Resepti.RaakaAine> uusiRaakaAineLista = new List<Resepti.RaakaAine>();

            if (!string.IsNullOrEmpty(aines) && maarat > 0)
            {
                var uusiRaakaAine = new Resepti.RaakaAine(aines, maarat);
                uusiRaakaAineLista.Add(uusiRaakaAine);
                var uusiAines = new ainesosat(aines, kalorit);
                Tallentaja.TalennaAinesosa(uusiAines);
                
            }
            var Tulostus = new Tulostaja();
            Tulostus.PaivitaAinekset(uusiRaakaAineLista);

        }

    }
    internal partial class Tulostaja : MainWindow // Ideana että tämä class hoitaa kaikki tulostukseen liittyvät asiat :).
    {
        public void PaivitaAinekset(List<Resepti.RaakaAine> aineslista)
        {
            var newString = "";
            List<ainesosat> kaikkiAinesosat = Tallentaja.LataakaikkiAinesosat();
            foreach (Resepti.RaakaAine tieto in aineslista)
                foreach (ainesosat raakaAine in kaikkiAinesosat)
                    if (raakaAine.aine == tieto.Nimi)
                        newString += $"{tieto.Nimi} {tieto.Maara} g {raakaAine.kalori} kcal \n";
                        ainekset.Text = newString;
        }
    }
}