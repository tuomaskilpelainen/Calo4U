using System.Diagnostics;
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
        List<ainesosat> aineslista = new List<ainesosat>();
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
                var uusiAines = new ainesosat(aines, maarat, kalorit);
                aineslista.Add(uusiAines);
            }
            PaivitaAinekset();
        }

        public void PaivitaAinekset()
        {
            var newString = "";
            foreach (var tieto in aineslista)
                newString += $"{tieto.aine} {tieto.maara} g {tieto.kalori} kcal \n";
            ainekset.Text = newString;
        }
    }
}