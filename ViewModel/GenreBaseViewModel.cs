using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class GenreBaseViewModel : BaseViewModel
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}