namespace WebTemplate.Controllers;

[ApiController]
[Route("[controller]")]
public class IspitController : ControllerBase
{
    public DjoleGrandContext Context { get; set; }

    public IspitController(DjoleGrandContext context)
    {
        Context = context;
    }



}
