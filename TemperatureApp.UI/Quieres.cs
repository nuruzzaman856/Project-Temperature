using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemperatureApp.Data;

namespace TemperatureApp.UI
{
    public class Quieres
    {
        internal static void SearchByDate()
        {
            Console.WriteLine("********************************************************************************************");

            using (var db = new TemperatureDataContext())
            {
                var OutSideTempData = db.TemperatureData.Where(l => l.Location == "Ute")
                                                        .Select(a => a);
                var InSideTempData = db.TemperatureData.Where(l => l.Location == "Inne")
                                                       .Select(a => a);
                var MaxDate = OutSideTempData.Max(m => m.DateAndTime);
                var MinDate = OutSideTempData.Min(m => m.DateAndTime);
                Console.WriteLine($"Write a date between {MinDate.ToShortDateString()} to {MaxDate.ToShortDateString()}");
                DateTime DT = DateTime.Parse(Console.ReadLine());
                var query1OutSide = OutSideTempData.Where(d => d.DateAndTime.Date == DT.Date)
                                             .GroupBy(d => d.DateAndTime.Date)
                                             .Select(t => new { Date = t.Key, Average = t.Average(h => h.Temperature) }).ToList();
                var query1InSide = InSideTempData.Where(d => d.DateAndTime.Date == DT.Date)
                                            .GroupBy(d => d.DateAndTime.Date)
                                            .Select(t => new { Date = t.Key, Average = t.Average(h => h.Temperature) }).ToList();
                if (query1OutSide.Count != 0 || query1InSide.Count != 0)
                {
                    Console.WriteLine("Average temperature for selected date OutSide:");
                    foreach (var temp in query1OutSide)
                    {

                        Console.WriteLine($"Date : {temp.Date.ToShortDateString()}, Average Temperature : {Math.Round(temp.Average)} Degree Celsius");

                    }
                    Console.WriteLine("********************************************************************************************");
                    Console.WriteLine("Average temperature for selected date Inside:");
                    foreach (var temp in query1InSide)
                    {
                        Console.WriteLine($"Date : {temp.Date.ToShortDateString()}, Average Temperature : {Math.Round(temp.Average)} Degree Celsius");

                    }
                }
                else if (query1OutSide.Count == 0)
                {
                    Console.WriteLine("No Temperature found on that date.");
                }
            }
            Console.WriteLine("********************************************************************************************");
        }

        internal static void FindMeteorologicalWinterDate()
        {
            Console.WriteLine("********************************************************************************************");
            Console.WriteLine("Date of meteorological Winter:");
            using (var db = new TemperatureDataContext())
            {
                var MeteorologicalWinterDates = db.TemperatureData.Where(l => l.Location == "Ute" && l.DateAndTime > DateTime.Parse("2016-08-01"))
                          .GroupBy(d => d.DateAndTime.Date)
                          .Select(d => new { Date = d.Key, AverageTemp = d.Average(t => t.Temperature) }).ToList()
                          .OrderBy(x => x.Date);
                int dayCount = 0;

                foreach (var dat in MeteorologicalWinterDates)
                {
                    if (dat.AverageTemp < 0)
                    {
                        dayCount++;

                        if (dayCount == 5)
                        {
                            Console.WriteLine($"Meteorological winter starts at {dat.Date.Date.ToShortDateString()}");
                            break;
                        }
                    }
                    else
                    {
                        dayCount = 0;

                    }
                }

                Console.WriteLine("********************************************************************************************");
            }

        }


        internal static void FindMeteorologicalAutumnDate()
        {
            Console.WriteLine("********************************************************************************************");
            Console.WriteLine("Date of meteorological Autumn:");
            using (var db = new TemperatureDataContext())
            {
                var MeteorologicalAutumnDates = db.TemperatureData.Where(l => l.Location == "Ute" && l.DateAndTime > DateTime.Parse("2016-06-11"))
                          .GroupBy(d => d.DateAndTime.Date)
                          .Select(d => new { Date = d.Key, AverageTemp = d.Average(t => t.Temperature) })
                          .OrderBy(x => x.Date).ToList();
                int dayCount = 0;

                foreach (var date in MeteorologicalAutumnDates)
                {
                    if (date.AverageTemp < 10)
                    {
                        dayCount++;

                        if (dayCount == 5)
                        {
                            Console.WriteLine($"Meteorological autumn starts at {date.Date.Date.ToShortDateString()}");
                            break;
                            
                        }
                       
                    }
                    else
                    {
                        dayCount = 0;

                    }

                }

                Console.WriteLine("********************************************************************************************");
            }
        }

