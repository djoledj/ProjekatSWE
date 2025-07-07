namespace WebTemplate.Models;

public class Musterija : User
{
  public Musterija()
  {
    Type = "Musterija";
  }

  public List<Ocena>? DateOcene { get; set; }

  public List<Karta>? MusterijaKarta { get; set; }

}