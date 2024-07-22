using InfoPortalWeb.Services;
using Microsoft.AspNetCore.Mvc;
using InfoPortalWeb.Models;


namespace InfoPortalWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly DBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminController(DBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            string adminSession = HttpContext.Session.GetString("admin_session");
            if (adminSession != null)
            {
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var admin = _context.Admins.FirstOrDefault(x => x.Email == email);
            if (admin != null && admin.Password == password)
            {
                HttpContext.Session.SetString("admin_session", admin.ID.ToString());
                return RedirectToAction("Index");
            }
            else
            {
                return Content("Incorrect username or password for admin");
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("admin_session");
            return RedirectToAction("Login");
        }
        public IActionResult Profile()
        {
            var adminID = HttpContext.Session.GetString("admin_session");
            if (adminID == null)
                return RedirectToAction("Login");
            var temp = _context.Admins.FirstOrDefault(x => x.ID == int.Parse(adminID));
            if (temp == null)
                return NotFound();
            return View(temp);
        }
        [HttpPost]
        public IActionResult Profile(Admin admin)
        {
            _context.Admins.Update(admin);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult FetchUser()
        {
            var users = _context.Users.ToList();
            return View(users);
        }
        public IActionResult UserDetails(int ID)
        {
            var user = _context.Users.FirstOrDefault(x => x.ID == ID);
            if (user == null)
                return NotFound();
            return View(user);
        }
        public IActionResult UpdateUser(int ID)
        {
            var user = _context.Users.FirstOrDefault(u => u.ID == ID);
            if (user == null)
                return NotFound();
            return View(user);
        }
        [HttpPost]
        public IActionResult UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToAction("FetchUser");
        }
        public IActionResult DeleteUser(int ID)
        {
            var user = _context.Users.FirstOrDefault(x => x.ID == ID);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            return RedirectToAction("FetchUser");
        }
        public IActionResult FetchFeedbackInfo()
        {
            return View(_context.Feedbacks.ToList());
        }
        public IActionResult AddContent(Content content)
        {
            if (content != null)
            {
                _context.Contents.Add(content);
                _context.SaveChanges();
                return RedirectToAction("FetchContent");

            }
            else
                return View();
        }
        public IActionResult UpdateContent(int ID)
        {
            var content = _context.Contents.FirstOrDefault(c => c.ID == ID);
            return View(content);
        }
        [HttpPost]
        public IActionResult UpdateContent(Content content)
        {
            if (content != null)
            {
                _context.Contents.Update(content);
                _context.SaveChanges();
                return RedirectToAction("FetchContent");
            }
            else { return View(content); }

        }
        public IActionResult DeleteContent(int ID)
        {
            var content = _context.Contents.FirstOrDefault(x => x.ID == ID);
            if (content != null)
            {
                _context.Contents.Remove(content);
                _context.SaveChanges();
                return RedirectToAction("FetchContent");
            }
            else
                return View();
        }
        public IActionResult FetchFAQ()
        {
           return View (_context.FAQs.ToList());
        }
        public IActionResult AddFAQ()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddFAQ(FAQ faq)
        {
            if (faq != null)
            {
                _context.FAQs.Add(faq);
                _context.SaveChanges();
                return RedirectToAction("FetchFAQ");
            }
            else
                return View(faq);
        }
        public IActionResult DeleteFAQ(int ID)
        {
            var faqTemp = _context.FAQs.FirstOrDefault(x => x.ID == ID);
            if (faqTemp != null)
            {
                _context.FAQs.Remove(faqTemp);
                _context.SaveChanges();

            }
            return RedirectToAction("FetchFAQ");
        }
        public IActionResult FetchContent()
        {
            return View(_context.Contents.ToList());
        }

    }
}
