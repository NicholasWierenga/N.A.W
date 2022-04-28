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
            new Products("Burger", Category.Entree, "It's a double cheeseburger", 5.00),
            new Products("Nuggets", Category.Entree, "8 chicken nuggets.", 4.50),
            new Products("Taco", Category.Entree, "A taco", 3.25),
            new Products("Hot dog", Category.Entree, "A hot dog, which is technically a sandwich", 2.00),
            new Products("Fries", Category.Side, "Half pound of curly fries", 1.50),
            new Products("Tots", Category.Side, "Tater tots", 3.25),
            new Products("Onion Rings", Category.Side, "8 big onion rings", 2.50),
            new Products("Cole Slaw", Category.Side, "It's just cole slaw", 1.25),
            new Products("Lemonade", Category.Drink, "Sour drink", 5.00),
            new Products("Coke", Category.Drink, "Coca-cola", 2.00),
            new Products("Water", Category.Drink, "Plain h2o", 0.99),
            new Products("Slush", Category.Drink, "Cold slushy", 5.00),
        };
        public Items()
        {
        }
        
        public void PrintAll()
        {
            for (int i = 0; i < allItems.Count; i++)
            {
                Console.WriteLine("Name: " + allItems[i].Name + " Category: " + allItems[i].Category
                    + " Description: " + allItems[i].Desc + " Price: $" + allItems[i].Price.ToString("0.00") + ".");
            }
        }

        public void CheckOut()
        {
            PrintAll();

            Console.WriteLine("What item would you like to buy?");
            string input = Console.ReadLine().Trim();

            if (int.TryParse(input, out int index) && index >= 1 && index <= allItems.Count)
            {
                Console.WriteLine("You've selected: ");
                allItems[index - 1].PrintItem();
            }
            else
            {
                Console.WriteLine("That is not a valid index integer. Let's try again.");
                CheckOut();
            }

            int amountOrdered = OrderAmount(index);
            double subPrice = amountOrdered * allItems[index - 1].Price;
            double salesTax = 0.06 * subPrice;
            double total = subPrice + salesTax;

            

            Console.WriteLine("Amount they ordered: " + amountOrdered);
            Console.WriteLine("Subtotal: " + "$" + subPrice.ToString("0.00") + ".");
            Console.WriteLine("Sales tax: " + "$" + salesTax.ToString("0.00") + ".");
            Console.WriteLine("Total: " + "$" + total.ToString("0.00") + ".");

            Payment pay = new Payment(total); 
            pay.Pay();

            
            
        }

        private int OrderAmount(int index)
        {
            Console.WriteLine("How many " + allItems[index - 1].Name + " would you like to order?");

            string input = Console.ReadLine().Trim();

            do
            {
                if (int.TryParse(input, out int numberOrdered))
                {
                    return numberOrdered;
                }
                else
                {
                    Console.WriteLine("That is not a valid integer. Let's try again.");
                    return OrderAmount(index);
                }

            } while (true);
        }
    }
}

