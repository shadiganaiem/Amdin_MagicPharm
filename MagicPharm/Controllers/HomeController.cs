using System.Web.Mvc;

namespace MagicPharm.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index() => View();
    }
}