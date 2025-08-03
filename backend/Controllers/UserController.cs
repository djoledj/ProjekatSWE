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

  [HttpPost("/login")]
  public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
  {
    try
    {
      var zaposleni = await new Functions<Zaposleni>().FindUser(loginDTO.UserName!, loginDTO.Password!, Context.Zaposleni!);

      if (zaposleni != null)
      {
        return Ok(zaposleni);
      }

      var musterija = await new Functions<Musterija>().FindUser(loginDTO.UserName!, loginDTO.Password!, Context.Musterije!);

      if (musterija != null)
      {
        return Ok(musterija);
      }

      var admin = await new Functions<Admin>().FindUser(loginDTO.UserName!, loginDTO.Password!, Context.Admini!);

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


