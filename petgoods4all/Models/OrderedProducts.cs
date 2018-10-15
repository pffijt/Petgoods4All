using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petgoods4all.Models
{
    public class OrderedProducts
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
