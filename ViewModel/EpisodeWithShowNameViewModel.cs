namespace ViewModel
{
    public class EpisodeWithShowNameViewModel : EpisodeBaseViewModel
    {
        public EpisodeWithShowNameViewModel()
        {
            Show = new ShowBaseViewModel();
        }
        public ShowBaseViewModel Show { get; set; }
    }
}