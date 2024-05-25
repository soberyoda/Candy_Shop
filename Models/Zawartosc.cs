using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Candy_Shop.Models
{
  public class Zawartosc
  {
    public int id { get; set; }
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Please enter a valid number of pieces.")]
    public int sztuk { get; set; }
    [Required]
    [ForeignKey("Czekoladka")]
    [Display(Name = "Czekoladka")]
    public int id_czekoladki { get; set; }
    public Czekoladka Czekoladka { get; set; }
  }
}
