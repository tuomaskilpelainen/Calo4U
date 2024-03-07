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
        private List<string> KayttajanReseptit = new List<string>(); // Kaikki käyttäjän valmiit reseptit
        private string resepti = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
            MainWindow_Loaded();
            PieChartCalories = CaloriesEaten;
            PieChart();
            LataaKayttajanTiedot();
            LataaKayttajanReseptit();

        }
        private void MainWindow_Loaded()
        {
            Main main = new Main();
            main.TarkistaViikko();
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
            PieChart();
        }

        public void LataaKayttajanTiedot()
        {
            Main main = new Main();
            string[] tiedot = main.LataaKayttajanTiedot(); //0 viikko, 1 Tavoite, 2 Saavutettu, kaloreita jäljellä. Hakee käyttän tiedot Tallentajasta ja muutta ne Mainissa oikeaan muotoon
            if (tiedot[0] != null)
            {
                vkoNroTextBlock.Text = tiedot[0];
                tavoiteNroBlock.Text = tiedot[1];
                saavMääräNroBlock.Text= tiedot[2];
                calJälBlock.Text = tiedot[3];

            }
        }


        public void PieChart()
        {
            int RemainingCalories = 0;
            int PieChartCalories = 0;
            Main main = new Main();
            string[] tiedot = main.LataaKayttajanTiedot();

            if (tiedot[0] != null)
            {
                PieChartCalories = Convert.ToInt32(tiedot[2].Replace(",", ".").Split(".")[0]);
                RemainingCalories = Convert.ToInt32(tiedot[1].Replace(",", ".").Split(".")[0]) - PieChartCalories;

            }

            if (RemainingCalories < 0)
            {
                RemainingCalories = 0;
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
                    Values = new LiveCharts.ChartValues<int> { RemainingCalories },
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





        private void syöButton_Click(object sender, RoutedEventArgs e)
        {
            bool miinus = false;
            double kalorit = 0;
            bool ok; //True jos kalorit talenettiin onnistuneesti false jos ei
            if (!string.IsNullOrWhiteSpace(calSyöttöTextBox.Text))
            {
                try
                {
                    kalorit += double.Parse(calSyöttöTextBox.Text);
                    Main main = new Main();
                    ok = main.SyoKalorit(kalorit); // +- kalorit käyttäjältä palauttaa true tai false onnistui / ei
                    calSyöttöTextBox.Text = string.Empty;
                    LataaKayttajanTiedot();

                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(resepti))
                    {
                        Main.SyoResepti(resepti, miinus);
                        LataaKayttajanTiedot();
                        LataaKayttajanReseptit();
                    }
                }
            }
            else if (!string.IsNullOrEmpty(resepti))
            {
                Main.SyoResepti(resepti, miinus);
                LataaKayttajanTiedot();
            }
            PieChart();

        }

        private void poistaButton_Click(object sender, RoutedEventArgs e)
        {
            bool miinus = true;
            double kalorit = 0;
            bool ok; //True jos kalorit talenettiin onnistuneesti false jos ei
            if (!string.IsNullOrEmpty(calSyöttöTextBox.Text))
            {
                try
                {
                    kalorit -= double.Parse(calSyöttöTextBox.Text);
                    Main main = new Main();
                    ok = main.SyoKalorit(kalorit); // +- kalorit käyttäjältä paluttaa true tai false onnistui / ei
                    calSyöttöTextBox.Text = string.Empty;
                    LataaKayttajanTiedot();
                    LataaKayttajanReseptit();

                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(resepti))
                    {
                        Main.SyoResepti(resepti, miinus);
                        LataaKayttajanTiedot();
                        LataaKayttajanReseptit();
                    }
                }
            }
            PieChart();
        }

        public void LataaKayttajanReseptit()
        {
            KayttajanReseptit = Main.KayttajanReseptitS(); // Hakee mainista käyttäjän kaikki valmiit reseptit
            KayttajanReseptitBox.ItemsSource = null;
            KayttajanReseptitBox.ItemsSource = KayttajanReseptit;

        }

        private void Lista_Click(object sender, MouseButtonEventArgs e)
        {
            string resepti1 = (string)KayttajanReseptitBox.SelectedItem as string;
            if (string.IsNullOrEmpty(resepti1) ) { return; }
            resepti = resepti1.Split(',')[0];

        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}