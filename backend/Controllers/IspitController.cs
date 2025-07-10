namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class SalaController : ControllerBase
{
    public DjoleGrandContext Context { get; set; }

    public SalaController(DjoleGrandContext context)
    {
        Context = context;
    }

    [HttpPost("/sala")]
    public async Task<IActionResult> AddSala([FromBody] Sala sala)
    {
        try
        {
            await Context!.Sale!.AddAsync(sala);
            await Context.SaveChangesAsync();
            return Ok("Uspesno dodat fim sa ID:" + sala.ID);
        }
        catch (Exception ec)
        {
            return BadRequest(ec.Message);
        }
    }

}
