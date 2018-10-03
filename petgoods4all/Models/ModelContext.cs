using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;


namespace petgoods4all.Models
{
    public class ModelContext : DbContext
    {   //this is actual entity object linked to the Voorraad in our DB
        public DbSet<Voorraad> Voorraad { get; set; }

        public DbSet<Account> Account { get; set; }

        //this method is run automatically by EF the first time we run the application
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //here we define the name of our database
            optionsBuilder.UseNpgsql("User ID=postgres;Password=jessiefag123;Host=localhost;Port=5433;Database=petgoods4all;Pooling=true;");
        }
    } 
}