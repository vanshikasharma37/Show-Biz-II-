using System.Web.Mvc;
using VS2247A6.Controllers;

namespace Controllers
{
    public class GenreController : Controller
    {
        private readonly Manager m = new Manager();

        // GET: Genre
        public ActionResult Index()
        {
            return View(m.GenresGetAll());
        }
    }
}