using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using PhysioWeb.Models;
using PhysioWeb.Repository;
using System.Diagnostics;
using System.Reflection;
using System.Security.Claims;


namespace PhysioWeb.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //New Changes Added By Group

      

        #region login
        [HttpGet]
        public async Task<ActionResult> Login(string returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl ?? Url.Content("~/");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Email, string Mobile, string Password, string returnUrl)
        {

            var User = await _userRepository.Login(Email, Mobile, Password);

            if (User != null && BCrypt.Net.BCrypt.Verify(Password, User.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, User.UserName),
                    new Claim(ClaimTypes.Role, User.UserRole),
                    new Claim(ClaimTypes.PrimarySid, Convert.ToString(User.UserId)),
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                var role = User.UserRole?.Trim().ToUpper();

                // ✅ If it's AJAX, return JSON instead of Redirect
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    string redirectUrl = role switch
                    {
                        "SUPERADMIN" => "/SuperAdmin/AdminDashboard",
                        "AGENCY" => "/Agent/AgencyDashboard",
                        _ => "/Home/Index"
                    };
                    return Json(new { success = true, redirect = redirectUrl });
                }

                // ✅ Normal form login
                if (role == "SUPERADMIN") return Redirect("/SuperAdmin/AdminDashboard");
                if (role == "AGENCY") return RedirectToAction("AgencyDashboard", "Agent");
                return RedirectToAction("Index", "Home");
            }

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = false, message = "Invalid credentials." });
            }

            ViewBag.Message = "Invalid credentials.";
            return View();
        }


        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Register
        [HttpGet]
        public async Task<ActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(Register register)
        {
            if (register != null)
            {
                string hashed = BCrypt.Net.BCrypt.HashPassword(register.Password);
                register.Password = hashed;
                var result = await _userRepository.RegisterUser(register);
                return Json(result);
            }
            return View();
        }
        #endregion
    }
}
