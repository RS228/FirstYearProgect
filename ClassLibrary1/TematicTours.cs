using System;
using System.Collections.Generic;
using System.Text;

namespace TourLibrary
{
    public class TematicTours : Tour
    {
        public delegate void TematicToursHandler(string message);
        public event TematicToursHandler UpPriceTematicTours;
        public override void PersonalGuide()
        {
            decimal Up = 150;
            PricePerDay += Up;
            if (UpPriceTematicTours != null)
            {
                UpPriceTematicTours($"Денна цiна туру збiльшена на {Up} грн\nЦiна за день: {this.PricePerDay} грн");
            }
        }
        public override void PersonalCar()
        {

            decimal Up = 220;
            PricePerDay += Up;
            if (UpPriceTematicTours != null)
            {
                UpPriceTematicTours($"Денна цiна туру збiльшена на {Up} грн\nЦiна за день: {this.PricePerDay} грн");
            }
        }
        public override void BreakfastInBed()
        {
            decimal Up = 75;
            PricePerDay += Up;
            if (UpPriceTematicTours != null)
            {
                UpPriceTematicTours($"Денна цiна туру збiльшена на {Up} грн\nЦiна за день: {this.PricePerDay} грн");
            }
        }

    }
}
