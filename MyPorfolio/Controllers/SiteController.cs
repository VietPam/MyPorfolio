using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPorfolio.Models.Context;

namespace MyPorfolio.Controllers;

[AllowAnonymous]
public class SiteController : Controller
{
    MyPorfolioContext db = new MyPorfolioContext();
    public IActionResult Index()
    {
        ViewBag.about = db.Abouts.OrderByDescending(x => x.TextDate).ToList();
        return View();
    }
}
