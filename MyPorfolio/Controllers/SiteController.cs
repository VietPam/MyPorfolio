using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPorfolio.Models.Context;
using MyPorfolio.Models.Entities;

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
    [HttpPost]
    public IActionResult Contact(Contact contact)
    {
        db.Contacts.Add(contact);
        db.SaveChanges();
        TempData["Message"] = "Form submission successful!";
        return Redirect("~/site/index#contact");
    }
}
