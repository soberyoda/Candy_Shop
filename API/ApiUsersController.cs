using Candy_Shop.Attributes;
using Candy_Shop.Data;
using Candy_Shop.Models;
using Candy_Shop.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using User = Candy_Shop.Models.User;

namespace Candy_Shop.API;

[Route("api/users/")]
[ApiAuthorized(Models.User.Type.Admin)]
public class ApiUsersController : ControllerBase {
  private readonly ApplicationDBContext context;

  public ApiUsersController(ApplicationDBContext context) {
    this.context = context;
  }

  // GET: /api/users/
  [HttpGet]
  public async Task<IResult> Get() {
    return Results.Ok(
      (await context.Users.ToListAsync()).Select(user => new UserDTO(user))
    );
  }
  
  // POST: /api/users/
  [HttpPost]
  public async Task<IResult> Post(IFormCollection? form) {
    if (form is null) {
      return Results.BadRequest();
    }

    string username = form["username"].ToString();
    string password = form["password"].ToString();
    User.Type type = (User.Type)Enum.Parse(typeof(User.Type), form["type"]);

    User user = new() {
      username = username, 
      password = Crypto.ToMd5(password), 
      apiToken = Guid.NewGuid().ToString(),
      type = type
    };
    
    context.Users.Add(user);

    await context.SaveChangesAsync();

    return Results.Created($"/api/users/{user.username}", new UserDTO(user));
  }
}
