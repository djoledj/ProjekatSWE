using Microsoft.AspNetCore.Identity;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class MusterijaController : ControllerBase
{
  public DjoleGrandContext Context { get; set; }

  public MusterijaController(DjoleGrandContext context)
  {
    Context = context;
  }

  [HttpPost("/musterija")]
  public async Task<IActionResult> AddMusterija([FromBody] Musterija musterija)
  {
    try
    {
      var musterije = Context!.Musterije!.Select(r => new { r.UserName, r.Telefon });
      var zaposleni = Context!.Zaposleni!.Select(p => new { p.UserName, p.Telefon });
      var admin = Context!.Admini!.Select(a => new { a.UserName, a.Telefon });
      var users = musterije.Concat(zaposleni).Concat(admin);

      foreach (var u in users)
      {
        if (u.UserName == musterija.UserName)
        {
          return BadRequest("Username je zauzet");
        }

        if (u.Telefon == musterija.Telefon)
        {
          return BadRequest("Vec je prijavljen korisnik sa tim brojem telefona");
        }
      }

      musterija.Password = new PasswordHasher<Musterija>().HashPassword(musterija, musterija.Password!);

      await Context.Musterije!.AddAsync(musterija);
      await Context.SaveChangesAsync();
      return Ok("Uspesno napravljen nalog");
    }
    catch (Exception ec)
    {
      return BadRequest(ec.Message);
    }
  }




}