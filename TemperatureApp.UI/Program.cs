using System;

namespace TemperatureApp.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            ReadAndSeedData.SeedDataToDatabase();
            Menu.QuieryMenu();
        }
    }
}
