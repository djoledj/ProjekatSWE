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

  [HttpPost("/projekcija/{zaposleniID}/{filmID}/{salaID}")]
  public async Task<IActionResult> AddProjekcija([FromBody] Projekcija projekcija, int zaposleniID, int filmID, int salaID)
  {
    try
    {
      var zaposleni = await Context.Zaposleni!.FirstOrDefaultAsync(p => p.ID == zaposleniID);
      if (zaposleni == null)
      {
        return Unauthorized("Ne postoji zaposleni sa tim ID");
      }

      var film = await Context.Filmovi!.FirstOrDefaultAsync(p => p.ID == filmID);

      var sala = await Context.Sale!.FirstOrDefaultAsync(p => p.ID == salaID);

      var pr = new Projekcija
      {
        ID = projekcija.ID,
        DatumProjekcije = projekcija.DatumProjekcije,
        Film = film,
        Sala = sala
      };

      await Context.Projekcije!.AddAsync(pr);
      await Context.SaveChangesAsync();
      return Ok($"Uspesno dodata projekcija sa ID: {pr.ID}");
    }
    catch (Exception ec)
    {
      return BadRequest(ec.Message);
    }
  }


  [HttpGet("/projekcija/{filmID}")]
  public async Task<ActionResult> GetProjekcije(int filmID)
  {
    try
    {
      var film = await Context.Filmovi!.Include(p => p.FilmProjekcija!).ThenInclude(p => p.Sala).Select(p => new
      {
        p.ID,
        p.Naziv,
        p.Description,
        p.Reziser,
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