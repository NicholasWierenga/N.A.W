using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale
{
    class Helper
    {
        public static bool RunAgain(string prompt)
        {
            Console.WriteLine(prompt);

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
                return RunAgain(prompt);
            }
        }
        public static string GetStringInput(string prompt)
        {
            Console.WriteLine(prompt);

            string input = Console.ReadLine().ToLower().Trim();

            // We use this to check if a user put in a double with more than 2 decimal places.
            if (double.TryParse(input, out double CashPaid) && CashPaid.ToString().Length > CashPaid.ToString("0.00").Length)
            { // Maybe get rid of this and make it its own method so we aren't parsing twice?
                Console.WriteLine("You entered a bad payment amount. Please ensure that the payment is only to two decimal places at most.");
                Console.WriteLine("Let's try again.");
                return GetStringInput(prompt);
            }
            else if (input.Length == 0)
            {
                Console.WriteLine("You must enter something.");
            }

            return input;
        }
        public static int GetIntInput(string prompt)
        {
            Console.WriteLine(prompt);

            string input = Console.ReadLine().ToLower().Trim();

            if (double.TryParse(input, out double numberOrdered) && numberOrdered % 1 == 0)
            {
                if (numberOrdered <= 0)
                {
                    Console.WriteLine("That is not a correct amount ordered.");
                    return GetIntInput(prompt);
                }
                else
                {
                    return (int)numberOrdered; // We parse as double and cast as int in the end to check for integers like 1.0.
                }
            }
            else
            {
                Console.WriteLine("That is not an integer. Let's try again.");
                return GetIntInput(prompt);
            }
        }
    }
}
