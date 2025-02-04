using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class Show
    {
        public Show()
        {
            Actors = new HashSet<Actor>();
            Episodes = new HashSet<Episode>();
            ReleaseDate = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Genre { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [StringLength(250)]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(250)]
        public string Coordinator { get; set; }

        public string Premise { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }
        public virtual ICollection<Episode> Episodes { get; set; }
    }
}