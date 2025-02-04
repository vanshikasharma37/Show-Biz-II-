using Data;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ViewModel;
using VS2247A6.Controllers;

namespace Controllers
{
    public class ShowController : Controller
    {
        private readonly Manager m = new Manager();

        // GET: Show
        public ActionResult Index()
        {
            return View(m.ShowsGetAll());
        }

        // GET: Show/Add
        [Route("Actor/{id}/AddShow")]
        [Authorize(Roles = "Coordinator")]
        public ActionResult Create(int id)
        {
            var actor = m.ActorGetById(id);
            if (actor == null) return HttpNotFound();

            var form = new ShowAddFormViewModel
            {
                ActorId = id,
                ActorName = actor.Name,
                ReleaseDate = DateTime.Now,
                Genres = m.GenresGetAll().Select(g => new SelectListItem
                {
                    Value = g.Name,
                    Text = g.Name,
                    Selected = m.GenresGetAll().FirstOrDefault().Id == g.Id
                }),
                Actors = m.ActorsGetAll().Select(a => new SelectListItem
                {
                    Value = a.Id.ToString(),
                    Text = a.Name,
                    Selected = a.Id == id
                })
            };

            return View(form);
        }

        // POST: Show/Add
        [HttpPost]
        [ValidateInput(false)]
        [Authorize(Roles = "Coordinator")]
        [ValidateAntiForgeryToken]
        [Route("Actor/{id}/AddShow")]
        public ActionResult Create(int id, ShowAddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var actor = m.ActorGetById(id);
                var form = new ShowAddFormViewModel
                {
                    Name = model.Name,
                    ReleaseDate = model.ReleaseDate,
                    Genre = model.Genre,
                    ImageUrl = model.ImageUrl,
                    SelectedActorIds = model.SelectedActorIds,
                    Premise = model.Premise,
                    Genres = m.GenresGetAll().Select(g => new SelectListItem
                    {
                        Value = g.Name,
                        Text = g.Name,
                        Selected = g.Name == model.Genre
                    }),
                    Actors = m.ActorsGetAll().Select(a => new SelectListItem
                    {
                        Value = a.Id.ToString(),
                        Text = a.Name,
                        Selected = model.SelectedActorIds.Contains(a.Id)
                    }),
                    ActorName = actor.Name,
                    ActorId = actor.Id
                };
                return View(form);
            }

            var addedShowId = m.ShowAdd(model, User.Identity.Name);
            if (addedShowId == null) return View(model);

            return RedirectToAction("Details", "Show", new { id = addedShowId });
        }

        // GET: Show/Create
        /*
        [Authorize(Roles = "Coordinator")]
        public ActionResult Create()
        {
            var genres = m.GenresGetAll().ToList();  // Get all genres
            if (!genres.Any())
            {
                TempData["Error"] = "No genres available. Please ask administrator to load genres first.";
                return RedirectToAction("Index");
            }

            var actors = m.ActorsGetAll().ToList();  // Get all actors
            if (!actors.Any())
            {
                TempData["Error"] = "No actors available. Please add actors before creating a show.";
                return RedirectToAction("Index");
            }

            var form = new ShowAddViewModel
            {
                ReleaseDate = DateTime.Now,  // Set default date
                GenreList = new SelectList(genres, "Name", "Name", genres.FirstOrDefault()?.Name),
                ActorList = new MultiSelectList(actors, "Id", "Name")
            };
            return View(form);
        }


        // POST: Show/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Coordinator")]
        public ActionResult Create(ShowAddViewModel show)
        {
            if (!ModelState.IsValid)
            {
                var genres = m.GenresGetAll().ToList();
                var actors = m.ActorsGetAll().ToList();

                show.GenreList = new SelectList(genres, "Name", "Name", show.Genre);
                show.ActorList = new MultiSelectList(actors, "Id", "Name", show.ActorIds);
                return View(show);
            }

            try
            {
                // Ensure at least one actor is selected
                if (show.ActorIds == null || !show.ActorIds.Any())
                {
                    ModelState.AddModelError("ActorIds", "Please select at least one actor.");
                    var genres = m.GenresGetAll().ToList();
                    var actors = m.ActorsGetAll().ToList();
                    show.GenreList = new SelectList(genres, "Name", "Name", show.Genre);
                    show.ActorList = new MultiSelectList(actors, "Id", "Name", show.ActorIds);
                    return View(show);
                }

                var showId = m.ShowAdd(show, User.Identity.Name);
                return RedirectToAction("Details", new { id = showId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error creating show: " + ex.Message);
                var genres = m.GenresGetAll().ToList();
                var actors = m.ActorsGetAll().ToList();
                show.GenreList = new SelectList(genres, "Name", "Name", show.Genre);
                show.ActorList = new MultiSelectList(actors, "Id", "Name", show.ActorIds);
                return View(show);
            }
        }
        */

        // GET: Show/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var show = m.ShowGetById(id.Value);
            if (show == null)
                return HttpNotFound();

            return View(show);
        }

        /*
        // GET: Show/{id}/AddEpisode
        [Authorize(Roles = "Clerk")]
        [Route("Show/{id}/AddEpisode")]
        public ActionResult AddEpisode(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var show = m.ShowGetById(id.Value);
            if (show == null)
                return HttpNotFound();

            var form = new EpisodeAddViewModel
            {
                ShowId = id.Value,
                ShowName = show.Name,
                GenreList = new SelectList(m.GenresGetAll(), "Name", "Name")
            };

            return View(form);
        }

        // POST: Show/{id}/AddEpisode
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Clerk")]
        [Route("Show/{id}/AddEpisode")]
        public ActionResult AddEpisode(int? id, EpisodeAddViewModel episode)
        {
            if (!ModelState.IsValid)
            {
                var show = m.ShowGetById(id.Value);
                episode.ShowName = show.Name;
                episode.GenreList = new SelectList(m.GenresGetAll(), "Name", "Name");
                return View(episode);
            }

            try
            {
                var episodeId = m.EpisodeAdd(episode, User.Identity.Name);
                return RedirectToAction("Details", "Episode", new { id = episodeId });
            }
            catch
            {
                var show = m.ShowGetById(id.Value);
                episode.ShowName = show.Name;
                episode.GenreList = new SelectList(m.GenresGetAll(), "Name", "Name");
                return View(episode);
            }
        }
        */

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