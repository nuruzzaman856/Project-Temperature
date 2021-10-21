using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureApp.Data
{
   
        public record TemperatureData
        {

            public int Id { get; set; }
            public DateTime DateAndTime { get; set; }
            public string Location { get; set; }
            public double Temperature { get; set; }
            public double Humidity { get; set; }

            public static TemperatureData ParseFromCSV(string line)
            {
                var columns = line.Split(',');

                return new TemperatureData
                {
                    DateAndTime = DateTime.Parse(columns[0]),
                    Location = columns[1],
                    Temperature = double.Parse(columns[2]),
                    Humidity = double.Parse(columns[3])

                };
            }
        }

    
}
