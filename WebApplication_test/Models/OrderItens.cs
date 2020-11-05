using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_test.Controllers;

namespace WebApplication_test.Models
{
    public class OrderItens
    {
        public int id { get; set; }
        public ProductOrder productOrder { get; set; }
        public int idProduct { get; set; }
        public Product product { get; set; }
        public int Quantity { get; set; }
        public Decimal unitPriceDescount { get; set; }

        public Product getProduct(int id)
        {
            Product product = ProductController.Products.FirstOrDefault(delegate (Product p) { return p.id == id; });
            this.product = product;
            return product;
        }
    }
}
