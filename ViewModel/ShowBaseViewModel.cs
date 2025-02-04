using System;
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class ShowBaseViewModel : BaseViewModel
    {
        [Required]
        [StringLength(150)]
        [Display(Name = "Show Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Genre { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Image")]
        [StringLength(250)]
        public string ImageUrl { get; set; }

        public string Coordinator { get; set; }

        public string Premise { get; set; }
    }
}