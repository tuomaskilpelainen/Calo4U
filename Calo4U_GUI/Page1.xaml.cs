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
                mainWindow.ShowMainWindow(); 
            }
        }

        private void LisääRaakaButton_Click(object sender, RoutedEventArgs e)
        {
            Main main = new Main();
            string nimi;
            int maara;
            double kalorit;
            nimi = raaka_aineBox.Text;
            try
            {
                maara = int.Parse(määräBox.Text);
            }
            catch
            {
                maara = 0;
            }
            try
            {
                kalorit = double.Parse(kaloritBox.Text);
            }
            catch { kalorit = 0; }

            if (!string.IsNullOrEmpty(nimi) && maara > 0)
            {
                string raakaAineText = main.LisaaRaakaAine(nimi, maara, kalorit);
                ainesTextBlock.Text += $"\n {raakaAineText}";
            }


        }

        private void ReseptButton_Click(object sender, RoutedEventArgs e)
        {
            string nimi;
            string ohjeet;
            int annokset;
            try
            {
                annokset = int.Parse(annoksetText.Text);

            }
            catch (Exception ex) { annokset = 0; }
            if (!string.IsNullOrEmpty(reseptiNimiBox.Text) && !string.IsNullOrEmpty(ohjeetBox.Text))
            {
                Main main = new Main();
                nimi = reseptiNimiBox.Text;
                ohjeet = ohjeetBox.Text;
                Main.LisaaResepti(nimi, ohjeet, annokset);
                string reseptiText = main.LataaResepti(nimi, annokset);
                NäytäOhjeetTextBlock.Text = reseptiText;
            }

        }
    }
}

