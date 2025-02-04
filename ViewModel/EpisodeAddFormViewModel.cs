using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ViewModel
{
    public class EpisodeAddFormViewModel
    {
        public EpisodeAddFormViewModel()
        {
            // Set default values
            AirDate = DateTime.Now;
        }

        public string ShowName { get; set; }

        [Required]
        public int ShowId { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Season")]
        [Range(1, int.MaxValue, ErrorMessage = "Season number must be greater than 0")]
        public int SeasonNumber { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Episode number must be greater than 0")]
        [Display(Name = "Episode")]

        public int EpisodeNumber { get; set; }

        [Required]
        [Display(Name = "Date Aired")]
        [DataType(DataType.Date)]
        public DateTime AirDate { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "Image URL is required")]
        [Display(Name = "Image URL")]
        [StringLength(250)]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string ImageUrl { get; set; }

        [DataType(DataType.MultilineText)]
        public string Premise { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Video")]
        public string VideoUpload { get; set; }

        public IEnumerable<SelectListItem> Genres { get; set; }
    }
}