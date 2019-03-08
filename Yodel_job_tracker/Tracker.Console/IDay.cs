using System;
using System.Collections.Generic;
using System.Text;
using static Tracker.Console.Enum;

namespace Tracker.Console
{
    interface IDay
    {
        DateTime Date { get; set; }

        int Parcels { get;  set; }

        int Stops { get;  set; }

        int Collections { get; set; }

        int Returned { get; set; }

        int ManualParcels { get; set; }

        byte[] SummaryImage { get; set; }

        string Damages { get; set; }

        string Bonus { get; set; }

        double Miles { get; set; }

        string Note { get; set; }
    }
}
