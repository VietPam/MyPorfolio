using Microsoft.AspNetCore.Mvc;

namespace MyPorfolio.Controllers;

public class SiteController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
