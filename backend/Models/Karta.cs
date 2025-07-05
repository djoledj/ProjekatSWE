namespace WebTemplate.Models;

public class Karta
{
  [Key]
  public int ID { get; set; }

  public Projekcija? Projekcija { get; set; }

  public Musterija? Musterija { get; set; }
}