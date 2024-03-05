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
    /// Interaction logic for KalorintarveValinta.xaml
    /// </summary>
    public partial class KalorintarveValinta : Page
    {
        private Frame mainFrame;
        public KalorintarveValinta(Frame mainFrame)
        {
            InitializeComponent();
            this.mainFrame = mainFrame;
        }

        private void ManuaalinenSyotto_Click(object sender, RoutedEventArgs e)
        {
            Manuaalinen popupContent = new Manuaalinen();
            Popup.Content = popupContent;
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

        private void KalorintarveLaskuri_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Kalorilaskuri(mainFrame));
            mainFrame.Visibility = Visibility.Visible;

        }
    }

}
