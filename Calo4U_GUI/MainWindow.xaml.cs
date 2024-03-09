using System.Text;
using System.Text.Json;
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
using System;
using System.IO;
using System.Text.Json;


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
            LataaKayttajanTiedot();
            LataaKayttajanReseptit();
            JsonTarkistaja();
            PieChart();

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
                saavMääräNroBlock.Text = tiedot[2];
                calJälBlock.Text = tiedot[3];
            }
            else
            {
                calJälBlock.Text = "0";
                saavMääräNroBlock.Text = "0";
                vkoNroTextBlock.Text = "0";
                tavoiteNroBlock.Text = "0";
            }
        }


        public void PieChart()
        {
            int RemainingCalories = 0;
            int PieChartCalories = 0;

            Viikkotallennus viikkohakija = new Viikkotallennus();
            viikkohakija.CheckJson();

            int.TryParse(calJälBlock.Text, out RemainingCalories);
            int.TryParse(saavMääräNroBlock.Text, out PieChartCalories);

            if (RemainingCalories < 0)
            {
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
                    Title = "Viikon jäljellä olevat kalorit",
                    Values = new LiveCharts.ChartValues<int> { 0 },
                    DataLabels = true,
                    Fill = new SolidColorBrush(Color.FromRgb(255, 138, 81))
                }
                };
                Main main = new Main();
                bool ok = main.TarkistaKayttaja();
                if (ok == true) 
                {
                    TeeKayttajaBox.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
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
                    Title = "Viikon jäljellä olevat kalorit",
                    Values = new LiveCharts.ChartValues<int> { RemainingCalories },
                    DataLabels = true,
                    Fill = new SolidColorBrush(Color.FromRgb(255, 138, 81))
                }
                };
            }
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
                        Main.SyoResepti(resepti.ToLower(), miinus);
                        LataaKayttajanTiedot();
                        LataaKayttajanReseptit();
                    }
                }
            }
            else if (!string.IsNullOrEmpty(resepti))
            {
                Main.SyoResepti(resepti.ToLower(), miinus);
                LataaKayttajanTiedot();
            }
            PieChart();

        }

        private void poistaButton_Click(object sender, RoutedEventArgs e)
        {
            Main main = new Main(); 
            bool miinus = true;
            double kalorit = 0;
            bool ok; //True jos kalorit talenettiin onnistuneesti false jos ei
            if (!string.IsNullOrEmpty(calSyöttöTextBox.Text))
            {
                try
                {
                    kalorit -= double.Parse(calSyöttöTextBox.Text);
                    ok = main.SyoKalorit(kalorit); // +- kalorit käyttäjältä paluttaa true tai false onnistui / ei
                    calSyöttöTextBox.Text = string.Empty;
                    LataaKayttajanTiedot();
                    LataaKayttajanReseptit();

                }
                catch (Exception ex)
                {
                    if (!string.IsNullOrEmpty(resepti))
                    {
                        main.PoistaKayttajanResepti(resepti); // Poistaa käyttäjän reseptin kokonaan
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
            if (string.IsNullOrEmpty(resepti1)) { return; }
            resepti = resepti1.Split(',')[0];

        }


        private int viikkoKalorit;
        private int syodytKalorit;

        private void edellinenButton_Click(object sender, RoutedEventArgs e)
        {
            JsonTarkistaja();
            int viikkonumero = Convert.ToInt32(vkoNroTextBlock.Text);
            string jsonFilePath = "viikkoKalorit.json";

            bool found = false;

            if (viikkonumero > 1)
            {
                viikkonumero -= 1;

                try
                {
                    string jsonString = File.ReadAllText(jsonFilePath);
                    JsonDocument jsonDoc = JsonDocument.Parse(jsonString);

                    foreach (JsonElement element in jsonDoc.RootElement.EnumerateArray())
                    {
                        int viikko = element.GetProperty("Viikko").GetInt32();

                        if (viikko == viikkonumero)
                        {
                            found = true;
                            viikkoKalorit = element.GetProperty("PvaKalorit").GetInt32() * 7;
                            syodytKalorit = element.GetProperty("SyodytKalorit").GetInt32();
                            break;
                        }
                    }

                    if (!found)
                    {
                        vkoNroTextBlock.Text = viikkonumero.ToString();
                        tavoiteNroBlock.Text = "0";
                        saavMääräNroBlock.Text = "0";
                        calJälBlock.Text = "0";
                    }
                    else
                    {
                        vkoNroTextBlock.Text = viikkonumero.ToString();
                        tavoiteNroBlock.Text = viikkoKalorit.ToString();
                        saavMääräNroBlock.Text = syodytKalorit.ToString();
                        calJälBlock.Text = (viikkoKalorit - syodytKalorit).ToString();
                    }
                    PieChart();
                }
                catch (FileNotFoundException)
                {

                }

            }
        }

        private void seuraavaButton_Click(object sender, RoutedEventArgs e)
        {
            JsonTarkistaja();
            int viikkonumero = Convert.ToInt32(vkoNroTextBlock.Text);
            string jsonFilePath = "viikkoKalorit.json";


            bool found = false;

            if (viikkonumero < viikkoraja)
            {
                viikkonumero += 1;

                try
                {
                    string jsonString = File.ReadAllText(jsonFilePath);
                    JsonDocument jsonDoc = JsonDocument.Parse(jsonString);

                    foreach (JsonElement element in jsonDoc.RootElement.EnumerateArray())
                    {
                        int viikko = element.GetProperty("Viikko").GetInt32();

                        if (viikko == viikkonumero)
                        {
                            found = true;
                            viikkoKalorit = element.GetProperty("PvaKalorit").GetInt32() * 7;
                            syodytKalorit = element.GetProperty("SyodytKalorit").GetInt32();
                            break;
                        }
                    }

                    if (!found)
                    {
                        vkoNroTextBlock.Text = viikkonumero.ToString();
                        tavoiteNroBlock.Text = "0" ;
                        saavMääräNroBlock.Text = "0";
                        calJälBlock.Text = "0";
                    }
                    else
                    {
                        vkoNroTextBlock.Text = viikkonumero.ToString();
                        tavoiteNroBlock.Text = viikkoKalorit.ToString();
                        saavMääräNroBlock.Text = syodytKalorit.ToString();
                        calJälBlock.Text = (viikkoKalorit - syodytKalorit).ToString();
                    }
                    PieChart();
                }
                catch (FileNotFoundException)
                {

                }

            }

        }

        private void tämänHetkiButton_Click(object sender, RoutedEventArgs e)
        {
            LataaKayttajanTiedot();
            PieChart();
        }

        int viikkoraja;

        // Tarkistaa suurimman viikkoarvon ja syöttää sen viikkoraja arvoksi
        private void JsonTarkistaja()
        {
            try
            {
                string json = File.ReadAllText("viikkoKalorit.json");
                using (JsonDocument document = JsonDocument.Parse(json))
                {
                    JsonElement root = document.RootElement;
                    JsonElement.ArrayEnumerator arrayEnumerator = root.EnumerateArray();

                    int suurinViikko = int.MinValue;

                    while (arrayEnumerator.MoveNext())
                    {
                        JsonElement viikkoElement = arrayEnumerator.Current.GetProperty("Viikko");

                        int viikkoValue = viikkoElement.GetInt32();

                        if (viikkoValue > suurinViikko)
                        {
                            suurinViikko = viikkoValue;
                        }
                    }
                    viikkoraja = suurinViikko;
                }
            }
            catch (FileNotFoundException)
            {

            }
        }

        
    }
}