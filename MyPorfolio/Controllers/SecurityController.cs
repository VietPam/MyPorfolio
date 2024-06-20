using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPorfolio.Models.Context;
using MyPorfolio.Models.Entities;
using System.Security.Claims;

namespace MyPorfolio.Controllers;

[AllowAnonymous]
public class SecurityController : Controller
{
    MyPorfolioContext db = new MyPorfolioContext();
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult login(Admin admin)
    {
        Admin? data = db.Admins.FirstOrDefault(x => x.UserName == admin.UserName && x.Password == admin.Password);
        if (data != null)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,admin.UserName)
            };
            ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "Security");
            ClaimsPrincipal principle = new ClaimsPrincipal(userIdentity);
            HttpContext.SignInAsync(principle);

            return RedirectToAction("Index", "Admin");
        }

        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
}
