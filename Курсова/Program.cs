using System;
using TourLibrary;
using System.Collections.Generic;
namespace TourApplication
{
    class Program
    {
        public static void ChoiseMethod(Tour []TourList, PersonalTour LikeTour, Account ClientNewAccount, List<Agency> AllAgencies, int choice,Club YourClub)
        {

            LikeTour.WhereAreYouGo = TourList[choice - 1].Name;
            LikeTour.Path = TourList[choice - 1].Path;
            LikeTour.PricePerDay = TourList[choice - 1].PricePerDay;
            Console.WriteLine("Напишiть будь ласка дату та час на який ви плануєте подорож у форматi: дд.мм.рррр ");
            int flag = 0;
            while (flag == 0)
            {
                try
                {
                    LikeTour.DateOfTour = DateTime.Parse(Console.ReadLine());
                    flag = 1;
                }
                catch (FormatException)
                {
                    ConsoleColor color = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Притримуйтесь будь ласка заданих параметрiв вводу");
                    Console.ForegroundColor = color;
                    flag = 0;

                }
            }
            Console.WriteLine("Доступнi Тур агенцiї:");
            Agency[] AvailableAgencies = YourClub.FindAgency(LikeTour);
            if (AvailableAgencies.Length > 0)
            {
                for (int i = 0; i < AvailableAgencies.Length; i++)
                {
                    Console.WriteLine(String.Format("{0}. {1}", i + 1, AvailableAgencies[i]._Name));

                }
            }
            else
            {
                throw new AgencyNotFoundException("На даний тур немає доступних агенцiй");
            }
            Console.WriteLine("Оберiть Aгенцiю:");

            int AgencyChoise = int.Parse(Console.ReadLine());
            LikeTour.TourAgency = AvailableAgencies[AgencyChoise - 1];

            Console.WriteLine("Доступнi авiакомпанiї :");
            AirCompany[] AvailableCompanies = LikeTour.TourAgency.FindAirCompany(LikeTour);
            if (AvailableCompanies.Length > 0)
            {
                for (int i = 0; i < AvailableCompanies.Length; i++)
                {
                    Console.WriteLine(String.Format("{0}. {1}", i + 1, AvailableCompanies[i]._Name));

                }
            }
            else
            {
                throw new AirCompanyNotFoundException("На задану дату немає доступних Авiакомпанiй");
            }
            Console.WriteLine("Оберiть компанiю :");
            int CompanyChoise = int.Parse(Console.ReadLine());
            LikeTour.TourAirCompany = AvailableCompanies[CompanyChoise - 1];
            Console.WriteLine("Введiть кiлькiсть днiв подорожi :");
            LikeTour.TourDuration = int.Parse(Console.ReadLine());
            Console.WriteLine("Нашi додатковi послуги:\n1.Персональний Гiд\n2.Персональна машина\n3.Снiданок у постiль\nНатиснiть будь-яку клавiшу, якщо додатковi послуги не потрiбнi");
            switch (Console.ReadLine())
            {
                case "1":
                    {
                        TourList[choice - 1].PersonalGuide();
                        LikeTour.PricePerDay = TourList[choice - 1].PricePerDay;
                        break;
                    }
                case "2":
                    {
                        TourList[choice - 1].PersonalCar();
                        LikeTour.PricePerDay = TourList[choice - 1].PricePerDay;
                        break;
                    }
                case "3":
                    {
                        TourList[choice - 1].BreakfastInBed();
                        LikeTour.PricePerDay = TourList[choice - 1].PricePerDay;
                        break;
                    }
                default:
                    break;
            }
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Цiна за Ваш тур:" + LikeTour.CountPrice() + "гривень");
            bool flag2 = true;
            while (flag2)
            {
                Console.WriteLine("Натиснiть 1 для оплати туру\nНатиснiть 2 для збереження туру у вашому аккаунтi(можна оплатити пiзнiше)\nНатиснiть будь-яку клавiшу, щоб повернутися на головне меню");
                switch (Console.ReadLine())
                {
                    case "1":
                        {
                            try
                            {
                                ClientNewAccount.PayFor(LikeTour.Price);
                                Console.ReadKey();
                                Console.Clear();
                                Console.WriteLine("##############################################");
                                Console.WriteLine(String.Format("Ваше замовлення: Тур '{0}'\nТип туру: {1}\nТур: {2}\nМаршрут авiапревезення: {8}\nДата початку подорожi: {3}\nТривалiсть подорожi: {4} днiв\nТур агенцiя, яка Вас обслуговує: {7}\nАвiакомпанiя, яка Вас обслуговує: {5}\nЦiна: {6} грн", LikeTour.Name, LikeTour.TourType, LikeTour.WhereAreYouGo, LikeTour.DateOfTour.Date, LikeTour.TourDuration, LikeTour.TourAirCompany._Name, LikeTour.Price, LikeTour.TourAgency._Name, LikeTour.Path));
                                LikeTour.Status = PayStatus.Paid;
                                ClientNewAccount.AllClientsTour.Add(LikeTour);
                                flag2 = false;
                                Console.ReadKey();

                            }
                            catch (LowMoneyException)
                            {
                                Console.WriteLine("\nНатиснiть 1 для поповнення рахунку\nНатиснiть будь-яку клавiшу для повернення в головне меню");
                                switch (Console.ReadLine())
                                {
                                    case "1":
                                        {
                                            string choicepay = "1";
                                            while (choicepay == "1")
                                            {
                                                try
                                                {
                                                    Console.WriteLine("Введiть суму для поповнення: ");
                                                    int Sum = int.Parse(Console.ReadLine());
                                                    if (Sum < 0)
                                                    {
                                                        throw new NegativeAmountException("The amount to replenish the account cannot be negative");
                                                    }
                                                    ClientNewAccount.Put(Sum);
                                                    Console.ReadKey();
                                                    choicepay = "0";
                                                }
                                                catch (FormatException)
                                                {
                                                    ConsoleColor color = Console.ForegroundColor;
                                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                    Console.WriteLine("Ви ввели невiрно!");
                                                    Console.ForegroundColor = color;
                                                    Console.WriteLine("Натиснiть 1 , щоб повтории спробу\nНатиснiть будь-яку клавiшу для продовження");
                                                    choicepay = Console.ReadLine();

                                                }
                                            }
                                            break;
                                        }
                                    default:
                                        {
                                            flag2 = false;
                                            break;
                                        }
                                }

                            }
                            break;
                        }
                    default:
                        {
                            flag2 = false;
                            break;
                        }
                    case "2":
                        {
                            LikeTour.Status = PayStatus.NotPaid;
                            ClientNewAccount.AllClientsTour.Add(LikeTour);
                            flag2 = false;
                            break;
                        }
                }
            }

        }
        static void Main(string[] args)
        {
            Club RomansClub = new Club("Roman's Club");
            string[] TourTypes = new string[] { "Країни, якi цiкавi для туризму", "Тематичнi тури", "Тури по видатних мiсцях" };
            InterestingCountry Ukraine = new InterestingCountry() { TourType = TourTypes[0], Name = "Україна", PricePerDay = 354, Path = "Ukraine" };
            InterestingCountry Poland = new InterestingCountry() { TourType = TourTypes[0], Name = "Польща", PricePerDay = 451, Path = "Poland" };
            InterestingCountry France = new InterestingCountry() { TourType = TourTypes[0], Name = "Францiя", PricePerDay = 543, Path = "France" };
            InterestingCountry Germany = new InterestingCountry() { TourType = TourTypes[0], Name = "Нiмеччина", PricePerDay = 578, Path = "Germany" };
            InterestingCountry Italy = new InterestingCountry() { TourType = TourTypes[0], Name = "Iталiя", PricePerDay = 605, Path = "Italy" };

            RomansClub.InterestingCountryTours.AddRange(new InterestingCountry[] { Ukraine, Poland, France, Germany, Italy });

            TematicTours WineTour = new TematicTours { TourType = TourTypes[1], Name = "Винний тур", PricePerDay = 345, Path = "Italy" };
            TematicTours HistoryTour = new TematicTours { TourType = TourTypes[1], Name = "Iсторичний  тур", PricePerDay = 243, Path = "Ukraine" };
            TematicTours RocksTour = new TematicTours { TourType = TourTypes[1], Name = "Гiрнолижний тур", PricePerDay = 565, Path = "Iceland" };
            TematicTours RomanticTour = new TematicTours { TourType = TourTypes[1], Name = "Романтичний тур", PricePerDay = 670, Path = "France" };
            TematicTours BullfightingTour = new TematicTours { TourType = TourTypes[1], Name = "Корида тур", PricePerDay = 445, Path = "Spain" };

            RomansClub.TematicToursTours.AddRange(new TematicTours[] { WineTour, HistoryTour, RocksTour, RomanticTour, BullfightingTour });

            SightseeingTours Akureiri = new SightseeingTours() { TourType = TourTypes[2], Name = "Акюрейрi", PricePerDay = 459, Path = "Iceland" };
            SightseeingTours Azores = new SightseeingTours() { TourType = TourTypes[2], Name = "Азорськi острови", PricePerDay = 567, Path = "Portugal" };
            SightseeingTours Malaga = new SightseeingTours() { TourType = TourTypes[2], Name = "Малага", PricePerDay = 673, Path = "Spain" };
            SightseeingTours Piedmont = new SightseeingTours() { TourType = TourTypes[2], Name = "П'ємонт", PricePerDay = 689, Path = "Italy" };
            SightseeingTours Tromse = new SightseeingTours() { TourType = TourTypes[2], Name = "Тромсе", PricePerDay = 708, Path = "Norway" };

            RomansClub.SightseeingToursTours.AddRange(new SightseeingTours[] { Akureiri, Azores, Malaga, Piedmont, Tromse });

            int[] EvenDays = new int[0];//масив парних днів
            for (int i = 0; i <= 31; i++)
            {
                if (i % 2 == 0)
                {
                    Array.Resize(ref EvenDays, EvenDays.Length + 1);
                    EvenDays[EvenDays.Length - 1] = i;
                }
            }
            int[] OddDays = new int[0];//масив непарних днів
            for (int i = 0; i <= 31; i++)
            {
                if (i % 2 != 0)
                {
                    Array.Resize(ref OddDays, OddDays.Length + 1);
                    OddDays[OddDays.Length - 1] = i;
                }
            }
            AirCompany MAU = new AirCompany("MAU", new string[] { "Ukraine", "Poland", "Iceland" }, EvenDays);
            AirCompany MotorSich = new AirCompany("Мотор Сiч", new string[] { "Ukraine", "Italy", "Iceland" }, OddDays);
            AirCompany Belavia = new AirCompany("Belavia", new string[] { "Italy", "Germany ", "Portugal" }, EvenDays);
            AirCompany AirFrance = new AirCompany("Air France", new string[] { "France", "Portugal", "Norway" }, OddDays);
            AirCompany KLM = new AirCompany("KLM", new string[] { "Germany", "Poland", " Norway" }, EvenDays);
            AirCompany DniproAvia = new AirCompany("Днiпро Авiа", new string[] { "Ukraine", "Germany", "Norway" }, OddDays);
            AirCompany YanAir = new AirCompany("Yan Air", new string[] { "Italy", "Poland", "Spain", "Germany" }, OddDays);
            AirCompany Pegasus = new AirCompany("Pegasus Airlines", new string[] { "France", "Italy", "Spain" }, EvenDays);

            AirCompany[] AllAirCompanies = new AirCompany[] { MAU, MotorSich, Belavia, AirFrance, KLM, DniproAvia, YanAir, Pegasus };

            Agency JoinUp = new Agency("Join Up", new AirCompany[] { MAU, MotorSich, KLM, DniproAvia, YanAir, Pegasus }, new Tour[] { Ukraine, Poland, Akureiri, Piedmont, HistoryTour, RocksTour });
            Agency ComeWithUs = new Agency("Поїхали з нами", new AirCompany[] { AirFrance, YanAir, Pegasus, MotorSich, MAU, Belavia, KLM }, new Tour[] { France, Italy, Akureiri, Azores, Tromse, WineTour , RocksTour, RomanticTour });
            Agency HotTour = new Agency("Гарячий Тур", new AirCompany[] { Belavia, KLM, YanAir, AirFrance, Pegasus, MotorSich }, new Tour[] { Germany, Azores, Malaga, Piedmont, Tromse, BullfightingTour });
            Agency KingsTour = new Agency("Королiвський Тур", new AirCompany[] { MAU, MotorSich, KLM, DniproAvia, YanAir, Pegasus }, new Tour[] { Ukraine, Poland, Akureiri, Piedmont, HistoryTour, RocksTour });
            Agency AsterraTravel = new Agency("Asterra Travel", new AirCompany[] { AirFrance, YanAir, Pegasus, MotorSich, MAU, Belavia, KLM }, new Tour[] { France, Italy, Akureiri, Azores, Tromse, WineTour, RocksTour, RomanticTour });
            Agency DreamJourney = new Agency("Подорож мрiї", new AirCompany[] { Belavia, KLM, YanAir, AirFrance, Pegasus, MotorSich }, new Tour[] { Germany, Azores, Malaga, Piedmont, Tromse, BullfightingTour });

            RomansClub.PartnerAgencies.AddRange(new Agency[] { JoinUp, ComeWithUs, HotTour, KingsTour, AsterraTravel, DreamJourney });

            Account[] RealAccounts = new Account[] { new Account(100, "Roman") };
            RomansClub.RealAccounts.AddRange(RealAccounts);
 
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\t\t\t\tДоброго дня, Вас вiтає клуб туризму 'Roman's Club'");
            Console.WriteLine("Введiть будь ласка iм'я свого акаунтy");
            string NameOfAccount = Console.ReadLine();
            Account ClientNewAccount = new Account();
            for (int i = 0; i < RomansClub.RealAccounts.Count; i++)
            {
                if (NameOfAccount == RomansClub.RealAccounts[i].Name)
                {
                    ClientNewAccount = RomansClub.RealAccounts[i];
                }
            }
            if (ClientNewAccount.Name != null)
            {
                ClientNewAccount.Notify += Account.DisplayMessage;
                Console.WriteLine($"Ваш аккаунт :{ClientNewAccount.Name}\nБаланс акаунтy:{ClientNewAccount.Bill} грн");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine($"Ваш акаунт не знайдено\nДля створення акаунтy введiть Ваше прiзвище:");
                ClientNewAccount = new Account(0, Console.ReadLine());
                RomansClub.RealAccounts.Add(ClientNewAccount);
                ClientNewAccount.Notify += Account.DisplayMessage;
                Console.WriteLine("Якщо хочете поповнити рахунок натиснiть будь-яку цифру\nНатиснiть будь-яку клавiшу для продовження");
                switch (int.TryParse(Console.ReadLine(), out int result))
                {
                    case true:
                        {
                            string choicepay = "1";
                            while (choicepay == "1")
                            {
                                try
                                {
                                    Console.WriteLine("Введiть суму для поповнення: ");
                                    int Sum = int.Parse(Console.ReadLine());
                                    if (Sum < 0)
                                    {
                                        throw new NegativeAmountException("The amount to replenish the account cannot be negative");
                                    }
                                    ClientNewAccount.Put(Sum);
                                    Console.ReadKey();
                                    choicepay = "0";
                                }
                                catch (FormatException)
                                {
                                    ConsoleColor color = Console.ForegroundColor;
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("Ви ввели невiрно!");
                                    Console.ForegroundColor = color;
                                    Console.WriteLine("Натиснiть 1 , щоб повтории спробу\nНатиснiть будь-яку клавiшу для продовження");
                                    choicepay = Console.ReadLine();
                                }
                            }
                            break;
                        }
                    default:
                        break;
                }
            }
            bool key = true;
            while (key)
            {
                Console.Clear();
                Console.WriteLine("\t\t\t\tДоброго дня, Вас вiтає клуб туризму 'Roman's Club'");
                Console.WriteLine("Виберiть, що саме Вас цiкавить в нашому клубi:");
                Console.WriteLine(String.Format("1.{0}\n" + "2.{1}\n" + "3.{2}\n8-Показати мої тури\n9-Ваш рахунок\n0-Вихiд", TourTypes[0], TourTypes[1], TourTypes[2]));
                int TourVariant = 4;
                while (TourVariant != 1 && TourVariant != 2 && TourVariant != 3 && TourVariant != 0 && TourVariant != 9 && TourVariant != 8)
                {
                    try
                    {
                        Console.WriteLine("Введiть сюди цифру :");
                        TourVariant = int.Parse(Console.ReadLine());
                        if (TourVariant != 1 && TourVariant != 2 && TourVariant != 3 && TourVariant != 0 && TourVariant != 9 && TourVariant != 8)
                        {
                            ConsoleColor color = Console.ForegroundColor;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.WriteLine("Оберiть цифру з запропонованих варiантiв !");
                            Console.ForegroundColor = color;

                        }
                    }
                    catch (FormatException)
                    {
                        ConsoleColor color = Console.ForegroundColor;
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("Ви ввели невiрно!");
                        Console.ForegroundColor = color;
                    }
                }
                PersonalTour LikeTour = new PersonalTour();
                if (TourVariant == 1 || TourVariant == 2 || TourVariant == 3)
                {
                    LikeTour.TourType = TourTypes[TourVariant - 1];
                    Console.WriteLine("Введiть будь ласка, як Ви хочете назвати свiй тур:");
                    LikeTour.Name = Console.ReadLine();//Creating a new tour with the name specified by the user
                }
                switch (TourVariant)
                {
                    case 1:
                        {
                            Console.Clear();
                            for (int j = 0; j < RomansClub.InterestingCountryTours.Count; j++)
                                Console.WriteLine(String.Format("{0}. Країна : {1}, Цiна за 1 день : {2} гривень", j + 1, RomansClub.InterestingCountryTours[j].Name, RomansClub.InterestingCountryTours[j].PricePerDay));
                            Console.WriteLine();
                            int choice = 0;
                            while (choice != 1 && choice != 2 && choice != 3 && choice != 4 && choice != 5)
                            {
                                try
                                {
                                    Console.WriteLine("Виберiть, який тур Ви хочете обрати:");
                                    choice = int.Parse(Console.ReadLine());
                                    if (choice != 1 && choice != 2 && choice != 3 && choice != 4 && choice != 5)
                                    {
                                        ConsoleColor color = Console.ForegroundColor;
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.WriteLine("Оберiть цифру з запропонованих варiантiв !");
                                        Console.ForegroundColor = color;

                                    }
                                }
                                catch (FormatException)
                                {
                                    ConsoleColor color = Console.ForegroundColor;
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("Ви ввели невiрно!");
                                    Console.ForegroundColor = color;
                                }
                            }
                            RomansClub.InterestingCountryTours[choice - 1].UpPriceInterestingCountry += InterestingCountry.DisplayMessage;
                            ChoiseMethod(RomansClub.InterestingCountryTours.ToArray(), LikeTour, ClientNewAccount, RomansClub.PartnerAgencies, choice,RomansClub);
                            break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            for (int j = 0; j < RomansClub.TematicToursTours.Count; j++)
                                Console.WriteLine(String.Format("{0}. Тур : {1}, Цiна за 1 день : {2} гривень", j + 1, RomansClub.TematicToursTours[j].Name, RomansClub.TematicToursTours[j].PricePerDay));
                            Console.WriteLine();
                            int choice = 0;
                            while (choice != 1 && choice != 2 && choice != 3 && choice != 4 && choice != 5)
                            {
                                try
                                {
                                    Console.WriteLine("Виберiть, який тур Ви хочете обрати:");
                                    choice = int.Parse(Console.ReadLine());
                                    if (choice != 1 && choice != 2 && choice != 3 && choice != 4 && choice != 5)
                                    {
                                        ConsoleColor color = Console.ForegroundColor;
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.WriteLine("Оберiть цифру з запропонованих варiантiв !");
                                        Console.ForegroundColor = color;

                                    }
                                }
                                catch (FormatException)
                                {
                                    ConsoleColor color = Console.ForegroundColor;
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("Ви ввели невiрно!");
                                    Console.ForegroundColor = color;
                                }
                            }
                            RomansClub.TematicToursTours[choice - 1].UpPriceTematicTours += TematicTours.DisplayMessage;
                            ChoiseMethod(RomansClub.TematicToursTours.ToArray(), LikeTour, ClientNewAccount, RomansClub.PartnerAgencies, choice,RomansClub);
                            break;
                        }
                    case 3:
                        {
                            Console.Clear();
                            for (int j = 0; j < RomansClub.SightseeingToursTours.Count; j++)
                                Console.WriteLine(String.Format("{0}. Тур : {1}, Цiна за 1 день : {2} гривень", j + 1, RomansClub.SightseeingToursTours[j].Name, RomansClub.SightseeingToursTours[j].PricePerDay));
                            Console.WriteLine();
                            int choice = 0;
                            while (choice != 1 && choice != 2 && choice != 3 && choice != 4 && choice != 5)
                            {
                                try
                                {
                                    Console.WriteLine("Виберiть, який тур Ви хочете обрати:");
                                    choice = int.Parse(Console.ReadLine());
                                    if (choice != 1 && choice != 2 && choice != 3 && choice != 4 && choice != 5)
                                    {
                                        ConsoleColor color = Console.ForegroundColor;
                                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                                        Console.WriteLine("Оберiть цифру з запропонованих варiантiв !");
                                        Console.ForegroundColor = color;

                                    }
                                }
                                catch (FormatException)
                                {
                                    ConsoleColor color = Console.ForegroundColor;
                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                    Console.WriteLine("Ви ввели невiрно!");
                                    Console.ForegroundColor = color;
                                }
                            }
                            RomansClub.SightseeingToursTours[choice - 1].UpPriceSightseeingTours += SightseeingTours.DisplayMessage;
                            ChoiseMethod(RomansClub.SightseeingToursTours.ToArray(), LikeTour, ClientNewAccount, RomansClub.PartnerAgencies, choice,RomansClub);
                            break;
                        }
                    case 0:
                        {
                            key = false;
                            break;
                        }
                    case 9:
                        {
                            Console.Clear();
                            Console.WriteLine(ClientNewAccount.ShowBill() + "\nНатиснiть 1 для поповнення рахунку\nНатиснiть будь-яку клавiшу для повернення в головне меню");
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    {
                                        string choicepay = "1";
                                        while (choicepay == "1")
                                        {
                                            try
                                            {
                                                Console.WriteLine("Введiть суму для поповнення: ");
                                                int Sum = int.Parse(Console.ReadLine());
                                                if (Sum < 0)
                                                {
                                                    throw new NegativeAmountException("The amount to replenish the account cannot be negative");
                                                }
                                                ClientNewAccount.Put(Sum);
                                                Console.ReadKey();
                                                choicepay = "0";
                                            }
                                            catch (FormatException)
                                            {
                                                ConsoleColor color = Console.ForegroundColor;
                                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                Console.WriteLine("Ви ввели невiрно!");
                                                Console.ForegroundColor = color;
                                                Console.WriteLine("Натиснiть 1 , щоб повтории спробу\nНатиснiть будь-яку клавiшу для продовження");
                                                choicepay = Console.ReadLine();
                                            }
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        break;
                                    }
                            }
                            break;
                        }
                    case 8:
                        {
                            Console.Clear();
                            Console.WriteLine("Вашi тури :");
                            if (ClientNewAccount.AllClientsTour.Count==0)
                            {
                                Console.WriteLine("Список турiв порожнiй(");
                                Console.ReadKey();
                            }
                            else
                            {
                                for (int i = 0; i < ClientNewAccount.AllClientsTour.Count; i++)
                                {
                                    Console.WriteLine($"{i + 1}.Тур: '{ClientNewAccount.AllClientsTour[i].Name}' ; Дата: {ClientNewAccount.AllClientsTour[i].DateOfTour.Day}.{ClientNewAccount.AllClientsTour[i].DateOfTour.Month}.{ClientNewAccount.AllClientsTour[i].DateOfTour.Year}; Cтатус оплати туру:{ClientNewAccount.AllClientsTour[i].Status}");
                                }
                                Console.WriteLine("Натиснiть 1 для оплати туру\nНатиснiть будь-яку клавiшу для продовження");
                                string choice = Console.ReadLine();
                                switch (choice)
                                {
                                    case "1":
                                        {
                                            Console.WriteLine("Введiть номер туру , який бажаєте оплатити :");
                                            int NumPayTour = int.Parse(Console.ReadLine());
                                            if (ClientNewAccount.AllClientsTour[NumPayTour - 1].Status == PayStatus.Paid)
                                            {
                                                Console.WriteLine("Цей тур вже оплачений !");
                                                Console.ReadKey();
                                                break;
                                            }
                                            try
                                            {
                                                ClientNewAccount.PayFor(ClientNewAccount.AllClientsTour[NumPayTour-1].Price);
                                                Console.ReadKey();
                                                ClientNewAccount.AllClientsTour[NumPayTour - 1].Status = PayStatus.Paid;

                                            }
                                            catch (LowMoneyException)
                                            {
                                                Console.WriteLine("\nНатиснiть 1 для поповнення рахунку\nНатиснiть будь-яку клавiшу для повернення в головне меню");
                                                switch (Console.ReadLine())
                                                {
                                                    case "1":
                                                        {
                                                            string choicepay = "1";
                                                            while (choicepay == "1")
                                                            {
                                                                try
                                                                {
                                                                    Console.WriteLine("Введiть суму для поповнення: ");
                                                                    int Sum = int.Parse(Console.ReadLine());
                                                                    if (Sum < 0)
                                                                    {
                                                                        throw new NegativeAmountException("The amount to replenish the account cannot be negative");
                                                                    }
                                                                    ClientNewAccount.Put(Sum);
                                                                    Console.ReadKey();
                                                                    choicepay = "0";
                                                                }
                                                                catch (FormatException)
                                                                {
                                                                    ConsoleColor color = Console.ForegroundColor;
                                                                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                                                                    Console.WriteLine("Ви ввели невiрно!");
                                                                    Console.ForegroundColor = color;
                                                                    Console.WriteLine("Натиснiть 1 , щоб повтории спробу\nНатиснiть будь-яку клавiшу для продовження");
                                                                    choicepay = Console.ReadLine();

                                                                }
                                                            }
                                                            break;
                                                        }
                                                    default:
                                                        {
                                                           break;
                                                        }
                                                }

                                            }
                                            break;
                                        }
                                    default:
                                        {
                                            break;
                                        }
                                }
                             
                            }
                          break;
                        }
                }
            }
        }
    }
}

