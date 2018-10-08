using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petgoods4all.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public List<Voorraad> Voorraad { get; set; }
        public Account Account { get; set; }
    }
}
