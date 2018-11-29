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
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderedProducts> OrderedProducts { get; set; } 
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<Review> Review { get; set; }

        //this method is run automatically by EF the first time we run the application
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //here we define the name of our database
            optionsBuilder.UseNpgsql("User ID=postgres;Password=root;Host=localhost;Port=5432;Database=petgoods4all;Pooling=true;");
        }
    } 
}