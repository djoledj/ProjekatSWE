namespace WebTemplate.Models;

public class Sala
{
  [Key]
  public int ID { get; set; }

  public string? Naziv { get; set; }

  public int Kapacitet { get; set; }

  public List<Projekcija>? SalaProjekcija { get; set; }
}