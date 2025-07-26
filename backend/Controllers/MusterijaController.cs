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

  [HttpPost("/karta/{projekcijaID}/{userID}")]
  public async Task<IActionResult> AddKarta(int projekcijaID, int userID)
  {
    try
    {
      var user = await Context.Musterije!.FirstOrDefaultAsync(p => p.ID == userID);

      if (user == null)
      {
        return BadRequest("Ne postoji korisnik sa tim ID");
      }

      var projekcija = await Context.Projekcije!.FirstOrDefaultAsync(p => p.ID == projekcijaID);

      if (projekcija == null)
      {
        return BadRequest("Ne postoji projekcija sa tim ID");
      }

      var k = new Karta
      {
        Projekcija = projekcija,
        Musterija = user
      };

      await Context.Karte!.AddAsync(k);
      await Context.SaveChangesAsync();
      return Ok("Uspesno obavljena rezervacija");


    }
    catch (Exception ec)
    {
      return BadRequest(ec.Message);
    }


  }

  [HttpPost("/komentar/{musterijaID}/{filmID}")]
  public async Task<IActionResult> AddKomentar([FromBody] Ocena ocena, int musterijaID, int filmID)
  {
    try
    {
      var user = await Context.Musterije!.FirstOrDefaultAsync(p => p.ID == musterijaID);

      if (user == null)
      {
        return BadRequest("Ne postoji korisnik sa tim ID");
      }

      var film = await Context.Filmovi!.FirstOrDefaultAsync(p => p.ID == filmID);\

      if (film == null)
      {
        return BadRequest("Ne postoji film sa tim ID");
      }

      var komentar = new Ocena
      {
        Vrednost = ocena.Vrednost,
        Komentar = ocena.Komentar,
        Film = film,
        Musterija = user
      };

      await Context.Ocene!.AddAsync(komentar);
      await Context.SaveChangesAsync();
      return Ok("Uspesno dodata ocena");
    }
    catch (Exception ec)
    {
      return BadRequest(ec.Message);
    }
  }




}