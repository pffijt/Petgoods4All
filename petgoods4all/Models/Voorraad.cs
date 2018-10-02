using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace petgoods4all.Models
{
    public class Voorraad
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Dier { get; set; }
        public string Subklasse { get; set; }
        public int Kwantiteit { get; set; }
        public string Prijs { get; set; }
        public string image {get; set;}
    }
}