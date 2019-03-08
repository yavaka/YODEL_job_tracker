namespace Tracker.Console
{
    using System;
    using static Tracker.Console.Enum;

    //TODO Annotations
    public class Day : IDay
    {
        public DateTime Date{ get; set; }

        public int Parcels{ get; set; }

        public int Stops{ get; set; }

        public int Collections{ get; set; }

        public int Returned{ get; set; }

        public int ManualParcels{ get; set; }

        public byte[] SummaryImage { get; set; }

        public string Damages{ get; set; }

        public string Bonus{ get; set; }

        public double Miles{ get; set; }

        public string Note{ get; set; }

        public DayOff DayOff { get; set; }

   }
}
