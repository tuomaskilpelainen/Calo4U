using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        

        //private void uusReseptButton_Click(object sender, RoutedEventArgs e)
        //{
        //    MainFrame.Navigate(new Page1(MainFrame));
        //    MainFrame.Visibility = Visibility.Visible;
        //}
        public void ShowMainWindow()
        {
            mainFrame.Visibility = Visibility.Collapsed;
        }

        //private void katsoReseptitButton_Click(object sender, RoutedEventArgs e)
        //{
        //    MainFrame.Navigate(new Page2(MainFrame));
        //    MainFrame.Visibility = Visibility.Visible;
        //}

        private void uusReseptButton_Copy_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void kalorintarveButton_Click(object sender, RoutedEventArgs e)
        //{
        //    MainFrame.Navigate(new KalorintarveValinta(MainFrame));
        //    MainFrame.Visibility =Visibility.Visible;

        //}
        private void katsoReseptitNavButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Page2(mainFrame));
            mainFrame.Visibility = Visibility.Visible;
        }

        private void kaloritarveNavButton_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new KalorintarveValinta(mainFrame));
            mainFrame.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }

}