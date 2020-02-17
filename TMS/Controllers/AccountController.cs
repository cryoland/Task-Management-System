using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TMS.ViewModels;
using TMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TMS.Services;

namespace TMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly IRepositoryHandler<Employees> repositoryHandler;
        public AccountController(IRepositoryHandler<Employees> handler)
        {
            repositoryHandler = handler;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {              
                Employees user = await repositoryHandler.GetFirstEntityAsync(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                Employees user = await repositoryHandler.GetFirstEntityAsync(u => u.Email == model.Email);
                if (user == null)
                {
                    // добавляем пользователя в бд
                    var employee = new Employees
                    {
                        Email = model.Email,
                        Password = model.Password,
                        ShortName = model.ShortName,
                        FullName = model.FullName
                    };

                    repositoryHandler.Create(employee);

                    //await Authenticate(model.FullName); // аутентификация

                    return RedirectToAction("Login");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(Employees user)
        {
            // создаем один claim
            var claims = new List<Claim> 
            { 
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.FullName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name.ToString())
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}