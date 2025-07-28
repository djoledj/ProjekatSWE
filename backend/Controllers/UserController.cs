namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]

public class UserController : ControllerBase
{
  public DjoleGrandContext Context { get; set; }

  public UserController(DjoleGrandContext context)
  {
    Context = context;
  }

  [HttpGet("login/{username}/{password}")]
  public async Task<IActionResult> Login(string username, string password)
  {
    try
    {
      var zaposleni = await new Functions<Zaposleni>().FindUser(username, password, Context.Zaposleni!);

      if (zaposleni != null)
      {
        return Ok(zaposleni);
      }

      var musterija = await new Functions<Musterija>().FindUser(username, password, Context.Musterije!);

      if (musterija != null)
      {
        return Ok(musterija);
      }

      var admin = await new Functions<Admin>().FindUser(username, password, Context.Admini!);

      if (admin != null)
      {
        return Ok(admin);
      }

      return Unauthorized("Wrong email or password");

    }

    catch (Exception ec)
    {
      return BadRequest(ec.Message);
    }

  }




}


