using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale
{
    class helper
    {
        public static bool RunAgain()
        {
            string answer = Console.ReadLine().ToLower().Trim();
            if (answer == "y")
            {
                return true;
            }
            else if (answer == "n")
            {
                return false;
            }
            else
            {
                Console.WriteLine("I didn't understand that. Let's try again.");
                return RunAgain();
            }
        }
        public static string GetStringInput(string prompt)
        {
            Console.WriteLine(prompt);

            string input = Console.ReadLine().ToLower().Trim();

            //Explain the thing below later
            if (double.TryParse(input, out double CashPaid) && CashPaid.ToString().Length > CashPaid.ToString("0.00").Length)
            {
                Console.WriteLine("You entered a bad payment amount. Please ensure that the payment is only to two decimal places at most.");
                Console.WriteLine("Let's try again.");
                return (GetStringInput(prompt));
            }

            return input;
        }
        public static int GetIntInput(string prompt)
        {
            Console.WriteLine(prompt);

            string input = Console.ReadLine().ToLower().Trim();

            //Explain the thing below later
            if (!int.TryParse(input, out int outputNum))
            {
                Console.WriteLine("You did not enter an integer. Let's try again.");
                return (GetIntInput(prompt));
            }

            return outputNum;
        }
    }
}
