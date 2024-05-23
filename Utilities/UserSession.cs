using Candy_Shop.Models;

namespace Candy_Shop.Utilities; 

public static class UserSession {
  public static bool isLoggedIn(ISession session) {
    return session.GetString("username") is not null;
  }

  public static bool isAdmin(ISession session) {
    return isLoggedIn(session) && session.GetString("type") == User.Type.Admin.ToString();
  }

  public static int GetUserType(ISession session) {
    return isLoggedIn(session) ? (isAdmin(session) ? (int) User.Type.Admin : (int) User.Type.User) : -1;
  }
}
