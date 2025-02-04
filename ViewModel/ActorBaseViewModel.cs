using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class ActorBaseViewModel : BaseViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Alternate Name")]
        public string AlternateName { get; set; }

        [Display(Name = "Birth Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Height (m)")]
        public double? Height { get; set; }

        [Display(Name = "Image")]
        [StringLength(250)]
        public string ImageUrl { get; set; }

        public string Executive { get; set; }

        public string Biography { get; set; }
    }
}