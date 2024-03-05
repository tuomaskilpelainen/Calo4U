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
using Calo4U_Sisa;

namespace Calo4U_GUI
{
    /// <summary>
    /// Interaction logic for Kalorilaskuri.xaml
    /// </summary>
    public partial class Kalorilaskuri : Page
    {
        private Frame mainFrame;
        public Kalorilaskuri(Frame mainFrame)
        {
            InitializeComponent();
            this.mainFrame = mainFrame;
        }
        private void laskeButton_Click(object sender, RoutedEventArgs e)
        {
            double paino;
            double pituus;
            int ikä;
            string sukupuoli;
            string aktiivisuus;
            string tavoite;
            bool lisaa = true;

            try
            {
                paino = double.Parse(Paino_textbox.Text);

            }

            catch
            {
                paino = 0;
                lisaa = false;
            }

            try
            {
                pituus = double.Parse(Pituus_texbox.Text);

            }

            catch
            {
                pituus = 0;
                lisaa = false;
            }

            try
            {
                ikä = int.Parse(Ikä_texbox.Text);

            }

            catch
            {
                ikä = 0;
                lisaa = false;
            }


            if (Mies_radiobutton.IsChecked == true)
            {
                sukupuoli = "m";
            }

            else if (Nainen_radiobutton.IsChecked == true)
            {
                sukupuoli = "n";
            }

            else
            {
                sukupuoli = "o";
                lisaa = false;
            }

            if (Lepo_radiobutton.IsChecked == true)
            {
                aktiivisuus = "l";
            }

            else if (Kevyt_radiobutton.IsChecked == true)
            {
                aktiivisuus = "k";
            }

            else if (Tavallinen_radiobutton.IsChecked == true)
            {
                aktiivisuus = "t";
            }

            else if (Kohtuullinen_radiobutton.IsChecked == true)
            {
                aktiivisuus = "ko";
            }

            else if (Raskas_radiobutton.IsChecked == true)
            {
                aktiivisuus = "r";
            }

            else if (Ammattiurheilija_radiobutton.IsChecked == true)
            {
                aktiivisuus = "e";
            }

            else
            {
                aktiivisuus = "o";
                lisaa=false;
            }

            if (Ylläpitää_radiobutton.IsChecked == true)
            {
                tavoite = "y";
            }

            else if (Pudottaa_radiobutton.IsChecked == true)
            {
                tavoite = "p";
            }

            else if (Kasvattaa_radiobutton.IsChecked == true)
            {
                tavoite = "k";
            }

            else { tavoite = "o"; lisaa = false; }


            Main main = new Main();
            if (lisaa)
            {
                Ylläpitäminen_textblock.Text = main.bmr(paino, pituus, ikä, sukupuoli, aktiivisuus, tavoite);
            }
            

            
        }

        private void Laskekalorintarve_button_Copy1_Click(object sender, RoutedEventArgs e)
        {

            Main.tallennaKayttaja();
            MainWindow mainWindow1 = new MainWindow();
            mainWindow1.LataaKayttajanTiedot();
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow is MainWindow mainWindow)
            {
                mainWindow.LataaKayttajanTiedot();
                Main.TyhjennaListat();
                mainWindow.ShowMainWindow();

            }

        }

        private void etusivuNavButton_Click_1(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow is MainWindow mainWindow)
            {
                mainWindow.LataaKayttajanTiedot();
                Main.TyhjennaListat();
                mainWindow.ShowMainWindow();

            }
        }

        private void LisääReseptiNavButton_Click_1(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Page1(mainFrame));
        }

        private void katsoReseptitNavButton_Click_1(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Page2(mainFrame));
        }
    }
}
