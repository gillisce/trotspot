﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TrotSpotAPI.Models;

namespace TrotSpotAPI.Migrations
{
    [DbContext(typeof(TrotSpotContext))]
    partial class TrotSpotContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TrotSpotAPI.Models.Class", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("ActiveClass")
                        .HasColumnType("bit");

                    b.Property<string>("ClassName")
                        .HasColumnType("text");

                    b.Property<string>("ClassNotes")
                        .HasColumnType("text");

                    b.Property<int>("ClassNumber")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ClassStart")
                        .HasColumnType("datetime");

                    b.Property<float?>("Cost")
                        .HasColumnType("float");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Creator")
                        .HasColumnType("text");

                    b.Property<bool>("Flag1")
                        .HasColumnType("bit");

                    b.Property<bool>("Flag2")
                        .HasColumnType("bit");

                    b.Property<int?>("MaxRiderCount")
                        .HasColumnType("int");

                    b.Property<int>("MinRiderCount")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Modifier")
                        .HasColumnType("text");

                    b.Property<int>("ShowID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Class");
                });

            modelBuilder.Entity("TrotSpotAPI.Models.Division", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Creator")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int?>("MaxAge")
                        .HasColumnType("int");

                    b.Property<int?>("MaxNumRiders")
                        .HasColumnType("int");

                    b.Property<int?>("MinAge")
                        .HasColumnType("int");

                    b.Property<int?>("MinNumRiders")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifiedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Modifier")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("ShowID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Division");
                });

            modelBuilder.Entity("TrotSpotAPI.Models.Rider", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<float>("AmountDue")
                        .HasColumnType("float");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("HasPaid")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("text");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Rider");
                });

            modelBuilder.Entity("TrotSpotAPI.Models.Show", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("MaxRiderNumber")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime?>("RegistrationEnd")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("RegistrationStart")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("ShowDate")
                        .HasColumnType("datetime");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<int?>("ZipCode")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Show");
                });

            modelBuilder.Entity("TrotSpotAPI.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<bool>("AllRidersPaid")
                        .HasColumnType("bit");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("State")
                        .HasColumnType("text");

                    b.Property<int?>("ZipCode")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}