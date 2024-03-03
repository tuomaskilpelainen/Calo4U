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
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Calo4U_GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int CaloriesEaten = 13000;
        int GoalCalories = 20000;
        int LastCaloriesEaten = 17000;
        int RemainingCalories = 0;
        int PieChartCalories = 0;
        public MainWindow()
        {
            InitializeComponent();
            PieChartCalories = CaloriesEaten;
            PieChart();
        }

        private void etusivuNavButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void LisääReseptiNavButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Page1(MainFrame));
            MainFrame.Visibility = Visibility.Visible;
        }

        private void katsoReseptitNavButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Page2(MainFrame));
            MainFrame.Visibility = Visibility.Visible;
        }

        private void kaloritarveNavButton_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new KalorintarveValinta(MainFrame));
            MainFrame.Visibility = Visibility.Visible;
        }
        public void ShowMainWindow()
        {
            MainFrame.Visibility = Visibility.Collapsed;
        }

        private void PieChart()
        {
            RemainingCalories = GoalCalories - PieChartCalories;
            saavMääräNroBlock.Text = PieChartCalories.ToString();
            calJälBlock.Text = RemainingCalories.ToString();
            tavoiteNroBlock.Text = GoalCalories.ToString();
            int RemainingCalories1 = 0;
            RemainingCalories1 = RemainingCalories;
            if (RemainingCalories1 < 0)
            {
                RemainingCalories1 = 0;
            }
            pieChart.Series = new LiveCharts.SeriesCollection
            {
                new LiveCharts.Wpf.PieSeries
                {
                    Title = "Viikon syödyt kalorit",
                    Values = new LiveCharts.ChartValues<int> { PieChartCalories },
                    DataLabels = true,
                    Fill = new SolidColorBrush(Color.FromRgb(46, 51, 78))
                },
                new LiveCharts.Wpf.PieSeries
                {
                    Title = "Viikon Jäljellä olevat kalorit",
                    Values = new LiveCharts.ChartValues<int> { RemainingCalories1 },
                    DataLabels = true,
                    Fill = new SolidColorBrush(Color.FromRgb(255, 138, 81))
                }
            };
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Manuaalinen syöttö")
            {
                textBox.Text = "";
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Manuaalinen syöttö";
            }
        }
    }
}