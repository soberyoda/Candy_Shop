using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Candy_Shop.Attributes; 

public class KeepMessages : ActionFilterAttribute {
  public override void OnActionExecuting(ActionExecutingContext filterContext) {
    if (filterContext.Controller is not Controller controller) {
      base.OnActionExecuting(filterContext);
      return;
    }

    if (controller.TempData.TryGetValue("error", out object? message)) {
      controller.ViewData["error"] = message;
    }

    if (controller.TempData.TryGetValue("success", out message)) {
      controller.ViewData["success"] = message;
    }
    
    base.OnActionExecuting(filterContext);
  }
}