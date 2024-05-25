using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Candy_Shop.Models;

public class Czekoladka {
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int id { get; set; }

  [Required]
  [MaxLength(32)]
  public string nazwa { get; set; }

  [Required]
  public Czekolada czekolada { get; set; }

  public Orzechy? orzechy { get; set; }

  [Required]
  [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
  public decimal cena { get; set; }

  [Required]
  [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive mass")]
  public decimal masa { get; set; }
  
  [Required]
  public string opis { get; set; }
  
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