using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class EpisodeAddViewModel
    {
        [Required, StringLength(150)]
        public string Name { get; set; }

        public int SeasonNumber { get; set; }

        public int EpisodeNumber { get; set; }

        [Required, StringLength(50)]
        public string Genre { get; set; }

        [Required]
        public DateTime AirDate { get; set; }

        public string ImageUrl { get; set; }

        [DataType(DataType.MultilineText)]
        public string Premise { get; set; }

        [StringLength(100)]
        public string VideoContentType { get; set; }

        public byte[] Video { get; set; }

        [Required]
        public int ShowId { get; set; }
    }
}