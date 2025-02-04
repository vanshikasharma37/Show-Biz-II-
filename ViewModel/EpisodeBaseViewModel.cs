using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class EpisodeBaseViewModel : BaseViewModel
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Display(Name = "Season")]
        public int SeasonNumber { get; set; }

        [Display(Name = "Episode")]
        public int EpisodeNumber { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        [Display(Name = "Date Aired")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime AirDate { get; set; }

        [Required]
        [Display(Name = "Image")]
        [StringLength(250)]
        public string ImageUrl { get; set; }

        public string Clerk { get; set; }

        public int ShowId { get; set; }

        public string Premise { get; set; }

        public string VideoContentType { get; set; }
    }
}