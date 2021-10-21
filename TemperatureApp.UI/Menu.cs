using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemperatureApp.UI
{
    public class Menu
    {
        public static bool QuieryOption = true;
        public static void QuieryMenu()
        {
            Console.WriteLine("'''''WelCome'''''");
            PrintMenu();

        }

        private static void PrintMenu()
        {
            Console.WriteLine("'''''1.Check Average Temperature with Date'''''");
            Console.WriteLine("'''''2.Sorting of warmest to coldest day according to average temperature per day:'''''");
            Console.WriteLine("'''''3.Sorting the driest to the wettest day according to average humidity per day:'''''");
            Console.WriteLine("'''''4.Sorting of the least to greatest risk of mold'''''");
            Console.WriteLine("'''''5.Date of meteorological Autumn:'''''");
            Console.WriteLine("'''''6.Date of meteorological Winter:'''''");
            Console.WriteLine("'''''7.Exit'''''");
            try
            {
                var option = int.Parse(Console.ReadLine());

                while (QuieryOption == true)
                {
                    switch (option)
                    {
                        case 1:
                            Quieres.SearchByDate();
                            PrintMenu();
                            break;
                        case 2:
                            Quieres.WarmestToColdestDay();
                            PrintMenu();
                            break;
                        case 3:
                            Quieres.DriestToWettest();
                            PrintMenu();
                            break;
                        case 4:
                            Quieres.RiskOfMold();
                            PrintMenu();
                            break;
                        case 5:
                            Quieres.FindMeteorologicalAutumnDate();
                            PrintMenu();
                            break;
                        case 6:
                            Quieres.FindMeteorologicalWinterDate();
                            PrintMenu();
                            break;
                        case 7:
                            Console.WriteLine("Bye Bye");
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Bye Bye");
                            QuieryOption = false;
                            break;
                    }
                }

            }
            catch (Exception e)
            {

                Console.WriteLine($"Invalid Input. OR  {e.Message}"); ;
            }
        }

    }
}
