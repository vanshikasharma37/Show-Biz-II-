using System.Web.Mvc;

namespace VS2247A6.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class LoadDataController : Controller
    {

        // Reference to the manager object
        Manager m = new Manager();

        #region Load Data Actions
        // GET: LoadData
        [AllowAnonymous()]
        public ActionResult Roles()
        {
            if (m.LoadRoles())
            {
                return Content("Role data has been loaded");
            }
            else
            {
                return Content("Role data exists already");
            }
        }

        public ActionResult Genres()
        {
            if (m.LoadGenres())
            {
                return Content("Genre data has been loaded");
            }
            else
            {
                return Content("Genre data exists already");
            }
        }

        public ActionResult Actors()
        {
            if (m.LoadActors())
            {
                return Content("Actor data has been loaded");
            }
            else
            {
                return Content("Actor data exists already");
            }
        }

        public ActionResult Shows()
        {
            if (m.LoadShows())
            {
                return Content("Show data has been loaded");
            }
            else
            {
                return Content("Show data exists already");
            }
        }

        public ActionResult Episodes()
        {
            if (m.LoadEpisodes())
            {
                return Content("Episode data has been loaded");
            }
            else
            {
                return Content("Episode data exists already");
            }
        }
        #endregion

        public ActionResult Remove()
        {
            if (m.RemoveData())
            {
                return Content("data has been removed");
            }
            else
            {
                return Content("could not remove data");
            }
        }

        public ActionResult RemoveDatabase()
        {
            if (m.RemoveDatabase())
            {
                return Content("database has been removed");
            }
            else
            {
                return Content("could not remove database");
            }
        }

    }
}