using Microsoft.AspNetCore.Identity;



public class Functions<T> : ControllerBase where T : User
{
  public async Task<T?> FindUser(string userName, string password, DbSet<T> users)
  {
    var user = await users.FirstOrDefaultAsync(u => u.UserName == userName);

    if (user != null)
    {
      var passwordVerification = new PasswordHasher<T>().VerifyHashedPassword(user, user.Password!, password);
      if (passwordVerification == PasswordVerificationResult.Success)
      {
        return user;
      }
    }
    return null;
  }
}