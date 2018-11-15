using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace petgoods4all.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int StarRating { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