        internal static void WarmestToColdestDay()
        {
            Console.WriteLine("********************************************************************************************");

            using (var db = new TemperatureDataContext())
            {
                var OutSideTempData = db.TemperatureData.Where(l => l.Location == "Ute")
                                                        .Select(a => a);
                var InSideTempData = db.TemperatureData.Where(l => l.Location == "Inne")
                                                       .Select(a => a);
                var query2OutSide = OutSideTempData
                                              .GroupBy(d => d.DateAndTime.Date)
                                              .Select(t => new { Date = t.Key, Average = t.Average(h => h.Temperature) })
                                              .OrderByDescending(t => t.Average).ToList();

                Console.WriteLine($"Sorting of warmest to coldest day according to average temperature per day OutSide:");
                foreach (var temp in query2OutSide.Take(10))
                {
                    Console.WriteLine($" Date : {temp.Date.ToShortDateString()},  Average Temperature : {Math.Round(temp.Average)} Degree Celsius");
                }

                Console.WriteLine("********************************************************************************************");
                var query2InSide = InSideTempData
                                         .GroupBy(d => d.DateAndTime.Date)
                                         .Select(t => new { Date = t.Key, Average = t.Average(h => h.Temperature) })
                                         .OrderByDescending(t => t.Average).ToList();

                Console.WriteLine($"Sorting of warmest to coldest day according to average temperature per day InSide:");
                foreach (var temp in query2InSide.Take(10))
                {
                    Console.WriteLine($" Date : {temp.Date.ToShortDateString()},  Average Temperature : {Math.Round(temp.Average)} Degree Celsius");
                }

            }
            Console.WriteLine("********************************************************************************************");

        }
        internal static void DriestToWettest()
        {
            Console.WriteLine("********************************************************************************************");

            using (var db = new TemperatureDataContext())
            {
                var OutSideTempData = db.TemperatureData.Where(l => l.Location == "Ute")
                                                        .Select(a => a);
                var InSideTempData = db.TemperatureData.Where(l => l.Location == "Inne")
                                                        .Select(a => a);


                var query3OutSide = OutSideTempData
                                            .GroupBy(d => d.DateAndTime.Date)
                                            .Select(t => new { Date = t.Key, Average = t.Average(h => h.Humidity) })
                                            .OrderBy(t => t.Average).ToList();
                Console.WriteLine($"Sorting the driest to the wettest day according to average humidity per day OutSide:");
                foreach (var temp in query3OutSide.Take(10))
                {
                    Console.WriteLine($" Date : {temp.Date.ToShortDateString()},  Average Humidity : {Math.Round(temp.Average)}  %");
                }
                Console.WriteLine("********************************************************************************************");
                var query3InSide = InSideTempData
                                            .GroupBy(d => d.DateAndTime.Date)
                                            .Select(t => new { Date = t.Key, Average = t.Average(h => h.Humidity) })
                                            .OrderBy(t => t.Average).ToList();
                Console.WriteLine($"Sorting the driest to the wettest day according to average humidity per day InSide:");
                foreach (var temp in query3InSide.Take(10))
                {
                    Console.WriteLine($" Date : {temp.Date.ToShortDateString()},  Average Humidity : {Math.Round(temp.Average)} %");
                }
                Console.WriteLine("********************************************************************************************");

            }
        }
        internal static void RiskOfMold()
        {
            Console.WriteLine("********************************************************************************************");

            using (var db = new TemperatureDataContext())
            {
                var OutSideTempData = db.TemperatureData.Where(l => l.Location == "Ute")
                                                        .Select(a => a);
                var InSideTempData = db.TemperatureData.Where(l => l.Location == "Inne")
                                                       .Select(a => a);

                var query4OutSide1 = OutSideTempData
                                           .GroupBy(d => d.DateAndTime.Date)
                                           .Select(t => new { Date = t.Key, MaxTemperature = t.Max(h => h.Temperature), MaxHumidity = t.Max(h => h.Humidity) })
                                           .ToList();

                var query4OutSide = query4OutSide1
                                     .Select(r => new { r.Date, ROfM = CalculateRiskOfMold(r.MaxHumidity, r.MaxTemperature) })
                                     .OrderByDescending(m => m.ROfM)
                                     .ToList();
                Console.WriteLine("Sorting of the least to greatest risk of mold Outside::");
                foreach (var item in query4OutSide.Take(10))
                {
                    if (item.ROfM < 0)
                    {
                        Console.WriteLine($" Date : {item.Date.ToShortDateString()},  RiskOfMold  : 0 %");
                    }
                    else if (item.ROfM > 100)
                    {
                        Console.WriteLine($" Date : {item.Date.ToShortDateString()},  RiskOfMold  : 100 %");
                    }
                    else if (item.ROfM >= 1 && item.ROfM <= 99)
                    {
                        Console.WriteLine($" Date : {item.Date.ToShortDateString()},  RiskOfMold  : {Math.Round(item.ROfM)} %");
                    }

                }
                Console.WriteLine("********************************************************************************************");
                var query4InSide1 = InSideTempData
                                         .GroupBy(d => d.DateAndTime.Date)
                                         .Select(t => new { Date = t.Key, MaxTemperature = t.Max(h => h.Temperature), MaxHumidity = t.Max(h => h.Humidity) })
                                         .ToList();

                var query4InSide = query4InSide1
                                     .Select(r => new { r.Date, ROfM = CalculateRiskOfMold(r.MaxHumidity, r.MaxTemperature) })
                                     .OrderByDescending(m => m.ROfM)
                                     .ToList();
                Console.WriteLine("Sorting of the least to greatest risk of mold InSide::");
                foreach (var item in query4InSide.Take(10))
                {
                    if (item.ROfM < 0)
                    {
                        Console.WriteLine($" Date : {item.Date.ToShortDateString()},  RiskOfMold  : 0 %");
                    }
                    else if (item.ROfM > 100)
                    {
                        Console.WriteLine($" Date : {item.Date.ToShortDateString()},  RiskOfMold  : 100 %");
                    }
                    else if (item.ROfM >= 1 && item.ROfM <= 99)
                    {
                        Console.WriteLine($" Date : {item.Date.ToShortDateString()},  RiskOfMold  : {Math.Round(item.ROfM)} %");
                    }
                }
                Console.WriteLine("********************************************************************************************");
            }
        }

        private static double CalculateRiskOfMold(double h, double t)
        {


            return ((h - 78) * (t / 15)) / 0.22;


        }

    }

}
