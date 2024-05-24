using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Candy_Shop.Models;

public class User : IUserDTO {
  [Key] [Required] public string username { get; set; }
  [Required] public string password { get; set; }
  [Required] public string apiToken { get; set; }
  public Type type { get; set; } = Type.User;

  public enum Type {
    [Description("user")] User = 1,

    [Description("admin")] Admin = 2
  }
}

public interface IUserDTO {
  string username { get; set; }
  string apiToken { get; set; }
  User.Type type { get; set; }
}

public class UserDTO : IUserDTO {
  public string username { get; set; }
  public string apiToken { get; set; }

  public User.Type type { get; set; } = User.Type.User;
  
  [JsonIgnore]
  public string password { get; set; }
  
  [JsonConstructor]
  public UserDTO() {}

  public UserDTO(User user) {
    username = user.username;
    apiToken = user.apiToken;
    type = user.type;
  }
}
