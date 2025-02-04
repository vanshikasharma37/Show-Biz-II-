using System.Collections.Generic;

namespace ViewModel
{
    public class ShowWithInfoViewModel : ShowBaseViewModel
    {
        public ShowWithInfoViewModel()
        {
            Actors = new List<ActorBaseViewModel>();
            Episodes = new List<EpisodeBaseViewModel>();
        }

        public IEnumerable<ActorBaseViewModel> Actors { get; set; }
        public IEnumerable<EpisodeBaseViewModel> Episodes { get; set; }
    }
}