namespace Tracker.Console.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Tracker.Console.Models;

    public class XmlReaderWriter
    {
        private const string PATH = @"../../../Data/days_details.xml";
        private static AllDays AllDays = new AllDays();


        public static void Write(Day day)
        {

            //TODO throw exception, find solution
            if (File.Exists(PATH))
            {
                //Get all existing days from XML file
                Read();

                //Add new day in existing XML file
                AddDay(day);
            }
            else if(!File.Exists(PATH))
            {
                File.Create(PATH);
                TextWriter tw = new StreamWriter(PATH);
                AddDay(day);
            }
            ////If XML file exist
            //try
            //{
                

            //}//If XML file not exist
            //catch (System.IO.FileNotFoundException exc)
            //{
            //    Console.WriteLine(exc.Message);
            //    //Create XML file and add new day
            //    AddDay(day);
            //}
            //catch (IOException exc)
            //{
            //    Console.WriteLine(exc.Message);
            //    //Create XML file and add new day
            //    AddDay(day);
            //}
            //catch (Exception exc)
            //{
            //    Console.WriteLine(exc.Message);
            //    //Create XML file and add new day
            //    AddDay(day);
            //}
            
            
        }

        public static AllDays Read()
        {
            using (FileStream stream = File.OpenRead(PATH))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AllDays));
                AllDays = (AllDays)serializer.Deserialize(stream);
            }
            return AllDays;
        }

        //Helpers
        private static void AddDay(Day day)
        {
            AllDays.Days.Add(day);
            using (Stream stream = File.OpenWrite(PATH))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(AllDays));
                serializer.Serialize(stream, AllDays);
            }
        }
    }
}
