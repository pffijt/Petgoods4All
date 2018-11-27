using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petgoods4all.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int? AccountId { get; set; }
        public DateTime Datum { get; set; }
        public string Prijs { get; set; }
        public string OrderStatus { get; set; }
    }
}
