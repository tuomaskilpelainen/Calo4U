﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calo4U_Sisa
{
    internal class Kayttaja
    {
        public string Nimi {  get; set; }
        public  double PvaKalorit {  get; set; }
        public double HaeKayttajanKalorit()
        {
            return PvaKalorit;
        }

    }
}
