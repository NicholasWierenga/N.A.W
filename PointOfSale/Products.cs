using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointOfSale
{
    public enum Category
    {
        Entree,
        Side,
        Drink,
    }

    class Products
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public string Desc { get; set; }
        public double Price { get; set; }

        public Products(string Name, Category Category, string Desc, double Price)
        {
            this.Name = Name;
            this.Category = Category;
            this.Desc = Desc;
            this.Price = Price;
        }
    }
}
