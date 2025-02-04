using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ViewModel
{
    public class ShowAddFormViewModel
    {
        public ShowAddFormViewModel()
        {
            // Set default values
            ReleaseDate = DateTime.Now;
        }

        public int ActorId { get; set; }

        public string ActorName { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required, StringLength(50)]
        public string Genre { get; set; }

        [Required]
        [Display(Name = "Cover Art URL")]
        [StringLength(250)]
        public string ImageUrl { get; set; }

        [Display(Name = "Associated Actors")]
        public List<int> SelectedActorIds { get; set; }

        [DataType(DataType.MultilineText)]
        public string Premise { get; set; }

        public IEnumerable<SelectListItem> Genres { get; set; }
        public IEnumerable<SelectListItem> Actors { get; set; }
    }
}