﻿using Candy_Shop.Attributes;
using Candy_Shop.Data;
using Candy_Shop.Models;
using Candy_Shop.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Candy_Shop.Controllers;

public class AdminController : Controller {
  private readonly ApplicationDBContext _context;

  public AdminController(ApplicationDBContext context) {
    _context = context; 
  }

  [Authorized(Models.User.Type.Admin)]
  public async Task<IActionResult> ShowUsers() {
    var users = await ApiClient.Get<IEnumerable<UserDTO>>(
      "/api/users/",
      new ApiClient.Options { ApiKey = HttpContext.Session.GetString("apiToken") }
    );

    return View(users);
  }

  [HttpPost]
  [Authorized(Models.User.Type.Admin)]
  public async Task<IActionResult> AddUser(IFormCollection form) {
    string username = form["username"];
    string password = form["password"];
    User.Type type = (User.Type)Enum.Parse(typeof(User.Type), form["type"]);

    if (!ValidateUserInfo(username, password)) {
      TempData["error"] = "Invalid user information.";
      return RedirectToAction("ShowUsers");
    }

    var newUser = new UserDTO {
      username = username,
      password = password, // Now it includes password
      type = type
    };

    var response = await ApiClient.Post<UserDTO>("/api/users/", form, new ApiClient.Options { ApiKey = HttpContext.Session.GetString("apiToken") });

    if (response is null) {
      TempData["error"] = "Failed to add user.";
    } else {
      TempData["success"] = "User added successfully.";
    }

    return RedirectToAction("ShowUsers");
  }

  [HttpPost]
  [Authorized(Models.User.Type.Admin)]
  public async Task<IActionResult> DeleteUser(string username) {
    var response = await ApiClient.Delete($"/api/users/{username}", new ApiClient.Options { ApiKey = HttpContext.Session.GetString("apiToken") });

    if (!response) {
      TempData["error"] = "Failed to delete user.";
    } else {
      TempData["success"] = "User deleted successfully.";
    }

    return RedirectToAction("ShowUsers");
  }
  [HttpGet]
  [Authorized(Models.User.Type.Admin)]
  public IActionResult AddChocolate()
  {
    return View();
  }

  [HttpPost]
  [Authorized(Models.User.Type.Admin)]
  public async Task<IActionResult> AddChocolate([FromForm] Czekoladka czekoladka)
  {
    if (!ModelState.IsValid)
    {
      var errors = ModelState.Values.SelectMany(v => v.Errors);
      foreach (var error in errors)
      {
        Console.WriteLine(error.ErrorMessage);
      }
      TempData["error"] = "Invalid chocolate information.";
      return View(czekoladka);
    }

    _context.Czekoladki.Add(czekoladka);
    await _context.SaveChangesAsync();
    TempData["success"] = "Chocolate added successfully.";
    return RedirectToAction("Index", "Czekoladki");
  }


  private static bool ValidateUserInfo(string username, string password) {
    return username.Length >= 3 && password.Length >= 3;
  }
}
