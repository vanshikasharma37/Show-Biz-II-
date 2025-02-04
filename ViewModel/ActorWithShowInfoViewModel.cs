using System.Collections.Generic;

namespace ViewModel
{
    public class ActorWithShowInfoViewModel : ActorBaseViewModel
    {
        public ActorWithShowInfoViewModel()
        {
            Shows = new List<ShowBaseViewModel>();
            ActorMediaItems = new List<ActorMediaItemBaseViewModel>();
            Photos = new List<ActorMediaItemBaseViewModel>();
            Documents = new List<ActorMediaItemBaseViewModel>();
            AudioClips = new List<ActorMediaItemBaseViewModel>();
            VideoClips = new List<ActorMediaItemBaseViewModel>();
        }

        public IEnumerable<ShowBaseViewModel> Shows { get; set; }

        public List<ActorMediaItemBaseViewModel> ActorMediaItems { get; set; }

        public List<ActorMediaItemBaseViewModel> Photos { get; set; }

        public List<ActorMediaItemBaseViewModel> Documents { get; set; }

        public List<ActorMediaItemBaseViewModel> AudioClips { get; set; }

        public List<ActorMediaItemBaseViewModel> VideoClips { get; set; }
    }
}