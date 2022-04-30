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
        public int padLength { get; set; }


        public Payment(double TotalOwed, int padLength)
        {
            this.TotalOwed = TotalOwed;
            this.padLength = padLength;
        }
        public string Pay()
        {
            PayOption option = GetPaymentMethod();
            string output = "";

            switch (option)
            {
                case PayOption.cash:
                    output = "\n" + CashPaid();
                    break;
                case PayOption.check:
                    CheckPaid();
                    break;
                case PayOption.card:
                    CardPaid();
                    break;
            }

            output = ("Paid by " + option.ToString()).PadRight(padLength) + "$" + TotalOwed.ToString("0.00") + "."
                + output;
          
            return output;
        }

        public PayOption GetPaymentMethod()
        {
            string input = Helper.GetStringInput("What method of payment do you want to use? We accept cash, check, or card?");

            if (Enum.TryParse(input, out PayOption payOption) &&
                payOption == PayOption.cash || payOption == PayOption.check || payOption == PayOption.card)
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

        public string CashPaid()
        {
            string input = Helper.GetStringInput("How much cash are you using to pay for this order?");
            
            if (double.TryParse(input, out double cashPaid) && cashPaid >= TotalOwed)
            {
                if (cashPaid > TotalOwed) // We only want to tell a customer their change if they're actually getting some.
                {
                    return "Change ".PadRight(padLength) + "$" + (cashPaid - TotalOwed).ToString("0.00") + ".";
                }
                return ""; // For when customer pays exact amount.
            }
            else
            {
                Console.WriteLine("You've given less than the amount you owe. Let's try again.");
                return CashPaid();
            }
            
        }

        public void CheckPaid()
        {
            int checkNum = Helper.GetIntInput("Please enter your check number.");
            if (checkNum.ToString().Length != 3 && checkNum.ToString().Length != 4)
            {
                Console.WriteLine("A check number must be an integer that is 3 or 4 digits long.");
                CheckPaid();
                return;
            }
          
            string input = Helper.GetStringInput("What is the check amount?");
            if (double.TryParse(input, out double amountPaid))
            {
                if (amountPaid < TotalOwed)
                {
                    Console.WriteLine("Sorry, but this does not cover the bill. Please write one for the exact total owed.");
                    CheckPaid();
                    return;
                }
                else if (amountPaid > TotalOwed)
                {
                    Console.WriteLine("Sorry, but we don't give change for payments involving checks. Please write one for the exact total owed.");
                    CheckPaid();
                    return;
                }
            }
            else
            {
                Console.WriteLine("This is not a valid check. Let's try again.");
                CheckPaid();
                return;
            }
        }

        public void CardPaid()
        {
            string cardNumber = Helper.GetStringInput("Please enter your 16 card number.");
            if (!long.TryParse(cardNumber, out long outputNum) || cardNumber.ToString().Length != 16) 
            {  // We use a long here because an int can't store 16 digits.

                Console.WriteLine("You entered non numbers or an incorrect amount of digits. Let's try again");
                CardPaid();
                return;
            }

            int cvv = Helper.GetIntInput("Please enter your card's CCV.");
            if (cvv.ToString().Length != 3 && cvv.ToString().Length != 4)
            {
                Console.WriteLine("A CVV must be a 3 or 4 digit number. Let's try again.");
                CardPaid();
                return;
            }

            int expiryMonth = Helper.GetIntInput("Please enter the month your card expires as an integer.");
            if (expiryMonth < 1 || expiryMonth > 12)
            {
                Console.WriteLine("That is not a valid month. Let's try again.");
                CardPaid();
                return;
            }

            int expiryYear = Helper.GetIntInput("Please enter the last 2 digits of the year your cards expires as an integer.") + 2000;
            if (expiryMonth < DateTime.Today.Month && expiryYear == DateTime.Today.Year || expiryYear < DateTime.Today.Year)
            { // I think cards don't expire until the end of the expiry month, so I did < instead of <=.
                Console.WriteLine("Your card is expired. Let's try again.");
                CardPaid();
                return;
            }
        }
    }
}
