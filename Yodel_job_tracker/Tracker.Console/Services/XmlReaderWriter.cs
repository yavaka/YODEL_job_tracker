namespace Tracker.Console.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;
    using Tracker.Console.Models;

    public class XmlReaderWriter
    {
        private const string PATH = @"../../../Data/days_details.xml";
        private static AllDays AllDays = new AllDays();
        private static Day Day = new Day();

        //Write XML file
        public static void Write(Day day)
        {
            //Get all existing days from XML file
            Read();

            //Add new day in existing XML file
            AddDay(day);
        }

        //Readd XML file
        public static AllDays Read()
        {
            //Open XML file reader
            using (FileStream stream = File.OpenRead(PATH))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AllDays));

                AllDays = (AllDays)serializer.Deserialize(stream);
            }
            return AllDays;
        }

        //Delete XML file    
        public static void DeleteXmlFile() => File.Delete(PATH);

        ////Remove day by date
        //public static void RemoveDayByDate(DateTime date)
        //{
        //    //Get day details by given date
        //    var dayDetails = AllDays.Days
        //        .FirstOrDefault(d => d.Date == date);

        //    //Remove day details
        //    AllDays.Days.Remove(dayDetails);

        //    //Open XML file writer 
        //    //Write all days without already removed
        //    using (Stream stream = File.OpenWrite(PATH))
        //    {
        //        XmlSerializer serializer = new XmlSerializer(typeof(AllDays));
        //        serializer.Serialize(stream, AllDays);
        //    }
        //}

        //Helpers
        public static void AddDay(Day day)
        {
            AllDays.Days.Add(day);
            //Open XML file writer
            using (Stream stream = File.OpenWrite(PATH))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AllDays));
                serializer.Serialize(stream, AllDays);
            }
        }
    }
}
