using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tracker.Console.Services
{
    public class SetCurrentDirectory
    {
        public static void Run()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            Directory.SetCurrentDirectory($@"{currentDirectory}/../../..");
            Directory.SetCurrentDirectory($@"Data");
        }
    }
}
