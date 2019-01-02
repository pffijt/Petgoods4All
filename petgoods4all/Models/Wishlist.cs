using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace petgoods4all.Models
{
    public class Wishlist
    {
        public int id {get; set;}
        public int customerid { get; set; }
        public int productid { get; set; }
        public int quantity {get; set;}
    }
}