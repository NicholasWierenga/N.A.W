using System;

public class Class1
{
	public Class1()
	{
        public Item(string Name, string Discription, string Catigory, double Price)
        {
            this.Name = Name;
            this.Catigory = Catigory;
            this.Discription = Discription;
            this.Price = Price;
 
        }

        public string Name { get; set; }

        public string Catigory { get; set; }

        public string Discription { get; set; }

        public double Price{ get; set; }

    public override string ToString ()
    {
        return Name + Catigory + Discription + Price;
    }
}
}
