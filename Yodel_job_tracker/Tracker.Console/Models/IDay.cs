using System;

namespace Tracker.Console.Models
{
    interface IDay
    {
        DateTime Date { get; set; }

        int Parcels { get;  set; }

        int Stops { get;  set; }

        int Collections { get; set; }

        int Returned { get; set; }

        int ManualParcels { get; set; }

        string Damages { get; set; }

        string Bonus { get; set; }

        double Miles { get; set; }

        string Note { get; set; }
    }
}
