namespace PointOfSale
{
    public class Program
    {
        
        public static void Main()
        {
            Console.WriteLine();
            Console.WriteLine("              Welcome to N.A.W.");
            Console.WriteLine("Take a moment to look over the menu and I will take your order!");

            Items items = new Items();
            
            items.CheckOut();
            
            
        }
    }
}