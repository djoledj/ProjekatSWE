namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class ZaposleniController : ControllerBase
{
  public DjoleGrandContext Context { get; set; }

  public ZaposleniController(DjoleGrandContext context)
  {
    Context = context;
  }

  [HttpPost("/film/{zaposleniID}")]
  public async Task<IActionResult> AddFilm([FromBody] Film film, int zaposleniID)
  {
    try
    {
      var zaposleni = await Context!.Zaposleni!.FirstOrDefaultAsync(p => p.ID == zaposleniID);

      if (zaposleni == null)
      {
        return Unauthorized("Ne postoji zaposleni sa tim ID");
      }

      await Context!.Filmovi!.AddAsync(film);
      await Context.SaveChangesAsync();
      return Ok("Uspesno dodat film " + film.ID);
    }
    catch (Exception ec)
    {
      return BadRequest(ec.Message);
    }
  }

  



}