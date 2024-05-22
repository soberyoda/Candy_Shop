using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Candy_Shop.Models;

public class Czekoladka {
  [Key]
  [DatabaseGenerated((DatabaseGeneratedOption.Identity))]
  public int id { get; set; }
  
  [Required]
  [MaxLength(32)]
  public string nazwa { get; set; }
  
  public Czekolada czekolada { get; set; }
  public Orzechy? orzechy { get; set; }
  public string opis { get; set; }
  public decimal cena { get; set; }
  public decimal masa { get; set; }
  
  public ICollection<Zawartosc> zawartosci { get; set; }
  
  public enum Czekolada {
    [Description("mleczna")]
    Mleczna = 1,
    
    [Description("gorzka")]
    Gorzka = 2
  }

  public enum Orzechy {
    [Description("laskowe")]
    laskowe = 1,
    
    [Description("migdały")]
    migdały = 2
  }
}