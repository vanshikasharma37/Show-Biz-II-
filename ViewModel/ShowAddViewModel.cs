using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModel
{
    public class ShowAddViewModel
    {
        [Required, StringLength(150)]
        public string Name { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required, StringLength(50)]
        public string Genre { get; set; }

        [Required, StringLength(250)]
        public string ImageUrl { get; set; }

        [DataType(DataType.MultilineText)]
        public string Premise { get; set; }

        public List<int> SelectedActorIds { get; set; }

    }
}