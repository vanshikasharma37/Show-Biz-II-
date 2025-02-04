using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ViewModel
{
    public class ActorMediaItemAddViewModel
    {
        [Required]
        public int ActorId { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        public HttpPostedFileBase ContentUpload { get; set; }

        [Required]
        [StringLength(250)]
        public string Caption { get; set; }
    }
}