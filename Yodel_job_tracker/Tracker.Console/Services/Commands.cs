namespace Tracker.Console.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using Models;
    using static Tracker.Console.Models.Enum;


    public static class Commands
    {
        private static Day Day = null;

        //All commands
        public static void AllCommands()
        {
            Console.WriteLine();
            Console.WriteLine("     YODEL job tracker v1.1");
            Console.WriteLine();
            Console.WriteLine("     powered by YAVAKA SOLUTIONS");
            Console.WriteLine();
            while (true)
            {
                Console.Write("=> ");
                var command = Console.ReadLine();

                switch (command.ToLower())
                {
                    case "add day":
                        DayDetails();
                        break;

                    case "calendar":
                        WholeWeekByDates();
                        break;

                        //TODO Money for the whole week by given date

                    case "help":
                        ListCommands();
                        break;

                    case "close":
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Command not found!");
                        break;
                }
            }
        }

        //Help command
        private static void ListCommands()
        {
            Console.WriteLine("1. Add day");
            Console.WriteLine("2. Calendar");
            Console.WriteLine("3. Close");
        }

        //Calendar command
        private static void WholeWeekByDates()
        {
            Console.Clear();
            var date = ConvertDate();

            var weekDay = date.DayOfWeek;
            int nextDay = 0;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, 0);

            //Print all dates for the week
            for (int day = 0; day <= 6; day++)
            {
                //Get first date of the week (Sunday)
                DateTime startOfWeek = date.AddDays(
                 (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek -
                 (int)date.DayOfWeek);

                duration = new TimeSpan(nextDay, 0, 0, 0, 0);
                startOfWeek = startOfWeek.Add(duration);
                nextDay++;

                Console.WriteLine();
                Console.WriteLine(startOfWeek.ToString("dddd, dd MMMM yyyy"));
                Console.WriteLine();
                GetDaysDetails(startOfWeek);
            }
        }

        //Add day details command
        private static void DayDetails()
        {
            Console.Clear();
            Console.WriteLine("     Add Day");

            var dayOff = DayOff();

            //Check is day off
            if (dayOff.ToString() == "no")
            {
                var date = ConvertDate();
                if (IsDateExist(date))
                {
                    DayDetails();
                }

                Console.Write($"parcels : ");
                var parcels = int.Parse(Console.ReadLine());

                Console.Write($"stops : ");
                var stops = int.Parse(Console.ReadLine());

                Console.Write($"collections : ");
                var collections = int.Parse(Console.ReadLine());

                Console.Write($"returned : ");
                var returned = int.Parse(Console.ReadLine());

                Console.Write($"manual parcels : ");
                var manualParcels = int.Parse(Console.ReadLine());

                Console.Write($"damages : ");
                var damages = Console.ReadLine();

                Console.Write($"bonus : ");
                var bonus = Console.ReadLine();

                Console.Write($"miles : ");
                var miles = double.Parse(Console.ReadLine());

                Console.Write($"note : ");
                var note = Console.ReadLine();

                AddDay(date, parcels,stops,collections,returned,manualParcels,damages,bonus,miles,note,dayOff);
            }
            else
            {
                var date = ConvertDate();
                //TODO is date exist in XML file
                //TODO Test is working proper
                if (IsDateExist(date))
                {
                    DayDetails();
                }
                Console.Write($"note : ");
                var note = Console.ReadLine();

                //Default values
                var parcels = 0;
                var stops = 0;
                var collections = 0;
                var returned = 0;
                var manualParcels = 0;
                var damages = "none";
                var bonus = "none";
                var miles = 0;

                AddDay(date, parcels, stops, collections, returned, manualParcels, damages, bonus, miles, note, dayOff);
            }
        }

        private static bool IsDateExist(DateTime date)
        {
            var allDays = GetDaysDetails(date);
            foreach (var day in allDays.Days)
            {
                if (day.Date == date)
                {
                    return true;
                }
            }
            return false;
        }


        //Helpers
        private static AllDays GetDaysDetails(DateTime date)
        {
            var allDays = XmlReaderWriter.Read();

            foreach (var day in allDays.Days)
            {
                //TODO If dayOff is y do not list whole data
                if (date == day.Date)
                {
                    Console.WriteLine($"Parcels : {day.Parcels}");
                    Console.WriteLine($"Stops : {day.Stops}");
                    Console.WriteLine($"Collections : {day.Collections}");
                    Console.WriteLine($"Returned : {day.Returned}");
                    Console.WriteLine($"Manual parcels : {day.ManualParcels}");
                    Console.WriteLine($"Miles : {day.Miles}");
                    Console.WriteLine($"Damages : {day.Damages}");
                    Console.WriteLine($"Bonus : {day.Bonus}");
                    Console.WriteLine($"Note : {day.Note}");
                } 
            }
            return allDays;
        }
        private static void AddDay(DateTime date, int parcels, int stops, int collections, int returned, int manualParcels,
            string damages, string bonus, double miles, string note, DayOff dayOff)
        {
            Day = new Day()
            {
                Date = date,
                Parcels = parcels,
                Stops = stops,
                Collections = collections,
                Returned = returned,
                ManualParcels = manualParcels,
                Damages = damages,
                Bonus = bonus,
                Miles = miles,
                Note = note,
                DayOff = dayOff
            };

            //Add day in XML file
            AddDayInXml();

        }
        private static void AddDayInXml()
        {
            XmlReaderWriter.Write(Day);
            Console.WriteLine("Day has been added.");
        }
        private static DayOff DayOff()
        {
            Console.Write($"day off y/n : ");
            var input = Console.ReadLine();
            switch (input.ToLower())
            {
                case "y":
                    return Models.Enum.DayOff.yes;
                case "n":
                    return Models.Enum.DayOff.no;
                default:
                    DayDetails();
                    break;
            }
            return Models.Enum.DayOff.no;
        }
        private static DateTime ConvertDate()
        {
            Console.Write($"date dd/mm/yyyy : ");
            var input = Console.ReadLine();

            DateTime date;
            bool isDateValid = DateTime.TryParseExact(
                input,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                style: DateTimeStyles.None,
                result: out date);

            if (!isDateValid)
            {
                AllCommands();
            }

            return date;
        }
        
    }
}
