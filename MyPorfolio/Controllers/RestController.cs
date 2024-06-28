using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace MyPorfolio.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestController : ControllerBase
{
    [HttpGet]
    [Route("hello")]
    public IActionResult hello()
    {
        return Ok("Hello World");
    }
    [HttpGet]
    [Route("getFileLog")]
    public IActionResult getFileLog()
    {
        try
        {
            FileContentResult? result = Program.api_file.loadFileLog();
            if (result == null)
            {
                return NotFound();
            }
            return File(result.FileContents, result.ContentType, result.FileDownloadName);
        }
        catch (Exception ex)
        {
            Log.Error(string.Format("{0}", ex.ToString()));
            return BadRequest("Internal Error");
        }
    }
}
