using AutoMapper;
using System.Net;
using System.Web.Mvc;
using ViewModel;
using VS2247A6.Controllers;

namespace Controllers
{
    public class ActorController : Controller
    {
        private readonly Manager m = new Manager();

        // GET: Actor
        public ActionResult Index()
        {
            return View(m.ActorsGetAll());
        }

        // GET: Actor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var actor = m.ActorGetById(id.Value);
            if (actor == null)
                return HttpNotFound();

            return View(actor);
        }

        // GET: Actor/Create
        [Authorize(Roles = "Executive")]
        public ActionResult Create()
        {
            return View(new ActorAddFormViewModel());
        }

        // POST: Actor/Create
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Executive")]
        public ActionResult Create(ActorAddViewModel actor)
        {
            var actorFormViewModel = new ActorAddFormViewModel
            {
                Name = actor.Name,
                AlternateName = actor.AlternateName,
                BirthDate = actor.BirthDate,
                Height = actor.Height,
                ImageUrl = actor.ImageUrl,
                Biography = actor.Biography,
            };

            if (!ModelState.IsValid)
                return View(actorFormViewModel);

            try
            {
                var actorId = m.ActorAdd(actor, User.Identity.Name);
                return RedirectToAction("Details", new { id = actorId });
            }
            catch
            {
                return View(actorFormViewModel);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
