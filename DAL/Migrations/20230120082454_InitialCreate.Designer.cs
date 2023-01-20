﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20230120082454_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.2");

            modelBuilder.Entity("Domain.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("OptimalQuantity")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasPrecision(12, 10)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("Domain.ItemInStock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemInStocks");
                });

            modelBuilder.Entity("Domain.Job", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("Domain.JobItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("JobId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("JobId");

                    b.ToTable("JobItems");
                });

            modelBuilder.Entity("Domain.PerformedJob", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("JobId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("TotalCost")
                        .HasPrecision(12, 10)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("PerformedJobs");
                });

            modelBuilder.Entity("Domain.UsedItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PerformedJobId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("PerformedJobId");

                    b.ToTable("UsedItems");
                });

            modelBuilder.Entity("Domain.Item", b =>
                {
                    b.HasOne("Domain.Category", "Category")
                        .WithMany("Items")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Domain.ItemInStock", b =>
                {
                    b.HasOne("Domain.Item", "Item")
                        .WithMany("ItemInStocks")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Domain.JobItem", b =>
                {
                    b.HasOne("Domain.Item", "Item")
                        .WithMany("JobItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Job", "Job")
                        .WithMany("JobItems")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("Domain.PerformedJob", b =>
                {
                    b.HasOne("Domain.Job", "Job")
                        .WithMany()
                        .HasForeignKey("JobId");

                    b.Navigation("Job");
                });

            modelBuilder.Entity("Domain.UsedItem", b =>
                {
                    b.HasOne("Domain.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.PerformedJob", "PerformedJob")
                        .WithMany("UsedItems")
                        .HasForeignKey("PerformedJobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("PerformedJob");
                });

            modelBuilder.Entity("Domain.Category", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Domain.Item", b =>
                {
                    b.Navigation("ItemInStocks");

                    b.Navigation("JobItems");
                });

            modelBuilder.Entity("Domain.Job", b =>
                {
                    b.Navigation("JobItems");
                });

            modelBuilder.Entity("Domain.PerformedJob", b =>
                {
                    b.Navigation("UsedItems");
                });
#pragma warning restore 612, 618
        }
    }
}
