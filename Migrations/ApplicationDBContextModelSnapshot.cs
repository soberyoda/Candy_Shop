﻿// <auto-generated />
using Candy_Shop.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Candy_Shop.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("Candy_Shop.Models.Zawartosc", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("id_czekoladki")
                        .HasColumnType("INTEGER");

                    b.Property<int>("sztuk")
                        .HasColumnType("INTEGER");

                    b.HasKey("id");

                    b.HasIndex("id_czekoladki");

                    b.ToTable("Zawartosc");
                });

            modelBuilder.Entity("Candy_Shop.Models.Zawartosc", b =>
                {
                    b.HasOne("Candy_Shop.Models.Czekoladka", "Czekoladka")
                        .WithMany("zawartosci")
                        .HasForeignKey("id_czekoladki")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Czekoladka");
                });

            modelBuilder.Entity("Candy_Shop.Models.Czekoladka", b =>
                {
                    b.Navigation("zawartosci");
                });
#pragma warning restore 612, 618
        }
    }
}
