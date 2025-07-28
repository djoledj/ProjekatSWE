namespace WebTemplate.Models;

public class Film
{
  [Key]
  public int ID { get; set; }

  public required string Naziv { get; set; }

  public string? Description { get; set; }

  public string? Reziser { get; set; }

  public string? Zanr { get; set; }

  public string? Slika { get; set; }

  public List<Projekcija>? FilmProjekcija { get; set; }

  public List<Ocena>? FilmOcena { get; set; }
}