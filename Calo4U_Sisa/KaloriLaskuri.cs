using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo4U_Sisa
{
    internal class KaloriLaskuri
    {
        public double Bmr(double paino, double pituus, int ikä, string sukupuoli, string aktiivisuus, string tavoite)
        {
            double bmr = 0;

            if (sukupuoli == "m")
            {
                bmr = (10 * paino) + (6.25 * pituus) - (5 * ikä) + 5;
            }

            else if (sukupuoli == "n")
            {
                bmr = (10 * paino) + (6.25 * pituus) - (5 * ikä) + -161;
            }

            else
            {
                bmr = 0;
                return bmr;
            }

            

            if (aktiivisuus == "l")
            {
                bmr = bmr * 1.0;
            }

            else if (aktiivisuus == "k")
            {
                bmr = bmr * 1.3;
            }

            else if (aktiivisuus == "t")
            {
                bmr = bmr * 1.5;
            }

            else if (aktiivisuus == "ko")
            {
                bmr = bmr * 1.9;
            }

            else if (aktiivisuus == "r")
            {
                bmr = bmr * 2.2;
            }

            else if (aktiivisuus == "e")
            {
                bmr = bmr * 2.5;
            }

            else
            {
                bmr = 0;
                return bmr;
            }

            if (tavoite == "y")
            {
                bmr = bmr;
            }

            else if (tavoite == "p")
            {
                bmr -= 500;
            }

            else if (tavoite == "k")
            {
                bmr += 500;
            }


            
            return bmr;


        }
    }
}
