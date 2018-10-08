﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using petgoods4all.Models;

namespace petgoods4all.Migrations
{
    [DbContext(typeof(ModelContext))]
    partial class ModelContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("petgoods4all.Models.Account", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("email");

                    b.Property<string>("password");

                    b.HasKey("id");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("petgoods4all.Models.Voorraad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Dier");

                    b.Property<int>("Kwantiteit");

                    b.Property<string>("Naam");

                    b.Property<string>("Prijs");

                    b.Property<string>("Subklasse");

                    b.Property<string>("image");

                    b.HasKey("Id");

                    b.ToTable("Voorraad");
                });
#pragma warning restore 612, 618
        }
    }
}
