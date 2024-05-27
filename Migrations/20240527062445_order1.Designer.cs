﻿// <auto-generated />
using Candy_Shop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Candy_Shop.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240527062445_order1")]
    partial class order1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.5");

            modelBuilder.Entity("Candy_Shop.Models.Czekoladka", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("cena")
                        .HasColumnType("TEXT");

                    b.Property<int>("czekolada")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("masa")
                        .HasColumnType("TEXT");

                    b.Property<string>("nazwa")
                        .IsRequired()
                        .HasMaxLength(32)
                        .HasColumnType("TEXT");

                    b.Property<string>("opis")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("orzechy")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.ToTable("Czekoladki");

                    b.HasData(
                        new
                        {
                            id = 1,
                            cena = 15.32m,
                            czekolada = 1,
                            masa = 0.53m,
                            nazwa = "Testowa",
                            opis = "Testowa czekoladka",
                            orzechy = 1
                        },
                        new
                        {
                            id = 2,
                            cena = 15.32m,
                            czekolada = 2,
                            masa = 0.53m,
                            nazwa = "Testowa 2",
                            opis = "Kolejna czekoladka testowa"
                        });
                });

            modelBuilder.Entity("Candy_Shop.Models.Order", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("id_zawartosci")
                        .HasColumnType("INTEGER");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Candy_Shop.Models.User", b =>
                {
                    b.Property<string>("username")
                        .HasColumnType("TEXT");

                    b.Property<string>("apiToken")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("type")
                        .HasColumnType("INTEGER");

                    b.HasKey("username");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            username = "admin",
                            apiToken = "aae1e103-bca5-9fa1-ba8c-42058b4abf28",
                            password = "21232F297A57A5A743894A0E4A801FC3",
                            type = 2
                        });
                });

            modelBuilder.Entity("Candy_Shop.Models.Zawartosc", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("id_czekoladki")
                        .HasColumnType("INTEGER");

                    b.Property<int>("sztuk")
                        .HasColumnType("INTEGER");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.HasIndex("id_czekoladki");

                    b.HasIndex("username");

                    b.ToTable("Zawartosc");
                });

            modelBuilder.Entity("Candy_Shop.Models.Zawartosc", b =>
                {
                    b.HasOne("Candy_Shop.Models.Czekoladka", "Czekoladka")
                        .WithMany()
                        .HasForeignKey("id_czekoladki")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Candy_Shop.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("username")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Czekoladka");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
