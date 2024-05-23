using Candy_Shop.Data;
using Candy_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace Candy_Shop.Attributes;

public class ApiAuthorized(User.Type type) : ActionFilterAttribute {
  public override async void OnActionExecuting(ActionExecutingContext context) {
    var dbContext = context.HttpContext.RequestServices.GetService(typeof(ApplicationDBContext)) as ApplicationDBContext;

    if (dbContext is null) {
      context.Result = new ObjectResult(null) { StatusCode = 500 };
      return;
    }
    
    if (context.HttpContext.Request.Headers.TryGetValue("x-api-key", out StringValues apiKeys) == false) {
      context.Result = new ObjectResult("Missing 'x-api-key' header") {StatusCode = 401};
      return;
    }

    if (apiKeys.Count != 1) {
      context.Result = new ObjectResult("Missing 'x-api-key' header") {StatusCode = 401};
      return;
    }

    string apiKey = apiKeys[0] ?? "";

    var user = await dbContext.Users.FirstOrDefaultAsync(user => user.apiToken == apiKey);

    if (user is null) {
      context.Result = new ObjectResult(null) { StatusCode = 403 };
      return;
    }

    if ((int)user.type < (int)type) {
      context.Result = new ObjectResult(null) { StatusCode = 403 };
      return;
    }

    base.OnActionExecuting(context);
  }
}
