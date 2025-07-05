namespace WebTemplate.Models;

public class Ocena
{
  [Key]
  public int ID { get; set; }

  [Range(1, 5)]
  public int Vrednost { get; set; }

  public string? Komentar { get; set; }

  public DateTime VremeKomentara { get; set; } = DateTime.Now;

  public Musterija? Musterija { get; set; }

  public Film? Film { get; set; }

}