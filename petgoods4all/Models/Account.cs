using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace petgoods4all.Models
{
    public class Account
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public Boolean Admin { get; set; }
        public string voornaam { get; set; }
        public string achternaam { get; set; }
        public string straatnaam { get; set; }
        public string huisnummer { get; set; }
        public string postcode { get; set; }
        public string provincie { get; set; }
        [Required]
        public string telefoonnummer { get; set; }
        public Boolean IsEmailVerified {get; set;}
        
    }
}