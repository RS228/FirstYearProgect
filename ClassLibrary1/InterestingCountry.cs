using System;
using System.Collections.Generic;
using System.Text;

namespace TourLibrary
{
    public class InterestingCountry : Tour
    {
        public delegate void InterestingCountryHandler(string message);
        public event InterestingCountryHandler UpPriceInterestingCountry;
        public override void PersonalGuide()
        {
            decimal Up = 100;
            PricePerDay += Up;
            if (UpPriceInterestingCountry != null)
            {
                UpPriceInterestingCountry($"Денна цiна туру збiльшена на {Up} грн\nЦiна за день: {this.PricePerDay} грн");
            }
        }
        public override void PersonalCar()
        {
            decimal Up = 200;
            PricePerDay += Up;
            if (UpPriceInterestingCountry != null)
            {
                UpPriceInterestingCountry($"Денна цiна туру збiльшена на {Up} грн\nЦiна за день: {this.PricePerDay} грн");
            }
        }
        public override void BreakfastInBed()
        {
            decimal Up = 70;
            PricePerDay += Up;
            if (UpPriceInterestingCountry != null)
            {
                UpPriceInterestingCountry($"Денна цiна туру збiльшена на {Up} грн\nЦiна за день: {this.PricePerDay} грн");
            }
        }

    }

}
