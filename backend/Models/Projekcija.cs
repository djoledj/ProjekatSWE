namespace WebTemplate.Models;

public class Projekcija
{
  [Key]
  public int ID { get; set; }

  public Film? Film { get; set; }

  public Sala? Sala { get; set; }

  public DateTime DatumProjekcije { get; set; }

  public List<Karta>? KarteProjekcija { get; set; }
}