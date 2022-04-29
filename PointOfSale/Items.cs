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
        public Items()
        {
        }

        public void PrintAll()
        {
            for (int i = 0; i < allItems.Count; i++)
            {
                Console.WriteLine((i + 1) + "  " + allItems[i].Name + " ............................. " + "Price: $".PadLeft(10) + allItems[i].Price.ToString("0.00") + ".");
                Console.WriteLine("(".PadLeft(12) + allItems[i].Desc + ")  ");
            }
        }

        public void CheckOut()
        {
            double RunningTotal = 0;
            string UserInput;
            List<Products> custFoodPicked = new List<Products>();
            List<string> orderDetails = new List<string>();

            do
            {
                PrintAll();
                Console.WriteLine();
                Console.WriteLine("What item number from the menu would you like to order?");
                string order = Console.ReadLine().Trim();


                if (int.TryParse(order, out int index) && index >= 1 && index <= allItems.Count)
                {
                    Console.WriteLine("You've selected: ");
                    allItems[index - 1].PrintItem();
                }
                else
                {
                    Console.WriteLine("I'm sorry, we do not have that item. Let's try again.");
                    CheckOut();
                }

                int amountOrdered = OrderAmount(index);
                Console.WriteLine();
                
                
                double subPrice = amountOrdered * allItems[index - 1].Price;
                double salesTax = 0.06 * subPrice;
                double total = subPrice + salesTax;
                RunningTotal = total + RunningTotal;


                custFoodPicked.Add(allItems[index - 1]); // Maybe do an if or something to merge orders, so if someone order 1 burger, then another,
                                                         // then another, and print out a bunch of lines of them order the same item.
                                                         // If it's really bad we could make a new object just to hold products obj and the amount ordered or not bother.


                orderDetails.Add("You ordered " + amountOrdered + " of " + allItems[index - 1].Name +
                        ", which costs $" + (amountOrdered * allItems[index - 1].Price).ToString("0.00") + ".");
                
                for (int i = 0; i < orderDetails.Count; i++)
                {
                    Console.WriteLine(orderDetails[i]);
                }
                
                //Console.WriteLine("Amount they ordered: " + amountOrdered);
                Console.WriteLine(("Subtotal: " + "$" + subPrice.ToString("0.00") + ".").PadLeft(30));
                Console.WriteLine(("Sales tax: " + "$" + salesTax.ToString("0.00") + ".").PadLeft(30));
                //Console.WriteLine("Total: " + "$" + total.ToString("0.00") + ".");
                Console.WriteLine("____________________________________________");
                Console.WriteLine(("Grand total: " + "$" + RunningTotal.ToString("0.00") + ".").PadLeft(30));
                Console.WriteLine();
                Console.WriteLine("Do you want to add to your order? Y or N?");
                
                UserInput = Console.ReadLine().Trim().ToLower();

            } while (UserInput == "y");

            Payment customerPay = new Payment(RunningTotal);
            customerPay.Pay();
        }

        public int OrderAmount(int index)
        {
            Console.WriteLine("How many do you want to order?");

            string input = Console.ReadLine().Trim();

            do
            {
                if (int.TryParse(input, out int numberOrdered) && numberOrdered > 0)
                {
                    return numberOrdered;
                }
                else
                {
                    Console.WriteLine("That is not a number. Let's try again.");
                    return OrderAmount(index);
                }

            } while (true);
        }
    }
}

