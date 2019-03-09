using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Tracker.Console.Models
{
    [XmlRoot("AllDays")]
    public class AllDays
    {
        public AllDays()
        {
            this.Days = new List<Day>();
        }

        [XmlElement("Day")]
        public List<Day> Days { get; set; }
    }
}
