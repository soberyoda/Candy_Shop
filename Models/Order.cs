using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Candy_Shop.Models;

public class Order {
  [Key]
  [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
  public int id { get; set; }
  [Required]
  [ForeignKey("User")]
  public string username { get; set; }

  [Required]
  [ForeignKey("Zawartosc")]
  [Display(Name = "Zawartosc")]
  public int id_zawartosci { get; set; }
  
  public User? User { get; set; }
  
  public Zawartosc Zawartosc { get; set; }

}
