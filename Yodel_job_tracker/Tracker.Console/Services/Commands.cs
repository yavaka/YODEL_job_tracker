namespace Tracker.Console.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using Models;
    using static Tracker.Console.Models.Enum;

    //TODO Check where file is not closed type using() body
    public static class Commands
    {
        private static Day Day = null;
        private const string PATH = @"../../../Data/days_details.xml";

        //All commands
        public static void AllCommands()
        {
            //Check is XML file exists
            IsXmlFileExist();

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

                    //TODO Remove day by date
                    //case "remove day":
                    //    RemoveDayByDate();
                    //    break;

                    //TODO Remove all days
                    case "remove all days":
                        RemoveAllDays();
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

        #region Commands

        ////TODO test is day removed
        ////Remove day command
        //private static void RemoveDayByDate()
        //{
        //    //Send inputed date from the user
        //    //Remove day
        //    XmlReaderWriter.RemoveDayByDate(ConvertDate());

        //    Console.WriteLine("Day has been removed!");
        //    Console.WriteLine();
        //}

        //Remove all days command

        private static void RemoveAllDays()
        {
            XmlReaderWriter.DeleteXmlFile();
            Console.WriteLine("XML file has been deleted!");

            //Check is XML file exists if not create one
            IsXmlFileExist();
        }

        //Help command
        private static void ListCommands()
        {
            Console.WriteLine("1. Add day");
            Console.WriteLine("2. Calendar");
            Console.WriteLine("3. Remove all days");
            Console.WriteLine("4. Close");
        }

        //Calendar command
        private static void WholeWeekByDates()
        {
            var date = Date();

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
            Console.WriteLine("     Add Day");

            var dayOff = DayOff();

            //Check is day off
            if (dayOff.ToString() == "no")
            {
                var date = Date();
                if (IsDateExist(date))
                {
                    Console.WriteLine("Date already exists!");
                    AllCommands();
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

                AddDay(date, parcels, stops, collections, returned, manualParcels, damages, bonus, miles, note, dayOff);
            }
            else
            {
                var date = Date();
                if (IsDateExist(date))
                {
                    AllCommands();
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

        #endregion

        #region Helpers

        private static void IsXmlFileExist()
        {
            if (!File.Exists(PATH))
            {
                Day = null;
                //Populate with data here if necessary, then save to make sure it exists
                XmlReaderWriter.AddDay(Day);
            }
        }
        private static bool IsDateExist(DateTime date)
        {
            var allDays = XmlReaderWriter.Read();
            var dayDetails = allDays.Days.FirstOrDefault(d => d.Date == date);
            if (dayDetails == null)
            {
                return false;
            }
            else if (dayDetails != null)
            {
                Console.WriteLine("Date already exists!");
                return true;
            }
            return false;
        }

        private static AllDays GetDaysDetails(DateTime date)
        {
            var allDays = XmlReaderWriter.Read();

            foreach (var day in allDays.Days)
            {
                //TODO If dayOff is y do not list whole data
                if (date == day.Date && day.DayOff.ToString() == "no")
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
                else if (day == null) { }
                else if (date == day.Date && day.DayOff.ToString() == "yes")
                {
                    Console.WriteLine($"Note : {day.Note}");
                }
            }
            return allDays;
        }
        private static void AddDay(DateTime date, int parcels, int stops, int collections, int returned, int manualParcels,
            string damages, string bonus, double miles, string note, DayOff dayOff)
        {
            var day = new Day()
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
            AddDayInXml(day);
        }
        private static void AddDayInXml(Day day)
        {
            XmlReaderWriter.Write(day);
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
                    Console.WriteLine("Invalid input only 'y' or 'n'!");
                    DayDetails();
                    break;
            }
            return Models.Enum.DayOff.no;
        }
        private static DateTime Date()
        {
            Console.Write($"date dd/mm/yyyy : ");

            var input = Console.ReadLine();
            return ConvertDate(input);
        }
        private static DateTime ConvertDate(string input)
        {
            DateTime date;
            bool isDateValid = DateTime.TryParseExact(
                input,
                "dd/MM/yyyy",
                CultureInfo.InvariantCulture,
                style: DateTimeStyles.None,
                result: out date);

            if (!isDateValid)
            {
                Console.WriteLine("Invalid input!");
                AllCommands();
            }

            return date;
        }

        #endregion
    }
}
