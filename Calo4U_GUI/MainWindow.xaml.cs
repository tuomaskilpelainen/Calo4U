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

        private void kalorintarveButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void uusReseptButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Page1());
            MainFrame.Visibility = Visibility.Visible;
        }
        public void ShowMainWindow()
        {
            MainFrame.Visibility = Visibility.Collapsed;
        }

        private void katsoReseptitButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Page2());
            MainFrame.Visibility = Visibility.Visible;
        }

        private void uusReseptButton_Copy_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}