﻿// <auto-generated />
using HotelListingAPI.VSCode.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HotelListingAPI.VSCode.Migrations
{
    [DbContext(typeof(HotelListingDbContext))]
    [Migration("20230710064225_SeededCountriesAndHotels")]
    partial class SeededCountriesAndHotels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HotelListingAPI.VSCode.Data.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("ShortName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "United States of America",
                            ShortName = "USA"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Canada",
                            ShortName = "Canada"
                        },
                        new
                        {
                            Id = 3,
                            Name = "France",
                            ShortName = "France"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Germany",
                            ShortName = "Germany"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Italy",
                            ShortName = "Italy"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Japan",
                            ShortName = "Japan"
                        },
                        new
                        {
                            Id = 7,
                            Name = "United Kingdom",
                            ShortName = "UK"
                        });
                });

            modelBuilder.Entity("HotelListingAPI.VSCode.Data.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<int>("CountryId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<double>("Rating")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Lakewood Address",
                            CountryId = 1,
                            Name = "Lakewood",
                            Rating = 4.2999999999999998
                        },
                        new
                        {
                            Id = 2,
                            Address = "Bridgewood Address",
                            CountryId = 1,
                            Name = "Bridgewood",
                            Rating = 4.4000000000000004
                        },
                        new
                        {
                            Id = 3,
                            Address = "Ridgewood Address",
                            CountryId = 1,
                            Name = "Ridgewood",
                            Rating = 4.5
                        },
                        new
                        {
                            Id = 4,
                            Address = "Hilton Address",
                            CountryId = 2,
                            Name = "Hilton",
                            Rating = 4.5999999999999996
                        });
                });

            modelBuilder.Entity("HotelListingAPI.VSCode.Data.Hotel", b =>
                {
                    b.HasOne("HotelListingAPI.VSCode.Data.Country", "Country")
                        .WithMany("Hotels")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Country");
                });

            modelBuilder.Entity("HotelListingAPI.VSCode.Data.Country", b =>
                {
                    b.Navigation("Hotels");
                });
#pragma warning restore 612, 618
        }
    }
}
