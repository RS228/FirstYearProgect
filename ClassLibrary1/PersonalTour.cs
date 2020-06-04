using System;
using System.Collections.Generic;
using System.Text;

namespace TourLibrary
{
    public class PersonalTour : Tour
    {
        public PayStatus Status { get; set; }
        public Decimal Price { get; private set; }
        public Agency TourAgency { get; set; }
        public AirCompany TourAirCompany { get; set; }
        public string WhereAreYouGo { get; set; }
        public Decimal CountPrice()
        {
            return this.Price = this.PricePerDay * this.TourDuration;
        }
    }
}
