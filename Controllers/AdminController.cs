using Candy_Shop.Attributes;
using Candy_Shop.Models;
using Candy_Shop.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Candy_Shop.Controllers;

public class AdminController : Controller {
  [Authorized(Models.User.Type.Admin)]
  public async Task<IActionResult> ShowUsers() {
    var users = await ApiClient.Get<IEnumerable<UserDTO>>(
      "/api/users/",
      new ApiClient.Options { ApiKey = HttpContext.Session.GetString("apiToken") }
    );

    return View(users);
  }
}
