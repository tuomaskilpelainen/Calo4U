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
        public Page1()
        {
            InitializeComponent();
        }

        private void LisääUusiResepText_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void etusivuButton_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);
            if (parentWindow is MainWindow mainWindow)
            {
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
                    ainesTextBlock.Text += $"\n {raakaAineText}";
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
                    string reseptiText = main.LataaResepti(nimi, annokset);
                    NäytäOhjeetTextBlock.Text = reseptiText;
                    ohjeetBox.Foreground = Brushes.Black;
                    reseptiNimiBox.Foreground = Brushes.Black;
                    annoksetText.Foreground = Brushes.Black;
                    NäytäOhjeetTextBlock.Foreground = Brushes.Black;
                    ohjeetBox.Text = string.Empty;
                    reseptiNimiBox.Text = string.Empty;
                    annoksetText.Text = string.Empty;
                    ainesTextBlock.Text = string.Empty ;
                }
                else
                {
                    NäytäOhjeetTextBlock.Foreground = Brushes.Red;
                    NäytäOhjeetTextBlock.Text = "Lisää ensin raaka-aineet.";
                }
            }

        }

        private void tagButton_Click(object sender, RoutedEventArgs e)
        {
            Main main = new Main() ;
            string tag;
            tag = tagBox.Text;
            if (!string.IsNullOrEmpty(tag) )
            {
                string tagText = main.LisaaTag(tag);
                if (!string.IsNullOrEmpty(tagText))
                {
                    NäytäAineksetTextBlock.Text += tagText;
                }
            }
        }
    }
}

