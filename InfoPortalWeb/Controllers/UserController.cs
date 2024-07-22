using InfoPortalWeb.Services;
using InfoPortalWeb.Models;
using Microsoft.AspNetCore.Mvc;


namespace InfoPortalWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly DBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(DBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.checkSession = HttpContext.Session.GetString("user_session");
            var contentList = _context.Contents.ToList();

            return View(contentList);
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email);
            if (user != null && user.Password == password)
            {
                HttpContext.Session.SetString("user_session", user.ID.ToString());
                return RedirectToAction("Index");
            }
            else
            {
                return Content("Incorrect Username or Password");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user_session");
            return RedirectToAction("Index");
        }

        public IActionResult UserProfile()
        {
            ViewBag.checkSession = HttpContext.Session.GetString("user_session");
            var userID = HttpContext.Session.GetString("user_session");
            if (userID == null)
                return RedirectToAction("Login");
            var tempUser = _context.Users.FirstOrDefault(u => u.ID == int.Parse(userID));
            if (tempUser == null)
                return NotFound();
            return View(tempUser);
        }

        [HttpPost]
        public IActionResult UserProfile(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToAction("UserProfile");
        }
        public IActionResult Feedback()
        {
            ViewBag.checkSession = HttpContext.Session.GetString("user_session");
            var userID = HttpContext.Session.GetString("user_session");
            if (userID == null) return RedirectToAction("Login");
            return View();
        }

        [HttpPost]
        public IActionResult Feeback(FeedbackInfo feedback)
        {
            TempData["message"] = "Your Feedback is received";
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
            return RedirectToAction("Feedback");

        }
        public IActionResult FetchFAQ()
        {
            ViewBag.checkSession = HttpContext.Session.GetString("user_session");
            var temp = _context.FAQs.ToList();
            return View(temp);
        }
        public IActionResult FetchContent()
        {
            ViewBag.checkSession = HttpContext.Session.GetString("user_session");
            var temp = _context.Contents.ToList();
            return View(temp);
        }
        public IActionResult Details(int ID)
        {
            var temp = _context.Contents.FirstOrDefault(x => x.ID == ID);
            if (temp != null)
            {
                return View(temp);
            }
            else
                return NotFound();
        }

    }


}
