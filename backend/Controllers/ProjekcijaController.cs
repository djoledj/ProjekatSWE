using System.ComponentModel;
using System.Threading.Tasks;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjekcijaController : ControllerBase
{
  public DjoleGrandContext Context { get; set; }

  public ProjekcijaController(DjoleGrandContext context)
  {
    Context = context;
  }

  [HttpGet("/projekcija/{filmID}")]
  public async Task<ActionResult> GetProjekcije(int filmID)
  {
    try
    {
      var film = await Context.Filmovi!.Include(p => p.FilmProjekcija!).ThenInclude(p => p.Sala).Select(p => new
      {
        ID = p.ID,
        Naziv = p.Naziv,
        Description = p.Description,
        Reziser = p.Reziser,
        Projekcije = p.FilmProjekcija!.Select(q => new
        {
          DatumiProjekcije = q.DatumProjekcije,
          Sale = q.Sala!.Naziv
        })

      }).Where(p => p.ID == filmID).ToListAsync();


      return Ok(film);
    }
    catch (Exception ec)
    {
      return BadRequest(ec.Message);
    }
  }
}