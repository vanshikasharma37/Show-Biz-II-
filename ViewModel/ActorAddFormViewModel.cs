using System.ComponentModel.DataAnnotations;
using System;

namespace ViewModel
{
    public class ActorAddFormViewModel : BaseViewModel
    {
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [Display(Name = "Alternate Name")]
        public string AlternateName { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Height (m)")]
        public double? Height { get; set; }

        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; }

        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }
    }
}