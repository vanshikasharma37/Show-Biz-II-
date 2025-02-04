using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModel;
using VS2247A6.Controllers;

namespace Controllers
{
    public class ActorMediaItemController : Controller
    {
        private readonly Manager m = new Manager();

        // GET: ActorMediaItem
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("Actor/MediaItemDownload/{id}")]
        public ActionResult MediaItemDownload(int id)
        {
            var mediaItem = m.ActorMediaItemGetById(id);

            if (mediaItem == null || mediaItem.Content == null)
            {
                return HttpNotFound();
            }

            // Determine file extension based on ContentType
            string extension = GetFileExtension(mediaItem.ContentType);

            // Ensure the filename has the correct extension
            string fileName = mediaItem.Caption;
            if (!string.IsNullOrEmpty(extension) && !fileName.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
            {
                fileName += extension;
            }

            return File(mediaItem.Content, mediaItem.ContentType, fileName);
        }

        // Helper method to map ContentType to file extensions
        private string GetFileExtension(string contentType)
        {
            var mimeTypeMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "image/jpg", ".jpg" },
                { "image/jpeg", ".jpeg" },
                { "image/png", ".png" },
                { "application/pdf", ".pdf" },
                { "audio/mpeg", ".mp3" },
                { "video/mp4", ".mp4" }
            };

            return mimeTypeMapping.TryGetValue(contentType, out var extension) ? extension : string.Empty;
        }


        // GET: Show/Add
        [Authorize(Roles = "Executive")]
        [Route("Actor/{id}/AddMediaItem")]
        public ActionResult Create(int id)
        {
            var actor = m.ActorGetById(id);

            if (actor == null) return HttpNotFound();
            var model = new ActorMediaItemAddFormViewModel
            {
                ActorId = actor.Id,
                ActorName = actor.Name
            };
            return View(model);
        }

        // POST: Show/Add
        [HttpPost]
        [Authorize(Roles = "Executive")]
        [ValidateAntiForgeryToken]
        [Route("Actor/{id}/AddMediaItem")]
        public ActionResult Create(int id, ActorMediaItemAddViewModel model, HttpPostedFileBase ContentUpload)
        {
            if (model.ContentUpload == null || model.ContentUpload.ContentLength == 0)
            {
                ModelState.AddModelError("ContentUpload", "Please upload a file.");
            }
            else
            {
                // Allowed content types
                var allowedContentTypes = new[]
                {
                    "image/jpg",
                    "image/jpeg",
                    "image/png",
                    "application/pdf",
                    "audio/mpeg",
                    "video/mp4"
                };

                // Check if the uploaded file's ContentType is valid
                if (!allowedContentTypes.Contains(model.ContentUpload.ContentType.ToLower()))
                {
                    ModelState.AddModelError("ContentUpload", "Invalid file type. Allowed types are: .jpg, .jpeg, .png, .pdf, .mp3, .mp4.");
                }

                // Optional: Check file size
                const int maxFileSize = 10 * 1024 * 1024; // 10 MB
                if (model.ContentUpload.ContentLength > maxFileSize)
                {
                    ModelState.AddModelError("ContentUpload", "File size must be less than 10 MB.");
                }
            }

            if (!ModelState.IsValid)
            {
                var actor = m.ActorGetById(id);
                var form = new ActorMediaItemAddFormViewModel
                {
                    ActorId = model.ActorId,
                    ActorName = actor.Name
                };
                return View(form);
            }

            if (ContentUpload != null && ContentUpload.ContentLength > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    ContentUpload.InputStream.CopyTo(memoryStream);
                    var uploadedData = memoryStream.ToArray();

                    // Other properties
                    model.Content = uploadedData;
                    model.ContentType = ContentUpload.ContentType;
                }
            }

            var addedShowId = m.ActorMediaItemAdd(model);

            return RedirectToAction("Details", "Actor", new { id = model.ActorId });
        }
    }
}