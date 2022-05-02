namespace PointOfSale
{
    public class Program
    {

        public static void Main()
        {
            string filePath = @"C:\Users\wendy\OneDrive\Desktop\naw project\N.A.W\AllItems.txt";

            List<string> lines = File.ReadAllLines(filePath).ToList();

            foreach (string line in lines)
            {
                Console.WriteLine(line);
                //string[] things = line.Spilt(",");
                //Items I = new Items(Items[1], Items[2], Items[3], Items[4]);
                //allItems.Add(I);
            }
            File.WriteAllLines(filePath, lines);
            List<string> outContents = new List<string>();
            foreach (string line in lines)
            {

                outContents.Add(line.ToString());
            }
            string outFile = @"C:\Users\wendy\OneDrive\Desktop\naw project\N.A.W\outFile.txt";
            File.WriteAllLines(outFile, outContents);



            Console.WriteLine();
            Console.WriteLine("              Welcome to N.A.W.");
            Console.WriteLine("Take a moment to look over the menu and I will take your order!");

            //    do
            //    {
            //        Items items = new Items();
            //        items.CheckOut();

            //    } while (Helper.RunAgain("Would you like to run the program again? y/n"));

            //Console.WriteLine("Goodbye.");
        }
        
    }
}