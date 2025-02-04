using System;
using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class Episode
    {
        public Episode()
        {
            AirDate = DateTime.Now;
        }

        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public int SeasonNumber { get; set; }

        public int EpisodeNumber { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        public DateTime AirDate { get; set; }

        [Required]
        [StringLength(250)]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(250)]
        public string Clerk { get; set; }

        public string Premise { get; set; }

        public int ShowId { get; set; }

        [Required]
        public virtual Show Show { get; set; }

        [StringLength(100)]
        public string VideoContentType { get; set; }

        public byte[] Video { get; set; }
    }
}