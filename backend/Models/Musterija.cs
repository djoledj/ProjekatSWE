namespace WebTemplate.Models;

public class Musterija : User
{
  public Musterija()
  {
    Type = "Musterija";
  }

  public List<Ocena>? MusterijaOcena { get; set; }

  public List<Karta>? MusterijaKarta { get; set; }

}