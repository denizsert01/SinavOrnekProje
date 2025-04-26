using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrnekProje.Models.Entities;
using OrnekProje.Models.ViewModels;
using System.Threading.Tasks;

namespace OrnekProje.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Register_VM model)
        {
            if (!ModelState.IsValid) return View(model);

            // Kullanıcı adı daha önce alınmış mı kontrol et
            var existingUser = await _userManager.FindByNameAsync(model.UserName);
            if (existingUser != null)
            {
                ModelState.AddModelError("Hata", "Bu kullanıcı adı daha önce alınmış");
                return View(model);
            }

            // E-posta daha önce alınmış mı kontrol et
            var existingEmail = await _userManager.FindByEmailAsync(model.Email);
            if (existingEmail != null)
            {
                ModelState.AddModelError("Hata", "Bu e-posta adresi daha önce alınmış");
                return View(model);
            }

            // Şifreyi burada vermiyoruz çünkü o hashlenerek gönderilecek
            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("Hata", error.Description);
            }


            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login_VM model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                // Giriş başarılıysa kitap listesine yönlendir
                return RedirectToAction("Index", "Book");
            }

            // !Result.Succeeded
            ModelState.AddModelError("Hata", "Kullanıcı adı veya şifre hatalı");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
