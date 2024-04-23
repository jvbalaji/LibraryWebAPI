using LibraryWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Catalog()
        {
            return RedirectToAction("ShowCatalog", "Book");
        }

        public ActionResult Borrow()
        {
            return RedirectToAction("Borrow", "Book");
        }

        public ActionResult Return()
        {
            return RedirectToAction("Return", "Book");
        }

        public ActionResult Add()
        {
            return RedirectToAction("Add", "Book");
        }
    }
}
