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
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public Boolean Admin { get; set; }
        [Required]
        public string voornaam { get; set; }
        [Required]
        public string achternaam { get; set; }
        [Required]
        public string straatnaam { get; set; }
        [Required]
        public string huisnummer { get; set; }
        [Required]
        public string postcode { get; set; }
        [Required]
        public string provincie { get; set; }
        [Required]
        [Phone]
        [StringLength(30, MinimumLength = 9, ErrorMessage = "Uw telefoonnummer is niet correct!")]
        public string telefoonnummer { get; set; }
        public Boolean IsEmailVerified {get; set;}
        
    }
}