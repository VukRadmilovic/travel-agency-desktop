using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Turisticka_Agencija.Models;

namespace Turisticka_Agencija.Utils
{
    internal class TripUser
    {
        public bool IsBought { get; set; }
        public bool IsReserved { get; set; }
        public Trip Trip { get; set; }
        public TripUser(Trip trip, bool isBought, bool isReserved)
        {
            Trip = trip;
            IsBought = isBought;
            IsReserved = isReserved;
        }
    }
}
