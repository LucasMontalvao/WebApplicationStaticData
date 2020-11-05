using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_test.Models
{
    public class Product
    {
        public int id { get; set; }
        public String description { get; set; }
        public int quantity { get; set; }
        public Decimal price { get; set; }
        public Decimal discount { get ; set; }

        //Just for show all data in a single line in dropdown
        public string productDescription { get {
                return description + ", quantity in stock: " + 
                       quantity + ", price: " + price;
            } 
        }
        
    }
}
