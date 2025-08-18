using Microsoft.AspNetCore.Identity;

namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    public DjoleGrandContext Context { get; set; }

    public AdminController(DjoleGrandContext context)
    {
        Context = context;
    }



    [HttpPost("/sala/{adminID}")]
    public async Task<IActionResult> AddSala([FromBody] Sala sala, int adminID)
    {
        try
        {
            var admin = await Context!.Admini!.FirstOrDefaultAsync(p => p.ID == adminID);
            if (admin == null)
            {
                return Unauthorized("Ne postoji admin sa tim ID");
            }

            await Context!.Sale!.AddAsync(sala);
            await Context.SaveChangesAsync();
            return Ok("Uspesno dodata sala sa ID:" + sala.ID);
        }
        catch (Exception ec)
        {
            return BadRequest(ec.Message);
        }
    }

    [HttpPost("/zaposleni/{adminID}")]
    public async Task<IActionResult> AddZaposleni([FromBody] Zaposleni zaposleni, int adminID)
    {
        try
        {
            var admin = await Context!.Admini!.FirstOrDefaultAsync(p => p.ID == adminID);
            if (admin == null)
            {
                return Unauthorized("Ne postoji admin sa tim ID");
            }

            zaposleni.Password = new PasswordHasher<Zaposleni>().HashPassword(zaposleni, zaposleni.Password!);

            await Context!.Zaposleni!.AddAsync(zaposleni);
            await Context.SaveChangesAsync();
            return Ok("Uspesno dodat zaposleni sa ID:" + zaposleni.ID);
        }
        catch (Exception ec)
        {
            return BadRequest(ec.Message);
        }
    }

    [HttpDelete("brisiKomentar/{adminID}/{komentarID}")]
    public async Task<IActionResult> DeleteKomentar(int adminID, int komentarID)
    {
        try
        {
            var admin = await Context!.Admini!.FirstOrDefaultAsync(p => p.ID == adminID);
            if (admin == null)
            {
                return Unauthorized("Ne postoji admin sa tim ID");
            }
            var komentar = await Context!.Ocene!.FirstOrDefaultAsync(p => p.ID == komentarID);

            if (komentar == null)
            {
                return BadRequest("Ne postoji komentar sa tim ID");
            }

            Context!.Ocene!.Remove(komentar);
            await Context.SaveChangesAsync();
            return Ok("Uspesno izbrisan komentar");
        }
        catch (Exception ec)
        {
            return BadRequest(ec.Message);
        }
    }

    [HttpGet("/komentari")]
    public async Task<IActionResult> GetComments()
    {
        try
        {
            var komentari = await Context.Ocene!.OrderByDescending(x => x.VremeKomentara).Select(x => new
            {
                x.ID,
                x.Komentar
            }).ToListAsync();

            return Ok(komentari);
        }
        catch (Exception ec)
        {
            return BadRequest(ec.Message);
        }
    }

    [HttpGet("/zaposleni/{adminID}")]
    public async Task<IActionResult> GetZaposleni(int adminID)
    {
        try
        {
            var admin = await Context!.Admini!.FirstOrDefaultAsync(p => p.ID == adminID);
            if (admin == null)
            {
                return Unauthorized("Ne postoji admin sa tim ID");
            }

            return Ok(Context.Zaposleni);
        }
        catch (Exception ec)
        {
            return BadRequest(ec.Message);
        }
    }

    [HttpDelete("zaposleniDelete/{adminID}/{zaposleniID}")]
    public async Task<IActionResult> DeleteZaposleni(int adminID, int zaposleniID)
    {
        try
        {
            var admin = await Context!.Admini!.FirstOrDefaultAsync(p => p.ID == adminID);
            if (admin == null)
            {
                return Unauthorized("Ne postoji admin sa tim ID");
            }
            var zap = await Context!.Zaposleni!.FirstOrDefaultAsync(p => p.ID == zaposleniID);

            if (zap == null)
            {
                return BadRequest("Ne postoji zaposleni sa tim ID");
            }

            Context!.Zaposleni!.Remove(zap);
            await Context.SaveChangesAsync();
            return Ok("Uspesno izbrisan zaposleni");
        }
        catch (Exception ec)
        {
            return BadRequest(ec.Message);
        }
    }

    [HttpPost("/admin")]
    public async Task<IActionResult> AddAdmin([FromBody] Admin admin)
    {
        try
        {


            admin.Password = new PasswordHasher<Admin>().HashPassword(admin, admin.Password!);

            await Context!.Admini!.AddAsync(admin);
            await Context.SaveChangesAsync();
            return Ok("Uspesno dodat zaposleni sa ID:" + admin.ID);
        }
        catch (Exception ec)
        {
            return BadRequest(ec.Message);
        }
    }
}
