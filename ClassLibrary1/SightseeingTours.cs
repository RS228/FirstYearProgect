using System;
using System.Collections.Generic;
using System.Text;

namespace TourLibrary
{
    public class SightseeingTours : Tour
    {
        public delegate void SightseeingToursHandler(string message);
        public event SightseeingToursHandler UpPriceSightseeingTours;
        public override void PersonalGuide()
        {
            decimal Up = 170;
            PricePerDay += Up;
            if (UpPriceSightseeingTours != null)
            {
                UpPriceSightseeingTours($"Денна цiна туру збiльшена на {Up} грн\nЦiна за день: {this.PricePerDay} грн");
            }
        }
        public override void PersonalCar()
        {
            decimal Up = 250;
            PricePerDay += Up;
            if (UpPriceSightseeingTours != null)
            {
                UpPriceSightseeingTours($"Денна цiна туру збiльшена на {Up} грн\nЦiна за день: {this.PricePerDay} грн");
            }

        }
        public override void BreakfastInBed()
        {
            decimal Up = 80;
            PricePerDay += Up;
            if (UpPriceSightseeingTours != null)
            {
                UpPriceSightseeingTours($"Денна цiна туру збiльшена на {Up} грн\nЦiна за день: {this.PricePerDay} грн");
            }
        }

    }
}
