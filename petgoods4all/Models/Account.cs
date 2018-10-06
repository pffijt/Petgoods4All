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
        public bool Admin { get; set; }
        public string Naam { get; set; }
        public string Achternaam { get; set; }
        public string Straatnaam { get; set; }
        public string Huisnummer { get; set; }
        public string Postcode { get; set; }
        public string Gemeente { get; set; }
    }
}