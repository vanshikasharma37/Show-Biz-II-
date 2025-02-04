using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class ActorMediaItem
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ContentType { get; set; }

        [Required]
        public byte[] Content { get; set; }

        [Required]
        [StringLength(250)]
        public string Caption { get; set; }

        public int ActorId { get; set; }

        // Navigation property
        public virtual Actor Actor { get; set; }
    }
}