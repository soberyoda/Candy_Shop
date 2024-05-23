using Candy_Shop.Models;
using Candy_Shop.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing.Template;

namespace Candy_Shop.Attributes;

public class Authorized
  (User.Type type, string? redirectAction = null, string? redirectController = null) : ActionFilterAttribute {
  public override void OnActionExecuting(ActionExecutingContext context) {
    var contextController = context.Controller as Controller;

    if (contextController is null) {
      base.OnActionExecuting(context);
      return;
    }

    int userType = UserSession.GetUserType(context.HttpContext.Session);
    Console.WriteLine(userType);
    Console.WriteLine((int)type);

    if (userType < (int)type) {
      string action = redirectAction ?? "Index";
      string? controller = redirectController ?? (redirectAction is not null ? null : "Home");

      contextController.TempData["error"] = "Insufficient permissions to access this resource";
      context.Result = new RedirectToActionResult(action, controller, null);
    }

    base.OnActionExecuting(context);
  }
}
