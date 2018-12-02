using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;

namespace petgoods4all.Models
{
    public class Personal_animal
    {
        public int id { get; set; }
        public int user_id {get; set;}
        public string animal {get; set;}
        
    }
}