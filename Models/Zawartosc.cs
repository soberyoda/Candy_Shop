using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Candy_Shop.Models; 

public class Zawartosc {
  [Key]
  [DatabaseGenerated((DatabaseGeneratedOption.Identity))]
  public int id { get; set; }
  public int sztuk { get; set; }
  
  [ForeignKey("id_czekoladki")]
  public Czekoladka Czekoladka { get; set; }
}