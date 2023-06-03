using _01_myPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;

namespace _01_myPortfolio.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewData["NoScroll"] = "true";
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            ModelState.Remove("PhoneNumber");
            ModelState.Remove("Email");

            if (ModelState.IsValid) 
            {
                var user = new IdentityUser()
                {
                    UserName = model.UserId,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email
                    // 패스워드는 별도 대입
                };

                var result = await _userManager.CreateAsync(user, model.Password);
               
                if (result.Succeeded) 
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    TempData["succeed"] = "회원가입 성공했습니다!";
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            ViewData["NoScroll"] = "true";
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserId, model.Password, model.RememberMe, false);
                
                if (result.Succeeded) 
                {
                    TempData["succeed"] = "로그인 성공";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "로그인 실패");
                }
            }   
            
            return View(model);
        }
    }
}
