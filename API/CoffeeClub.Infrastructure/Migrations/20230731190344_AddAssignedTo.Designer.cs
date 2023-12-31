﻿// <auto-generated />
using System;
using CoffeClub.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoffeClub.Infrastructure.Migrations
{
    [DbContext(typeof(CoffeeClubContext))]
    [Migration("20230731190344_AddAssignedTo")]
    partial class AddAssignedTo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CoffeeBeanClub.Domain.Models.CoffeeBean", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("InStock")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Roast")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("CoffeeBeans");
                });

            modelBuilder.Entity("CoffeeClub.Domain.Models.DrinkOrder", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CoffeeBeanId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Drink")
                        .HasColumnType("int");

                    b.Property<int?>("MilkType")
                        .HasColumnType("int");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CoffeeBeanId");

                    b.HasIndex("OrderId");

                    b.ToTable("DrinkOrders");
                });

            modelBuilder.Entity("CoffeeClub.Domain.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AssignedToId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("AssignedToId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("CoffeeClub.Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AuthId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AuthProvider")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CoffeeBeanClub.Domain.Models.CoffeeBean", b =>
                {
                    b.HasOne("CoffeeClub.Domain.Models.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("CoffeeClub.Domain.Models.DrinkOrder", b =>
                {
                    b.HasOne("CoffeeBeanClub.Domain.Models.CoffeeBean", "CoffeeBean")
                        .WithMany()
                        .HasForeignKey("CoffeeBeanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoffeeClub.Domain.Models.Order", null)
                        .WithMany("DrinkOrders")
                        .HasForeignKey("OrderId");

                    b.Navigation("CoffeeBean");
                });

            modelBuilder.Entity("CoffeeClub.Domain.Models.Order", b =>
                {
                    b.HasOne("CoffeeClub.Domain.Models.User", "AssignedTo")
                        .WithMany()
                        .HasForeignKey("AssignedToId");

                    b.HasOne("CoffeeClub.Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedTo");

                    b.Navigation("User");
                });

            modelBuilder.Entity("CoffeeClub.Domain.Models.Order", b =>
                {
                    b.Navigation("DrinkOrders");
                });
#pragma warning restore 612, 618
        }
    }
}
