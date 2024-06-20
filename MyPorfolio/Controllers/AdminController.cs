using Microsoft.AspNetCore.Mvc;

namespace MyPorfolio.Controllers;

public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
