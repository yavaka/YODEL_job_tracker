namespace Tracker.Console
{
    using System;
    using System.Xml.Serialization;
    using Models;
    using static Tracker.Console.Models.Enum;

    public class Day : IDay
    {
        [XmlAttribute("Date")]
        public DateTime Date{ get; set; }

        [XmlAttribute("Parcels")]
        public int Parcels{ get; set; }

        [XmlAttribute("Stops")]
        public int Stops{ get; set; }

        [XmlAttribute("Collections")]
        public int Collections{ get; set; }

        [XmlAttribute("Returned")]
        public int Returned{ get; set; }

        [XmlAttribute("ManualParcels")]
        public int ManualParcels{ get; set; }

        [XmlAttribute("Damages")]
        public string Damages{ get; set; }

        [XmlAttribute("Bonus")]
        public string Bonus{ get; set; }

        [XmlAttribute("Miles")]
        public double Miles{ get; set; }

        [XmlAttribute("Note")]
        public string Note{ get; set; }

        [XmlAttribute("DayOff")]
        public DayOff DayOff { get; set; }

   }
}
