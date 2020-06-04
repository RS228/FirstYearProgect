using System;
using System.Collections.Generic;
using System.Text;

namespace TourLibrary
{
    public abstract class Tour
    {
        public DateTime DateOfTour { get; set; }
        public string Path { get; set; }
        public string TourType { get; set; }
        public string Name { get; set; }
        public int TourDuration { get; set; }
        public decimal PricePerDay { get; set; }
        public abstract void BreakfastInBed();
        public abstract void PersonalGuide();
        public abstract void PersonalCar();
        public static void DisplayMessage(string message)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(message);
            Console.ForegroundColor = color;
        }

    }
}
