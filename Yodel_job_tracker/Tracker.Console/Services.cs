namespace Tracker.Console
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Text;
    using static Tracker.Console.Enum;

    public static class Services
    {
        private const int LEFT_CURSOR_POSITION = 10;
        private const int TOP_CURSOR_POSITION = 5;

        private static Day Day = null;

        public static void Commands()
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

        //TODO Get and list whole info for every day
        //Calendar command
        private static void WholeWeekByDates()
        {
            Console.Clear();
            var date = ConvertDate();

            var weekDay = date.DayOfWeek;

            int nextDay = 0;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, 0);
            for (int day = 0; day <= 6; day++)
            {
                DateTime startOfWeek = date.AddDays(
                 (int)CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek -
                 (int)date.DayOfWeek);

                duration = new TimeSpan(nextDay, 0, 0, 0, 0);
                startOfWeek = startOfWeek.Add(duration);
                Console.WriteLine(startOfWeek.ToString("dddd, dd MMMM yyyy"));
                nextDay++;
            }
        }

        //Add day details command
        private static void DayDetails()
        {
            Console.Clear();
            Console.WriteLine("     Add Day");

            var date = ConvertDate();

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

            var dayOff = DayOff();

            Day = new Day()
            {
                Date = date,
                Parcels = parcels,
                Stops = stops,
                Collections = collections,
                Returned = returned,
                ManualParcels = manualParcels,
                SummaryImage = new byte[0],
                Damages = damages,
                Bonus = bonus,
                Miles = miles,
                Note = note,
                DayOff = dayOff
            };
            Console.WriteLine("Day has been added.");
        }

        private static DayOff DayOff()
        {
            Console.Write($"day off y/n : ");
            var input = Console.ReadLine();
            switch (input.ToLower())
            {
                case "y":
                    return Enum.DayOff.yes;
                case "n":
                    return Enum.DayOff.no;
                default:
                    DayDetails();
                    break;
            }
            return Enum.DayOff.no;
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
                Commands();
            }
            return date;
        }
    }
}
