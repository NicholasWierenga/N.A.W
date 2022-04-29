namespace PointOfSale
{
    public class Program
    {
        
        public static void Main()
        {
            Console.WriteLine();
            Console.WriteLine("              Welcome to N.A.W.");
            Console.WriteLine("Take a moment to look over the menu and I will take your order!");

            Console.WriteLine(4134.ToString().Length);

                Items items = new Items();
                do
                {
                    items.CheckOut();
                } while (RunAgain());
            
            
        }
        public static bool RunAgain()
        {
            Console.WriteLine("Would you like to run the program again? y/n");
            string answer = Console.ReadLine().ToLower().Trim();
            if (answer == "y")
            {
                return true;
            }
            else if (answer == "n")
            {
                Console.WriteLine("Goodbye.");
                return false;
            }
            else
            {
                Console.WriteLine("I didn't understand that. Let's try again.");
                return RunAgain();
            }
        }
    }
}