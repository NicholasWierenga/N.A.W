using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale
{
    public enum PayOption
    {
        cash,
        check,
        card,
    }

    class Payment
    {
        public double TotalOwed { get; set; }
        public PayOption Method { get; set; }
        public double AmountGiven { get; set; } // Not sure if this is needed. maybe delete


        public Payment(double TotalOwed)
        {
            this.TotalOwed = TotalOwed;
        }
        public void Pay()
        {
            PayOption option = GetPaymentMethod();

            switch (option)
            {
                case PayOption.cash:
                    CashPaid();
                    break;
                case PayOption.check:
                    CheckPaid();
                    break;
                case PayOption.card:
                    CardPaid();
                    break;
            }
        }

        public PayOption GetPaymentMethod()
        {
            string input = GetStringInput("What method of payment do you want to use? We accept cash, check, or card?");

            if (Enum.TryParse(input, out PayOption payOption))
            {
                Console.WriteLine("You've selected " + payOption.ToString() + " as your payment option." +
                    " Your total owed is: $" + TotalOwed.ToString("0.00") + ".");
                return payOption;
            }
            else
            {
                Console.WriteLine("That was not a valid payment option. Let's try again.");
                return GetPaymentMethod();
            }
        }

        public void CashPaid() // If we need to update TotalOwed we could change return type to double and return TotalOwed - CashPaid.
        {
            string input = GetStringInput("How much cash are you using to pay for this order?");

            if (double.TryParse(input, out double cashPaid) && cashPaid >= TotalOwed)
            {
                if (cashPaid > TotalOwed) // We only want to tell a customer their change if they're actually getting some.
                {
                    Console.Write("Your change is: $" + (cashPaid - TotalOwed).ToString("0.00") + ".");
                }

                Console.WriteLine("Thank you for your business. Enjoy your meal.");
            }
            else
            {
                Console.WriteLine("You've given less than the amount you owe. Let's try again.");
                CashPaid();
            }
        }

        public void CheckPaid()
        {
            string input = GetStringInput("How much money are you using to pay for this by check?");

            if (double.TryParse(input, out double amountPaid))
            {
                if (amountPaid < TotalOwed)
                {
                    Console.WriteLine("Sorry, but this does not cover the bill. Please write one for the exact total owed.");
                    CheckPaid();
                }
                else if (amountPaid > TotalOwed)
                {
                    Console.WriteLine("Sorry, but we don't give change for payments involving checks. Please write one for the exact total owed.");
                    CheckPaid();
                }
                else
                {
                    Console.WriteLine("Thanks for your business. Come again!");
                }
            }
            else
            {
                Console.WriteLine("This is not a valid check. Let's try again.");
                CheckPaid();
            }
        }

        public void CardPaid()
        {

            string cardNumber = GetStringInput("Please enter your card number.");
            if (!long.TryParse(cardNumber, out long outputNum) || cardNumber.ToString().Length != 16)
            {
                Console.WriteLine("You entered non numbers or an incorrect amount of digits. Let's try again");
                CardPaid();
            }

            int ccv = GetIntInput("Please enter your card's CCV."); // remember to change these back to ints later
            if (ccv.ToString().Length < 3 || ccv.ToString().Length > 4)
            {
                Console.WriteLine("You entered non numbers or an incorrect amount of digits. Let's try again");
                CardPaid();
            }

            int expiryMonth = GetIntInput("Please enter the month your card expires as an integer.");
            if (expiryMonth < 1 || expiryMonth > 12)
            {
                Console.WriteLine("That is not a valid month. Let's try again.");
                CardPaid();
            }

            int expiryYear = GetIntInput("Please enter the last 2 digits of the year your cards expires as an integer.") + 2000;
            if (expiryMonth < DateTime.Today.Month && expiryYear == DateTime.Today.Year || expiryYear < DateTime.Today.Year)
            { // I think cards don't expire until the end of the expiry month, so I did < instead of <=.
                Console.WriteLine("Your card is expired. Let's try again, hopefully with a different card.");
                CardPaid();
            }

            Console.WriteLine("Thank you for your business. Please come again!");
        }

        public string GetStringInput(string prompt)
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
        public int GetIntInput(string prompt)
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
