using Candy_Shop.Models;
using Candy_Shop.Utilities;
using Microsoft.EntityFrameworkCore;
using User = Candy_Shop.Models.User;

namespace Candy_Shop.Data;

public class ApplicationDBContext : DbContext
{
  public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

  public DbSet<Czekoladka> Czekoladki { get; set; }
  public DbSet<Zawartosc> Zawartosc { get; set; }
  public DbSet<User> Users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
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
        new Czekoladka
        {
          id = 1,
          nazwa = "Testowa",
          czekolada = Czekoladka.Czekolada.Mleczna,
          orzechy = Czekoladka.Orzechy.laskowe,
          masa = 0.53m,
          cena = 15.32m,
          opis = "Testowa czekoladka"
        },
        new Czekoladka
        {
          id = 2,
          nazwa = "Testowa 2",
          czekolada = Czekoladka.Czekolada.Gorzka,
          masa = 0.53m,
          cena = 15.32m,
          opis = "Kolejna czekoladka testowa"
        }
      );

    modelBuilder
      .Entity<User>()
      .HasData(
        new User
        {
          username = "admin",
          password = Crypto.ToMd5("admin"),
          apiToken = Crypto.SeededGuid(123),
          type = User.Type.Admin
        }
      );
  }
}

