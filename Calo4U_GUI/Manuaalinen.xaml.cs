using Calo4U_Sisa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calo4U_GUI
{
    /// <summary>
    /// Interaction logic for Manuaalinen.xaml
    /// </summary>
    
    public partial class Manuaalinen : UserControl
    {
        public Manuaalinen()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string input = Kaloritextbox.Text;
            int kalorit;
            if (int.TryParse(input, out kalorit) && Convert.ToInt32(Kaloritextbox.Text) > 0)
            {
                kalorit = Convert.ToInt32(input);
                IlmoitusBox.Foreground = new SolidColorBrush(Colors.Green);
                IlmoitusBox.Text = "Tallennettu";
                IlmoitusBox.TextAlignment = TextAlignment.Center;
                Main.ManuaaliTallennus(kalorit);
                Main.tallennaKayttaja();
            }
            else
            {
                IlmoitusBox.Foreground = new SolidColorBrush(Colors.Red);
                IlmoitusBox.Text = "Syötä positiivinen numeerinen arvo";
            }
        }
    }
}
