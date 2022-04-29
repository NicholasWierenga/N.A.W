namespace PointOfSale
{
    public class Program
    {
        
        public static void Main()
        {
            Console.WriteLine();
            Console.WriteLine("              Welcome to N.A.W.");
            Console.WriteLine("Take a moment to look over the menu and I will take your order!");

                
                do
                {
                    Items items = new Items();
                    items.CheckOut();
                    Console.WriteLine("Would you like to run the program again? y/n");
                } while (helper.RunAgain());

            Console.WriteLine("Goodbye.");
            
            
        }
        
    }
}