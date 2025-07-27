using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PhysioWeb.Models;
using PhysioWeb.Repository;
using BCrypt.Net;
using Microsoft.Win32;


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
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true,
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);


                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else if (User.UserRole.ToUpper() == "SUPERADMIN")
                {
                    return RedirectToAction("SuperAdminDashboard", "SuperAdmin");
                }
                else if (User.UserRole.ToUpper() == "AGENCY")
                {
                    return RedirectToAction("AgencyDashboard", "Agent");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                ViewBag.Message = "Credentials Wont Match";
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
