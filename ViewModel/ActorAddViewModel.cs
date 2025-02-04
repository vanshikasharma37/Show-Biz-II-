using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class ActorAddViewModel
    {
        [Required]
        [StringLength(150)]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Display(Name = "Alternate Name")]
        [StringLength(150)]
        public string AlternateName { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Height (m)")]
        public double? Height { get; set; }

        [Required]
        [Display(Name = "Image URL")]
        [StringLength(250)]
        public string ImageUrl { get; set; }

        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }
    }
}