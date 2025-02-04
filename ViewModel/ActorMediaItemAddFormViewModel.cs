using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ViewModel
{
    public class ActorMediaItemAddFormViewModel
    {
        public int ActorId { get; set; }
        public string ActorName { get; set; }
        public HttpPostedFileBase ContentUpload { get; set; }

        [Required]
        [StringLength(250)]
        public string Caption { get; set; }
    }
}