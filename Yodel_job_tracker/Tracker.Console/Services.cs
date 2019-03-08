namespace Tracker.Console
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public static class Services
    {
        public static void Commands()
        {
            while (true)
            {
                Console.WriteLine("Enter command!");
                Console.Write("=> ");
                var command = Console.ReadLine();

                switch (command.ToLower())
                {
                    case "add day":
                        DayDetails();
                        break;

                    case "find day":
                        FindDay();
                        break;

                    case "close":
                        Environment.Exit(0);
                        break;

                    default:
                        throw new ArgumentException("Command not found!");
                }
            }

        }

        private static void FindDay()
        {
            var date = ConvertDate();
            Console.WriteLine(day.Miles);
        }

        private static Day day = null;

        //Add day details
        private static void DayDetails()
        {
            Console.Clear();
            Console.Write($"date : ");
            var date = ConvertDate();
            Console.WriteLine();

            Console.Write($"parcels : ");
            var parcels = int.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.Write($"stops : ");
            var stops = int.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.Write($"collections : ");
            var collections = int.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.Write($"returned : ");
            var returned = int.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.Write($"manual parcels : ");
            var manualParcels = int.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.Write($"damages : ");
            var damages = Console.ReadLine();
            Console.WriteLine();

            Console.Write($"bonus : ");
            var bonus = Console.ReadLine();
            Console.WriteLine();

            Console.Write($"miles : ");
            var miles = double.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.Write($"note : ");
            var note = Console.ReadLine();
            Console.WriteLine();

            Console.Write($"day off : ");
            var dayOff = DayOff();
            Console.WriteLine();

            day = new Day()
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
        }

        private static bool DayOff()
        {
            var input = Console.ReadLine();
            if (input.ToLower() == "yes")
            {
                return true;
            }
            return false;
        }

        private static DateTime ConvertDate()
        {
            var input = Console.ReadLine();
            return Convert.ToDateTime(input);
        }
    }
}
