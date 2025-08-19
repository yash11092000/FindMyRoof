using BCrypt.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using PhysioWeb.Models;
using PhysioWeb.Repository;
using BCrypt.Net;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.Security.Claims;


namespace PhysioWeb.Controllers
{
    public class HomeController : Controller
    {

        private readonly IUserRepository _userRepository;
        private readonly IMasterRepository _masterRepository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository, IMasterRepository masterRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _masterRepository = masterRepository;
        }

        public async Task<IActionResult> Index()
        {
            HomeDashboard propertyDetails = await _userRepository.GetDashboardData();

            return View(propertyDetails);
        }

        [HttpGet]
        public async Task<ActionResult> GetPropertiesList()
        {
            HomeDashboard propertyDetails = await _userRepository.GetDashboardData();
            return Json(propertyDetails.PropertyDetails);
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
                    new Claim(ClaimTypes.GroupSid, User.AgencyId),
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


        #region SearchProperty
        public async Task<ActionResult> SearchProperty(string location = "", string PropertyType = "", string RentalType = "", string PropertyCategory = "", string Amenities = "", string MinPrice = "", string MaxPrice = "", int PageNo = 1, int PageSize = 6)
        {
            var result = await _masterRepository.SearchProperties(location, PropertyType, RentalType, PropertyCategory, Amenities, MinPrice, MaxPrice, PageNo, PageSize);
            return View(result);
        }

        public async Task<ActionResult> LoadProperties(int PageNo = 1, int PageSize = 6)
        {
            string location = null;
            string PropertyType = null;
            string RentalType = null;
            string PropertyCategory = null;
            string Amenities = null;
            string MinPrice = null;
            string MaxPrice = null;
            var result = await _masterRepository.SearchProperties(location, PropertyType, RentalType, PropertyCategory, Amenities, MinPrice, MaxPrice, PageNo, PageSize);
            return PartialView("_PropertyListPartial", result);
        }
        #endregion

        [Route("secure-images/{*filePath}")]
        public IActionResult GetSecureImage(string filePath = "")
        {
            // Combine with secure-images directory
            if (filePath != null)
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "secure-images", filePath);

                // Security check - prevent directory traversal
                fullPath = Path.GetFullPath(fullPath);
                if (!fullPath.StartsWith(Path.Combine(Directory.GetCurrentDirectory(), "secure-images")))
                {
                    return BadRequest("Invalid file path");
                }

                if (!System.IO.File.Exists(fullPath))
                    return NotFound();

                // Get proper content type based on file extension
                var contentType = GetContentType(fullPath);

                // Special handling for video files
                if (contentType.StartsWith("video/"))
                {
                    // Enable range requests for video streaming
                    var fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    return File(fileStream, contentType, enableRangeProcessing: true);
                }

                // Standard handling for images
                return PhysicalFile(fullPath, contentType);
            }
            return null;
        }

        private string GetContentType(string path)
        {
            var extension = Path.GetExtension(path).ToLowerInvariant();
            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                ".mp4" => "video/mp4",
                ".webm" => "video/webm",
                ".ogg" => "video/ogg",
                _ => "application/octet-stream"
            };
        }
    }
}
