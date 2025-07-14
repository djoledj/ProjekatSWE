namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class ZaposleniController : ControllerBase
{
  public DjoleGrandContext Context { get; set; }

  public ZaposleniController(DjoleGrandContext context)
  {
    Context = context;
  }
    
  
}