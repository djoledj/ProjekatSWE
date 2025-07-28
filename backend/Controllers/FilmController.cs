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


}