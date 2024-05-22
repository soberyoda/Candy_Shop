using Candy_Shop.Models;
using Microsoft.EntityFrameworkCore;

namespace Candy_Shop.Data;

public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : DbContext(options) {
  public DbSet<Czekoladka> Czekoladki { get; set; }
  public DbSet<Zawartosc> Zawartosc { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    base.OnModelCreating(modelBuilder);

    modelBuilder
      .Entity<Czekoladka>()
      .Property(e => e.czekolada)
      .HasConversion<int>();

    modelBuilder
      .Entity<Czekoladka>()
      .Property(e => e.orzechy)
      .HasConversion<int>();

    modelBuilder
      .Entity<Czekoladka>()
      .HasData(
        new Czekoladka {
          id = 1,
          nazwa = "Testowa",
          czekolada = Czekoladka.Czekolada.Mleczna,
          orzechy = Czekoladka.Orzechy.laskowe,
          masa = 0.53m,
          cena = 15.32m,
          opis = "Testowa czekoladka"
        },
        new Czekoladka {
          id = 2,
          nazwa = "Testowa 2",
          czekolada = Czekoladka.Czekolada.Gorzka,
          masa = 0.53m,
          cena = 15.32m,
          opis = "Kolejna czekoladka testowa"
        }
      );
  }
}
