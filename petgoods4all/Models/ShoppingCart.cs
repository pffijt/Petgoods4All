using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petgoods4all.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int VoorraadId { get; set; }
        public int? AccountId { get; set; }
        public int Quantity { get; set; }
    }
}
