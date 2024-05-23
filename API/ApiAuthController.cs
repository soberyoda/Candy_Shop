using Candy_Shop.Data;
using Candy_Shop.Models;
using Candy_Shop.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Candy_Shop.API;

[Route("api/auth/")]
public class ApiAuthController(ApplicationDBContext context) : ControllerBase {
  // POST: /api/auth/
  [HttpPost]
  public async Task<IResult> Post(IFormCollection? form) {
    if (form is null) {
      return Results.BadRequest();
    }

    string username = form["username"].ToString();
    string password = form["password"].ToString();

    string hashedPassword = Crypto.ToMd5(password);

    var user = await context.Users.FirstOrDefaultAsync(user => user.username == username && user.password == hashedPassword);

    return user is null ? Results.BadRequest() : Results.Ok(new UserDTO(user));
  }
}
