using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace petgoods4all.Models
{
    public class Account
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Boolean Admin { get; set; }
    }
}