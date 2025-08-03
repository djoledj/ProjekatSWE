namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class FilmController : ControllerBase
{
  public DjoleGrandContext Context { get; set; }

  public FilmController(DjoleGrandContext context)
  {
    Context = context;
  }

  [HttpGet("/film")]
  public ActionResult GetFilmovi()
  {
    try
    {
      var films = Context.Filmovi!.Select(p => new
      {
        p.ID,
        p.Naziv,
        p.Description,
        p.Reziser,
        p.Zanr,
        p.Slika
      }).ToList();

      return Ok(films);
    }
    catch (Exception ec)
    {
      return BadRequest(ec.Message);
    }

  }

  [HttpDelete("/film/{filmID}")]
  public async Task<IActionResult> Update(int filmID)
  {
    try
    {
      var film = await Context.Filmovi!.FirstOrDefaultAsync(p => p.ID == filmID);

      if (film == null)
      {
        return BadRequest("Ne postoji taj film");
      }



      Context.Filmovi!.Remove(film);
      await Context.SaveChangesAsync();

      return Ok("Uspesno obrisan film");
    }
    catch (Exception ec)
    {
      return BadRequest(ec.Message);
    }
  }


}