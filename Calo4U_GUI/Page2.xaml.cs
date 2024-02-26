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
using Calo4U_Sisa;

namespace Calo4U_GUI
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        private List<string> Resepetit;
        public Page2()
        {

            InitializeComponent();
            Lista_Box_TextChance_2();
            //Lista_Box.Items.Clear();
            Lista_Box.ItemsSource = Resepetit;

        }
        public void LaataaLuotuResepti(string resepti, int annokset)
        {
            NaytaResepti(resepti, annokset);
        }
        private void Lista_Box_TextChance_2()
        {
            Main main = new Main();
            Resepetit = main.ReseptitS();
            Lista_Box.ItemsSource = Resepetit;
            string haku = Haku_Box.Text.ToLower();
            var haut = Resepetit.Where(item => item.ToLower().Contains(haku)).ToList();
            Lista_Box.ItemsSource = haut;

        }
        private void Lista_Box_TextChance(object sender, TextChangedEventArgs e)
        {
            Main main = new Main();
            Resepetit = main.ReseptitS();
            Lista_Box.ItemsSource = Resepetit;
            string haku = Haku_Box.Text.ToLower();
            var haut = Resepetit.Where(item => item.ToLower().Contains(haku)).ToList();
            Lista_Box.ItemsSource = haut;
            
        }

        private void EtusivuButton_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow is MainWindow mainWindow)
            {
                mainWindow.ShowMainWindow();
            }
        }

        private void Lista_Click(object sender, MouseButtonEventArgs e)
        {
            int annokset = 5;
            string resepti = (string)Lista_Box.SelectedItem as string;
            NaytaResepti(resepti, annokset);





        }
        private void NaytaResepti(string resepti, int annokset)
        {
            Main main = new Main();
            string[] Sresepti = main.LataaResepti(resepti, annokset); //0 Nimi, 1, Ohjeet, 2 RaakaAineet, 3 Kalorit, 4 Annokset
            ReseptinNimiTextBlock.Text = Sresepti[0];
            AineksetTextBlock.Text = Sresepti[2];
            OhjeetText_Copy.Text = Sresepti[1];
            ReseptinNimiTextBlock2.Text = Sresepti[0];
            AnnosMääräTextBlock.Text = Sresepti[4];
            KaloritTextBlock.Text = Sresepti[3];
        }


        private void ValmistaRuoanButton_Click(object sender, RoutedEventArgs e)
        {
            int annokset;
            string nimi = ReseptinNimiTextBlock.Text;
            try
            {
                annokset = int.Parse(hakuBox1_Copy2.Text);
            }
            catch 
            {
                annokset = 0;
                hakuBox1_Copy2.Foreground = Brushes.Red;
            }
            if(!String.IsNullOrEmpty(nimi))
            {
                if (annokset > 0) 
                {
                    hakuBox1_Copy2.Foreground = Brushes.Black;
                    NaytaResepti(nimi, annokset);
                }
            }
            else
            {
                ReseptinNimiTextBlock.Text = "Reseptiä ei löytynyt";
            }
        }
    }
}
