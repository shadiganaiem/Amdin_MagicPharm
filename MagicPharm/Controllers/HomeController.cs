using MagicPharm.Models;
using System.Linq;
using System.Web.Mvc;

namespace MagicPharm.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context = null;
        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult HomePage() => View("Index");

        public ActionResult Login() => View();

        [HttpPost]
        public ActionResult Login(User user)
        {
            var matchUser = _context.Users.FirstOrDefault(x => x.Username.Equals(user.Username) && x.Password.Equals(user.Password) && x.StatusId == 5);
            if (matchUser != null)
                return RedirectToAction("HomePage", "Home");
            return View();
        }

    }
}