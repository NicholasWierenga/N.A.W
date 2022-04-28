namespace PointOfSale
{
    public class Program
    {
        
        public static void Main()
        {
            Console.WriteLine("Welcome to N.A.W."); // maybe call it Wendy's?

            Items items = new Items();

            items.CheckOut();
            
        }
    }
}