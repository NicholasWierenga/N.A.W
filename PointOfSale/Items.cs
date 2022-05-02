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
        List<Products> orderDetails = new List<Products>();
        List<int> numOrderedList = new List<int>();
        int padLength = 27; // to adjust to change spacing between order details and amounts.
        int index;
        int amountOrdered;

        public void CheckOut()
        {
            double subTotal = 0;
            double salesTax;
            double total;

            do
            {
                PrintAll();
                
                index = GetUserOrder();

                amountOrdered = Helper.GetIntInput("How many do you want to order?");

                Console.WriteLine();
                
                subTotal = Math.Round(subTotal + amountOrdered * allItems[index].Price, 2);
                salesTax = Math.Round(0.06 * subTotal, 2);
                total = Math.Round(subTotal + salesTax, 2);

                if (orderDetails.Contains(allItems[index])) // For when customer orders more of an already ordered item.
                {
                    numOrderedList[orderDetails.IndexOf(allItems[index])] += amountOrdered; // Adds the extra to our numOrderedList
                }
                else // For when item ordered is new.
                {
                    orderDetails.Add(allItems[index]);
                    numOrderedList.Add(amountOrdered);
                }
                

                PrintCurrentOrder(subTotal, salesTax, total);

                Console.WriteLine();

            } while (Helper.RunAgain("Would you like to order anything else today? y/n"));

            CompleteOrder(subTotal, salesTax, total);
        }

        public void PrintAll()
        {
            for (int i = 0; i < allItems.Count; i++)
            {
                Console.WriteLine(((i + 1) + "  " + allItems[i].Name + " ").PadRight(25, '.') + " Price: $" + allItems[i].Price.ToString("0.00") + ".");
                Console.WriteLine("(".PadLeft(12) + allItems[i].Desc + ")  ");
            }
        }

        public int GetUserOrder()
        {
            string order = Helper.GetStringInput("What item from the menu would you like to order? Enter its number or name.");
            index = allItems.FindIndex(item => item.Name.ToLower() == order); // We check if the order is in the list, equals -1 if it isn't.

            if (index != -1) // For when user entered name.
            {
                Console.WriteLine("You've selected: ");
                Console.WriteLine(allItems[index].Name + " for " + "$" + allItems[index].Price.ToString("0.00") + ".");
                return index;
            }
            else if (int.TryParse(order, out index) && index >= 1 && index <= allItems.Count) // For when user entered number.
            {
                Console.WriteLine("You've selected: ");
                Console.WriteLine(allItems[index - 1].Name + " for " + "$" + allItems[index - 1].Price.ToString("0.00") + ".");
                return index - 1; // Since we listed off the items starting at 1 and not 0, we have to return whatever they put - 1.
            }
            else
            {
                PrintAll();
                Console.WriteLine("We do not have that item. Let's try again.");
                return GetUserOrder();
            }
        }

        public void PrintCurrentOrder(double subPrice, double salesTax, double total)
        {
            int padTotal = ("$" + total.ToString("0.00") + ".").Length; // This is to right-align the costs so digits line up.

            for (int i = 0; i < orderDetails.Count; i++) // Prints out customer's order with number ordered, name, and price for them.
            {
                Console.WriteLine((numOrderedList[i] + "x " + orderDetails[i].Name).PadRight(padLength)
                    + ("$" + (numOrderedList[i] * orderDetails[i].Price).ToString("0.00") + ".").PadLeft(padTotal));
            }
            Console.WriteLine();


            Console.WriteLine("Subtotal ".PadRight(padLength) + ("$" + subPrice.ToString("0.00") + ".").PadLeft(padTotal));
            Console.WriteLine("Sales tax ".PadRight(padLength) + ("$" + salesTax.ToString("0.00") + ".").PadLeft(padTotal));
            Console.WriteLine("".PadLeft(padLength + ("$" + total.ToString("0.00") + ".").Length, '_'));
            Console.WriteLine("Grand total ".PadRight(padLength) + "$" + total.ToString("0.00") + ".");
        }

        public void CompleteOrder(double subPrice, double salesTax, double total)
        {
            int padTotal = ("$" + total.ToString("0.00") + ".").Length; // This is to right-align the costs so digits line up.
            
            Payment customerPay = new Payment(total, padLength, padTotal); 
            string payMessage = customerPay.Pay();// Gets the pay message saying what they paid by and for how much.

            int receiptLength = padLength + ("$" + total.ToString("0.00") + ".").Length; // Used to print the ===RECEIPT=== line
            Console.WriteLine("RECEIPT".PadLeft(receiptLength / 2 + 4, '=').PadRight(receiptLength, '=')); 

            PrintCurrentOrder(subPrice, salesTax, total);

            Console.WriteLine(payMessage);
            Console.WriteLine("Thank you for your business. Please come again!");
        }
    }
}

