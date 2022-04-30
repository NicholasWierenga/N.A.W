using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale
{
    public class Items
    {
        List<Products> allItems = new List<Products>
        {
            new Products("Burger", Category.Entree, "It's a cheeseburger", 5.00),
            new Products("Nuggets", Category.Entree, "8 chicken nuggets", 4.50),
            new Products("Taco", Category.Entree, "A taco", 3.25),
            new Products("Hot dog", Category.Entree, "A hot dog", 2.00),
            new Products("Fries", Category.Side, "Curly fries", 1.50),
            new Products("Tots", Category.Side, "Tater tots", 3.25),
            new Products("Onion Rings", Category.Side, "6 big rings", 2.50),
            new Products("Cole Slaw", Category.Side, "It's cole slaw", 1.25),
            new Products("Lemonade", Category.Drink, "Sour drink", 5.00),
            new Products("Coke", Category.Drink, "Coca-cola", 2.00),
            new Products("Water", Category.Drink, "Plain h2o", 0.99),
            new Products("Slush", Category.Drink, "Cold drink", 5.00),
        };
        List<string> orderDetails = new List<string>();
        int padLength = 21; // to adjust to change spacing between order details and amounts. It should be odd so ===receipt=== is symmetrical

        public void CheckOut()
        {
            double subTotal = 0;
            double salesTax;
            double total;

            do
            {
                PrintAll();

                string order = Helper.GetStringInput("What item number from the menu would you like to order?");

                if (int.TryParse(order, out int index) && index >= 1 && index <= allItems.Count)
                {
                    Console.WriteLine("You've selected: ");
                    Console.WriteLine(allItems[index - 1].Name + " for " + "$" + allItems[index - 1].Price.ToString("0.00") + ".");
                }
                else
                {
                    Console.WriteLine("I'm sorry, we do not have that item. Let's try again.");
                    CheckOut();
                    return;
                }

                int amountOrdered = Helper.GetIntInput("How many do you want to order?");

                Console.WriteLine();
                
                subTotal = subTotal + amountOrdered * allItems[index - 1].Price;
                salesTax = 0.06 * subTotal;
                total = subTotal + salesTax;

                orderDetails.Add((amountOrdered + "x " + allItems[index - 1].Name).PadRight(padLength)
                    + "$" + (amountOrdered * allItems[index - 1].Price).ToString("0.00") + ".");

                PrintCurrentOrder(subTotal, salesTax, total);

                Console.WriteLine();

            } while (Helper.RunAgain("Would you like to order anything else today? y/n"));

            CompleteOrder(subTotal, salesTax, total);

        }
        public void PrintCurrentOrder(double subPrice, double salesTax, double total)
        {
            for (int i = 0; i < orderDetails.Count; i++)
            {
                Console.WriteLine(orderDetails[i]);
            }

            Console.WriteLine("Subtotal ".PadRight(padLength) + "$" + subPrice.ToString("0.00") + ".");
            Console.WriteLine("Sales tax ".PadRight(padLength) + "$" + salesTax.ToString("0.00") + ".");
            Console.WriteLine("".PadLeft(padLength + ("$" + total.ToString("0.00") + ".").Length, '_'));
            Console.WriteLine("Grand total ".PadRight(padLength) + "$" + total.ToString("0.00") + ".");
        }
        public void CompleteOrder(double subPrice, double salesTax, double total)
        {
            Payment customerPay = new Payment(Math.Round(total, 2), padLength); // We do round, because total might have more than 2 decimals.
            string payMessage = customerPay.Pay();

            Console.WriteLine("RECEIPT".PadLeft((int)(padLength / 2) + 7, '=').PadRight(padLength + 6, '=')); // Prints the ===Receipt=== line
            
            PrintCurrentOrder(subPrice, salesTax, total);

            Console.WriteLine(payMessage);
            Console.WriteLine("Thank you for your business. Please come again!");
        }
        public void PrintAll()
        {
            for (int i = 0; i < allItems.Count; i++)
            {
                Console.WriteLine(((i + 1) + "  " + allItems[i].Name + " ").PadRight(25, '.') + " Price: $" + allItems[i].Price.ToString("0.00") + ".");
                Console.WriteLine("(".PadLeft(12) + allItems[i].Desc + ")  ");
            }
        }
    }
}

