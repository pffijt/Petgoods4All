using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petgoods4all.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public List<int> Voorraad { get; set; }
        public int Account { get; set; }
    }
}
