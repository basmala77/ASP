using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CRUD_Operations.Controllers;

[ApiController]
[Route("[controller]")]  
public class ConfigController : ControllerBase
{
    private readonly IConfiguration _configuration;
    //private readonly IOptions<AttachmentOptions> Options;
    //private readonly IOptionsSnapshot<AttachmentOptions> _optionsSnapshot;
    private readonly IOptionsMonitor<AttachmentOptions> _optionsSnapshot;
    public ConfigController(IConfiguration configuration,IOptionsMonitor<AttachmentOptions> options)
    {
        _configuration = configuration;
        //Options = options;
        //_optionsSnapshot = options;
        _optionsSnapshot = options;
        //var v = options.Value;
    }

    [HttpGet]
    public IActionResult Get()
    {
        Thread.Sleep(10000);
        var c = new
        {
            AllowedHosts = _configuration["AllowedHosts"],
            DefultConnection = _configuration["ConnectionStrings:DefaultConnection"],
            TestKey= _configuration["TestKey"],
            SingingKey = _configuration["SigningKey"],
            //AttachmentsOptions = Options.Value
            AttachmentsOptions = _optionsSnapshot.CurrentValue
        };
        return Ok(c);
    }
}

