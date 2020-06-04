using System;
using System.Collections.Generic;
using System.Text;


namespace TourLibrary
{
    public class Account
    {
        
        public delegate void AccountHandler(object sender,string message);
        public event AccountHandler Notify;
        public string Name { get; set; }
        public decimal Bill { get; set; }
        public List<PersonalTour> AllClientsTour { get; set; }
        public Account()
        {
            Name = null;
            Bill = 0;
            AllClientsTour = new List<PersonalTour>();
        }
        public Account(decimal sum, string name)
        {
            Name = name;
            Bill = sum;
            AllClientsTour = new List<PersonalTour>();
        }
        public string ShowBill()
        {

            if (Notify != null)
            {
                Notify(this,$"Баланс вашого рахунку: {Bill} грн");
            }
            return "Дякуємо, що обрали наш Клуб !";
        }
        public void Put(decimal sum)
        {
            Bill += sum;
            if (Notify != null)
            {
                Notify(this,$"На рахунок поступило: {sum} грн\nВаш баланс: {Bill} грн");
            }
        }
        public void PayFor(decimal price)
        {
            if (Bill >= price)
            {
                Bill -= price;
                if (Notify != null)
                {
                    Notify(this,$"Тур оплачено. З рахунку знято: {price} грн. Ваш баланс: {Bill} грн");
                } // 2.Вызов события
            }
            else
            {
                Notify?.Invoke(this,$"Недостатньо грошей. Ваш баланс: {Bill} грн");
                throw new LowMoneyException("Недостатньо грошей на рахунку");
            }
        }
        public static void DisplayMessage(object sender, string message)
        {
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(message);
            Console.ForegroundColor = color;
        }

    }
}
