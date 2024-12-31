using Kafe.BL.Manager.Abstract;
using Kafe.Entities.Entity.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AspNetCoreHero.ToastNotification.Abstractions;
using Kafe.MVC.Models.VMs;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Kafe.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IManager<MyUser> _userManager;
        private readonly INotyfService _notyfService;

        public AccountController(IManager<MyUser> userManager, INotyfService notyfService)
        {
            _userManager = userManager;
            _notyfService = notyfService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            LoginVM loginVM = new LoginVM();
            return View(loginVM);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var user = _userManager.GetAll().FirstOrDefault(p => p.Password == loginVM.Password);

            if (user == null)
            {
                _notyfService.Error("Password Hatali.");
                return View(loginVM);
            }

            // Cookie üzerinde tutulacak bilgileri tanımlıyoruz.
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Name)
            };
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userClaimPrincipal = new ClaimsPrincipal(claimIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userClaimPrincipal);

            return RedirectToAction("Index", "Table");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            RegisterVM register = new RegisterVM();
            return View(register);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                _notyfService.Error("Düzeltilmesi gereken yerler var");
                return View(registerVM);
            }

            MyUser user = new MyUser
            {
                Name = registerVM.Name,
                Password = registerVM.Password
            };

            _userManager.Add(user);

            _notyfService.Success("İşlem Başarılı");

            // Kayıt başarılı olduğunda login sayfasına yönlendirme
            return RedirectToAction("Login");
        }
    }
}