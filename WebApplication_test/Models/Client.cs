using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_test.Models
{
    public class Client
    {
        public Client() { }
        public int id { get; set; }
        public String name { get; set; }
        public String cpf { get; set; }
        public String genre { get; set; }
        public String address { get; set; }
    }
}
