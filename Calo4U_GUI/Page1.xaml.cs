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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        private Frame mainFrame;
        private List<string> raakaAineTextLista = new List<string>();
        private string valittuRaakaAine;
        private string valittuTeksti;
        public Page1(Frame mainFrame)
        {
            InitializeComponent();
            RaakaAineLista.ItemsSource = raakaAineTextLista;
            this.mainFrame = mainFrame;
            string dataFromMethod = LataaMuokattuResepti();



        }
        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            ohjeetBox.Text = string.Empty;
            reseptiNimiBox.Text = string.Empty;
            annoksetText.Text = string.Empty;
            raakaAineTextLista = new List<string>();

            RaakaAineLista.ItemsSource = null;
            RaakaAineLista.ItemsSource = raakaAineTextLista;

        }

        private void etusivuNavButton_Click(object sender, RoutedEventArgs e)
        {

            Window parentWindow = Window.GetWindow(this);
            if (parentWindow is MainWindow mainWindow)
            {
                mainWindow.LataaKayttajanReseptit();
                ohjeetBox.Text = string.Empty;
                reseptiNimiBox.Text = string.Empty;
                annoksetText.Text = string.Empty;

                raakaAineTextLista = new List<string>();
                RaakaAineLista.ItemsSource =  null;
                RaakaAineLista.ItemsSource = raakaAineTextLista;

                Main.TyhjennaListat();

                mainWindow.ShowMainWindow(); 
            }
        }

        private void LisääRaakaButton_Click(object sender, RoutedEventArgs e)
        {
            bool lisaa = true;
            Main main = new Main();
            string nimi;
            int maara;
            double kalorit;
            nimi = raaka_aineBox.Text;
            try
            {

                maara = int.Parse(määräBox.Text);
                määräBox.Foreground = Brushes.Black;
                if (maara <= 0) 
                {
                    lisaa = false;
                    määräBox.Foreground = Brushes.Red;
                }
            }
            catch
            {
                määräBox.Foreground = Brushes.Red;
                maara = 0;
                lisaa = false;
                
            }
            try
            {
                kalorit = double.Parse(kaloritBox.Text);
                kaloritBox.Foreground = Brushes.Black;
            }
            catch 
            { 
                kaloritBox.Foreground = Brushes.Red;
                kalorit = 0;
                lisaa = false;
            }

            if (!string.IsNullOrEmpty(nimi))
            {
                raaka_aineBox.Foreground = Brushes.Black;
                if (lisaa)
                {
                    string raakaAineText = main.LisaaRaakaAine(nimi, maara, kalorit);
                    raakaAineTextLista.Add(raakaAineText);

                    RaakaAineLista.ItemsSource = null;
                    RaakaAineLista.ItemsSource = raakaAineTextLista;
                    RaakaAineLista.InvalidateVisual();

                    määräBox.Foreground = Brushes.Black;
                    kaloritBox.Foreground = Brushes.Black;
                    raaka_aineBox.Foreground = Brushes.Black;
                    määräBox.Text = string.Empty;
                    kaloritBox.Text = string.Empty;
                    raaka_aineBox.Text = string.Empty;
                }

            }
            else { raaka_aineBox.Foreground = Brushes.Red; }


        }

        private void ReseptButton_Click(object sender, RoutedEventArgs e)
        {
            bool lisaa = true;
            string nimi;
            string ohjeet;
            int annokset;
            try
            {
                annokset = int.Parse(annoksetText.Text);
                if (annokset <= 0) 
                {
                    lisaa = false;
                    annoksetText.Foreground = Brushes.Red;

                }
                else { annoksetText.Foreground = Brushes.Black; }

            }
            catch (Exception ex) 
            { 
                annokset = 0; 
                annoksetText.Foreground = Brushes.Red;
                lisaa = false;  

            }
            if (string.IsNullOrEmpty(reseptiNimiBox.Text))
            {
                lisaa = false;
                reseptiNimiBox.Foreground = Brushes.Red;
            }
            else { reseptiNimiBox.Foreground= Brushes.Black;}
            if (string.IsNullOrEmpty(ohjeetBox.Text))
            {
                lisaa = false;
                ohjeetBox.Foreground = Brushes.Red;
            }
            else { ohjeetBox.Foreground= Brushes.Black;}
            if (lisaa)
            {
                bool tallenna;

                Main main = new Main();
                tallenna = main.TarkistaRaakaAineetLista();
                if (tallenna)
                {
                    nimi = reseptiNimiBox.Text;
                    ohjeet = ohjeetBox.Text;
                    Main.LisaaResepti(nimi, ohjeet, annokset);

                    // Näitä ei välttis tarvii koska avaa suoraan page 2 :)
                    ohjeetBox.Foreground = Brushes.Black;
                    reseptiNimiBox.Foreground = Brushes.Black;
                    annoksetText.Foreground = Brushes.Black;
                    NäytäOhjeetTextBlock.Foreground = Brushes.Black;
                    ohjeetBox.Text = string.Empty;
                    reseptiNimiBox.Text = string.Empty;
                    annoksetText.Text = string.Empty;
                    //ainesTextBlock.Text = string.Empty ;
                    // Näitä ei välttis tarvii koska avaa suoraan page 2 :)
                    raakaAineTextLista = new List<string>();
                    Main.TyhjennaListat();

                    Page2 page2 = new Page2(mainFrame);
                    page2.LaataaLuotuResepti(nimi, annokset);
                    NavigationService.Navigate(page2);
                }
                else
                {
                    NäytäOhjeetTextBlock.Foreground = Brushes.Red;
                    NäytäOhjeetTextBlock.Text = "Lisää ensin raaka-aineet.";
                }
            }

        }

        

        private void katsoReseptitNavButton_Click(object sender, RoutedEventArgs e)
        {
            ohjeetBox.Text = string.Empty;
            reseptiNimiBox.Text = string.Empty;
            annoksetText.Text = string.Empty;
            raakaAineTextLista = new List<string>();
            RaakaAineLista.ItemsSource = null;
            RaakaAineLista.ItemsSource = raakaAineTextLista;

            Main.TyhjennaListat();
            mainFrame.Navigate(new Page2(mainFrame));

        }

        private void kaloritarveNavButton_Click(object sender, RoutedEventArgs e)
        {
            ohjeetBox.Text = string.Empty;
            reseptiNimiBox.Text = string.Empty;
            annoksetText.Text = string.Empty;
            raakaAineTextLista = new List<string>();
            RaakaAineLista.ItemsSource = null;
            RaakaAineLista.ItemsSource = raakaAineTextLista;

            Main.TyhjennaListat();

            mainFrame.Navigate(new KalorintarveValinta(mainFrame));

        }

        private void ValitseRaakaAine(object sender, MouseButtonEventArgs e)
        {
            valittuTeksti = (string)RaakaAineLista.SelectedItem as string;
            if (!string.IsNullOrEmpty (valittuTeksti) )
            {
                string[] moniTeksti = valittuTeksti.Split(' ');
                valittuRaakaAine = moniTeksti[0];
            }

        }

        private void RaakaineDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(valittuRaakaAine))
            {
                Main.PoistaRaakaAine(valittuRaakaAine);
                raakaAineTextLista.Remove(valittuTeksti);
                RaakaAineLista.ItemsSource = null;
                RaakaAineLista.ItemsSource = raakaAineTextLista;

            }
        }
        /*public void LataaMuokattuResepti()
        {
            Main main = new Main();
            string[] resepriText = main.MuokkaaReseptia(); //0 nimi, 1 ohjeet, 2 annokset
            raakaAineTextLista = main.HaeRaakaAineLista();

            RaakaAineLista.ItemsSource = null;
            RaakaAineLista.ItemsSource = raakaAineTextLista;

            reseptiNimiBox.Text = resepriText[0];
            annoksetText.Text = resepriText[2];
            ohjeetBox.Text = resepriText[1];

        }*/

        private string LataaMuokattuResepti()
        {
            Main main = new Main();
            raakaAineTextLista = main.HaeRaakaAineLista();

            RaakaAineLista.ItemsSource = null;
            RaakaAineLista.ItemsSource = raakaAineTextLista;
            string[] resepriText = main.MuokkaaReseptia(); //0 nimi, 1 ohjeet, 2 annokset
            reseptiNimiBox.Text = resepriText[0];
            if (resepriText[2] == "0")
            {
                annoksetText.Text = string.Empty;
            }
            else { annoksetText.Text = resepriText[2]; }
            ohjeetBox.Text = resepriText[1];

            return "Data from LataaMuokattuResepti() method";
        }
    }
}

