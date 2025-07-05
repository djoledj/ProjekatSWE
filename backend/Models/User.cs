namespace WebTemplate.Models;

public class User
{
  [Key]
  public int ID { get; set; }

  public string? UserName { get; set; }

  public string? Password { get; set; }

  public char? Pol { get; set; }

  public string? Ime { get; set; }

  public string? Prezime { get; set; }

  public string? Telefon { get; set; }

  public string? Drzava { get; set; }

  public string? Grad { get; set; }

  public string? PostanskiBroj { get; set; }

  public string? Type { get; protected set; }
}