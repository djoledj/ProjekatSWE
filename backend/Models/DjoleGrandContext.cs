namespace WebTemplate.Models;

public class DjoleGrandContext : DbContext
{
    public DbSet<Admin>? Admini { get; set; }

    public DbSet<Musterija>? Musterije { get; set; }

    public DbSet<Zaposleni>? Zaposleni { get; set; }

    public DbSet<Karta>? Karte { get; set; }

    public DbSet<Sala>? Sale { get; set; }

    public DbSet<Ocena>? Ocene { get; set; }

    public DbSet<Projekcija>? Projekcije { get; set; }

    public DbSet<Film>? Filmovi { get; set; }





    public DjoleGrandContext(DbContextOptions options) : base(options)
    {

    }
}
