using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_test.Controllers;

namespace WebApplication_test.Models
{
    public class ProductOrder
    {
        public int orderId { get; set; }
        public int clientId { get; set; }
        public Client Client { get; set; }
        public Decimal totalOrderValue { get; set; }
        public Decimal totalOrderValueDescount { get; set; }

        public Client getClient(int id)
        {
            Client client = ClientController.Clients.FirstOrDefault(delegate (Client c) { return c.id == id; });
            this.Client = client;
            return client;
        }
    }
}
