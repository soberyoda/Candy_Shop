using Candy_Shop.Data;
using Candy_Shop.Models;
using Candy_Shop.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Candy_Shop.Controllers; 

public class AuthController : Controller {
  public IActionResult Logout() {
    HttpContext.Session.Clear();

    return RedirectToAction("Login");
  }

  [HttpGet]
  public IActionResult Login() {
    return View();
  }

  [HttpPost]
  public async Task<IActionResult> Login(IFormCollection? form) {
    if (form is null) {
      ViewData["error"] = "Error while proccesing the request";
      return View();
    }

    string username = form["username"].ToString();
    string password = form["password"].ToString();

    if (!ValidateUserInfo(username, password)) {
      ViewData["error"] = "Username and/or password is too short";
      return View();
    }

    var user = await ApiClient.Post<UserDTO?>("/api/auth/", form);

    if (user is null) {
      ViewData["error"] = "Incorrect credentials";
      return View();
    }

    HttpContext.Session.SetString("username", user.username);
    HttpContext.Session.SetString("apiToken", user.apiToken);
    HttpContext.Session.SetString("type", user.type.ToString());
    
    TempData["success"] = "Successfully logged in";
    return RedirectToAction("Index", "Home");
  }
  
  private static bool ValidateUserInfo(string username, string password) {
    return username.Length >= 3 && password.Length >= 3;
  }
}
