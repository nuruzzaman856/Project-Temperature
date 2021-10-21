using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemperatureApp.Data;

namespace TemperatureApp.UI
{

    public class ReadAndSeedData
    {
        public const string filePath = @"C:\Users\nuruz\Desktop\ProjectTemperature\Temperature App\TemperatureApp.Data\TemperatureApp.Data\TemperatureData.csv";

        public static void SeedDataToDatabase()
        {
            if (File.Exists(filePath))
            {
                var TemperatureDataList = ProcessFile(filePath);
                using (var db = new TemperatureDataContext())
                {
                    db.Database.EnsureCreated();
                    if (!db.TemperatureData.Any())
                    {
                        foreach (var temperature in TemperatureDataList)
                        {
                            db.TemperatureData.Add(temperature);
                        }
                    }
                    db.SaveChanges();
                }


            }

        }

        private static List<TemperatureData> ProcessFile(string path)
        {
            return
            File.ReadAllLines(path)
                 .Where(l => l.Length > 1)
                 .Select(TemperatureData.ParseFromCSV)
                 .ToList();
        }

    }
}
