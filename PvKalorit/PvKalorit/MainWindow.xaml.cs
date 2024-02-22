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

namespace PvKalorit
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

        private void laskeButton_Click(object sender, RoutedEventArgs e)
        {
            double paino;
            double pituus;
            int ikä;
            
            try
            {
                paino = double.Parse(painoBox.Text);
              
            }

            catch
            {
                paino = 0;
            }

            try
            {
                pituus = double.Parse(pituusBox.Text);

            }

            catch
            {
                pituus = 0;
            }

            try
            {
                ikä = int.Parse(ikäBox.Text);

            }

            catch
            {
                ikä = 0;
            }

            



            double bmr = laskeBmr(paino, pituus, ikä);
            double päivänKalorinTarve = laskePäivänKalorinTarve(bmr);




            double laskeBmr(double paino, double pituus, int ikä)
            {
                double bmr = 0;

                if(miesButton.IsChecked == true)
                {
                    bmr = (10 * paino) + (6.25 * pituus) - (5 * ikä) + 5;
                }

                else if (nainenButton.IsChecked == true)
                {
                    bmr = (10 * paino) + (6.25 * pituus) - (5 * ikä) + - 161;
                }

                else
                {

                }
                return bmr;
            
            }
          
            double laskePäivänKalorinTarve( double bmr)
            {
                double päivänKalorinTarve = 0;

                if (lepoRbutton.IsChecked == true)
                {
                    päivänKalorinTarve = bmr * 1.0;
                }

                else if (kevytRbutton.IsChecked == true)
                {
                   päivänKalorinTarve = bmr * 1.3;
                }

                else if (normaaliRbutton.IsChecked == true)
                {
                    päivänKalorinTarve = bmr * 1.5;
                }

                else if (normaaliRbutton.IsChecked == true)
                {
                    päivänKalorinTarve = bmr * 1.9;
                }

                else if (raskasRbutton.IsChecked == true)
                {
                    päivänKalorinTarve = bmr * 2.2;
                }

                else if (erittäinRaskasRbutton.IsChecked == true)
                {
                    päivänKalorinTarve = bmr * 2.5;
                }

                else
                {

                }

                return päivänKalorinTarve;
            }
            

            näytäKaloritBox.Text = (päivänKalorinTarve).ToString();



        }
    }
}