using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class Actor
    {
        public Actor()
        {
            ActorMediaItems = new HashSet<ActorMediaItem>();
            Shows = new HashSet<Show>();
            BirthDate = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [StringLength(150)]
        public string AlternateName { get; set; }

        public DateTime? BirthDate { get; set; }

        public double? Height { get; set; }

        [Required]
        [StringLength(250)]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(250)]
        public string Executive { get; set; }

        public string Biography { get; set; }

        public virtual ICollection<Show> Shows { get; set; }
        public virtual ICollection<ActorMediaItem> ActorMediaItems { get; set; }
    }
}