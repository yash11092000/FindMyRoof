using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PhysioWeb.Models;
using PhysioWeb.Repository;

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
        public async Task<IActionResult> Login(string Username, string Password, string Email, string returnUrl)
        {
            var User = await _userRepository.Login(Username, Password, Email);
            if (User != null) // dummy check
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, User.UserName),
                new Claim(ClaimTypes.Role, User.UserRole), // Optional, for role-based later
                           };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // Remember across browser sessions
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                // Redirect to original page if returnUrl is safe
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                // Default redirect
                return RedirectToAction("Index", "Dashboard");

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
    }
}
