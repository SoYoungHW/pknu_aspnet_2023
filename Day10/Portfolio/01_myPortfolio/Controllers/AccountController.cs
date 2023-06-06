using _01_myPortfolio.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["succeed"] = "로그아웃";

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Profile(string userName)
        {
            ViewData["NoScroll"] = "true"; // 게시판은 메인스크롤X
            Debug.WriteLine(userName);
            var curUser = await _userManager.FindByNameAsync(userName);

            if (curUser == null)
            {
                TempData["error"] = "사용자가 없습니다";
                return RedirectToAction("Index", "Home");
            }

            var model = new RegisterModel()
            {
                UserId = curUser.UserName,
                Email = curUser.Email,
                PhoneNumber = curUser.PhoneNumber,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Profile(RegisterModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserId); // new가 아님

            user.UserName = model.UserId;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password); // 비밀번호 변경(어렵다..!)
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {

                await _signInManager.SignInAsync(user, isPersistent: false);

                TempData["succeed"] = "프로필변경 성공했습니다.";
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(model); // 프로필 변경을 실패하면 그 화면 그대로 유지
        }

    }
}
