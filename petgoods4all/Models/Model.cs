using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;


namespace petgoods4all.Models
{
    public class Model : DbContext
    {   //this is actual entity object linked to the Voorraad in our DB
        public DbSet<Voorraad> Voorraad { get; set; }

        //this method is run automatically by EF the first time we run the application
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //here we define the name of our database
            optionsBuilder.UseNpgsql("User ID=postgres;Password=admin;Host=localhost;Port=5432;Database=MovieDB;Pooling=true;");
        }
    }

    public class Voorraad
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public string Dier { get; set; }
        public string Subklasse { get; set; }
        public int Kwantiteit { get; set; }
        public double Prijs { get; set; }
    }
}