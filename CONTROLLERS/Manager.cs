using AutoMapper;
using Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Web;
using ViewModel;
using VS2247A6.Data;
using VS2247A6.Models;

// ************************************************************************************
// WEB524 Project Template V$templateVer$ == $semesterCode$-$guid1$
//
// By submitting this assignment you agree to the following statement.
// I declare that this assignment is my own work in accordance with the Seneca Academic
// Policy. No part of this assignment has been copied manually or electronically from
// any other source (including web sites) or distributed to other students.
// ************************************************************************************

namespace VS2247A6.Controllers
{
    public class Manager
    {

        // Reference to the data context
        private ApplicationDbContext ds = new ApplicationDbContext();

        // AutoMapper instance
        public IMapper mapper;

        // Request user property...

        // Backing field for the property
        private RequestUser _user;

        // Getter only, no setter
        public RequestUser User
        {
            get
            {
                // On first use, it will be null, so set its value
                if (_user == null)
                {
                    _user = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);
                }
                return _user;
            }
        }

        #region constructor
        // Default constructor...
        public Manager()
        {
            // If necessary, add constructor code here

            // Configure the AutoMapper components
            var config = new MapperConfiguration(cfg =>
            {
                // Define the mappings below, for example...
                // cfg.CreateMap<SourceType, DestinationType>();
                // cfg.CreateMap<Product, ProductBaseViewModel>();

                // Genre mappings
                cfg.CreateMap<Genre, GenreBaseViewModel>();

                // Actor mappings
                cfg.CreateMap<Actor, ActorBaseViewModel>();
                cfg.CreateMap<Actor, ActorWithShowInfoViewModel>()
                    .ForMember(dest => dest.Shows, opt => opt.MapFrom(src => src.Shows))
                    .ForMember(dest => dest.ActorMediaItems, opt => opt.MapFrom(src => src.ActorMediaItems));
                cfg.CreateMap<ActorAddViewModel, Actor>();

                // Show mappings
                cfg.CreateMap<Show, ShowBaseViewModel>();
                cfg.CreateMap<Show, ShowWithInfoViewModel>()
                    .ForMember(dest => dest.Actors, opt => opt.MapFrom(src => src.Actors))
                    .ForMember(dest => dest.Episodes, opt => opt.MapFrom(src => src.Episodes));
                cfg.CreateMap<ShowAddViewModel, Show>();

                // Episode mappings
                cfg.CreateMap<Episode, EpisodeBaseViewModel>();
                cfg.CreateMap<Episode, EpisodeWithShowNameViewModel>()
                    .ForMember(dest => dest.Show, opt => opt.MapFrom(src => src.Show));
                cfg.CreateMap<EpisodeAddViewModel, Episode>();

                // ActorMediaItem mapping
                cfg.CreateMap<ActorMediaItem, ActorMediaItemBaseViewModel>();
                cfg.CreateMap<ActorMediaItemAddViewModel, ActorMediaItem>();

                cfg.CreateMap<Models.RegisterViewModel, Models.RegisterViewModelForm>();
            });

            mapper = config.CreateMapper();

            // Turn off the Entity Framework (EF) proxy creation features
            // We do NOT want the EF to track changes - we'll do that ourselves
            ds.Configuration.ProxyCreationEnabled = false;

            // Also, turn off lazy loading...
            // We want to retain control over fetching related objects
            ds.Configuration.LazyLoadingEnabled = false;
        }
        #endregion

        // Add your methods below and call them from controllers. Ensure that your methods accept
        // and deliver ONLY view model objects and collections. When working with collections, the
        // return type is almost always IEnumerable<T>.
        //
        // Remember to use the suggested naming convention, for example:
        // ProductGetAll(), ProductGetById(), ProductAdd(), ProductEdit(), and ProductDelete().




        // *** Add your methods ABOVE this line **

        #region Role Claims

        public List<string> RoleClaimGetAllStrings()
        {
            return ds.RoleClaims.OrderBy(r => r.Name).Select(r => r.Name).ToList();
        }

        #endregion

        public static void InitializeUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                // Create roles
                string[] roleNames = { "Administrator", "Executive", "Coordinator", "Clerk" };
                foreach (var roleName in roleNames)
                {
                    if (!roleManager.RoleExists(roleName))
                    {
                        roleManager.Create(new IdentityRole(roleName));
                    }
                }

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                // Define users
                var users = new[]
                {
                    new { Email = "admin@example.com", Password = "Password123!", Role = "Administrator" },
                    new { Email = "exec@example.com", Password = "Password123!", Role = "Executive" },
                    new { Email = "coord@example.com", Password = "Password123!", Role = "Coordinator" },
                    new { Email = "clerk@example.com", Password = "Password123!", Role = "Clerk" }
                };

                foreach (var user in users)
                {
                    if (userManager.FindByEmail(user.Email) == null)
                    {
                        var appUser = new ApplicationUser { UserName = user.Email, Email = user.Email };
                        var result = userManager.Create(appUser, user.Password);

                        if (result.Succeeded)
                        {
                            userManager.AddToRole(appUser.Id, user.Role);

                            userManager.AddClaim(appUser.Id, new Claim(ClaimTypes.Email, user.Email));
                            userManager.AddClaim(appUser.Id, new Claim(ClaimTypes.GivenName, user.Role));
                            userManager.AddClaim(appUser.Id, new Claim(ClaimTypes.Role, user.Role));
                        }
                    }
                }
            }
        }

        #region Load Data Methods

        // Add some programmatically-generated objects to the data store
        // Write a method for each entity and remember to check for existing
        // data first.  You will call this/these method(s) from a controller action.

        #region Load Data
        public bool LoadRoles()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // *** Role claims ***
            if (ds.RoleClaims.Count() == 0)
            {
                ds.RoleClaims.Add(new RoleClaim() { Name = "Administrator" });

                // Add additional role claims here
                ds.RoleClaims.Add(new RoleClaim() { Name = "Executive" });
                ds.RoleClaims.Add(new RoleClaim() { Name = "Coordinator" });
                ds.RoleClaims.Add(new RoleClaim() { Name = "Clerk" });

                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool LoadGenres()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // *** Role claims ***
            if (ds.Genres.Count() == 0)
            {
                var genres = new List<Genre>
                {
                    new Genre { Name = "Action" },
                    new Genre { Name = "Comedy" },
                    new Genre { Name = "Drama" },
                    new Genre { Name = "Horror" },
                    new Genre { Name = "Romance" },
                    new Genre { Name = "Sci-Fi" },
                    new Genre { Name = "Thriller" },
                    new Genre { Name = "Documentary" },
                    new Genre { Name = "Animation" },
                    new Genre { Name = "Mystery" }
                };

                ds.Genres.AddRange(genres);
                ds.SaveChanges();
                
                done = true;
            }

            return done;
        }

        public bool LoadActors()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            // *** Role claims ***
            if (ds.Actors.Count() == 0)
            {
                var actors = new List<Actor>
                {
                    new Actor
                    {
                        Name = "Dwayne Johnson",
                        AlternateName = "The Rock",
                        BirthDate = new DateTime(1972, 5, 2),
                        Height = 1.96,
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/1/11/Dwayne_%22The_Rock%22_Johnson_Visits_the_Pentagon_%2841%29_%28cropped%29.jpg",
                        Executive = user
                    },
                    new Actor
                    {
                        Name = "Scarlett Johansson",
                        AlternateName = "ScarJo",
                        BirthDate = new DateTime(1984, 11, 22),
                        Height = 1.6,
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/2/26/Scarlett_Johansson_by_Gage_Skidmore_2019.jpg/1200px-Scarlett_Johansson_by_Gage_Skidmore_2019.jpg",
                        Executive = user
                    },
                    new Actor
                    {
                        Name = "Leonardo DiCaprio",
                        AlternateName = "Leo",
                        BirthDate = new DateTime(1974, 11, 11),
                        Height = 1.83,
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/4/46/Leonardo_Dicaprio_Cannes_2019.jpg",
                        Executive = user
                    },
                    new Actor
                    {
                        Name = "Morgan Freeman",
                        BirthDate = new DateTime(1937, 6, 1),
                        Height = 1.88,
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/4/42/Morgan_Freeman_at_The_Pentagon_on_2_August_2023_-_230802-D-PM193-3363_%28cropped%29.jpg",
                        Executive = user
                    },
                    new Actor
                    {
                        Name = "Mary Louise Streep",
                        AlternateName = "Meryl Streep",
                        BirthDate = new DateTime(1949, 6, 22),
                        Height = 1.68,
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a3/Meryl_Streep_by_Jack_Mitchell.jpg/170px-Meryl_Streep_by_Jack_Mitchell.jpg",
                        Executive = user
                    },
                    new Actor
                    {
                        Name = "Thomas Jeffrey Hanks",
                        AlternateName = "Tom Hanks",
                        BirthDate = new DateTime(1956, 7, 9),
                        Height = 1.83,
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/a/a9/Tom_Hanks_TIFF_2019.jpg",
                        Executive = user
                    }
                };

                ds.Actors.AddRange(actors);
                ds.SaveChanges();
                done = true;
            }

            return done;
        }

        public bool LoadShows()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            var actors = ds.Actors.ToList();
            if (!actors.Any()) return done;

            // *** Role claims ***
            if (ds.Shows.Count() == 0)
            {
                var shows = new List<Show>
                {
                    new Show
                    {
                        Name = "WWE Smackdown!",
                        Genre = "Action",
                        ReleaseDate = new DateTime(1999, 4, 29),
                        ImageUrl = "https://m.media-amazon.com/images/M/MV5BNGJmZmM3ODAtYWRhZS00ZjUyLWI0NzAtMTMzZjY5YWY1OWRmXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                        Coordinator = user,
                        Actors = new List<Actor> { actors[0], actors[1] }
                    },
                    new Show
                    {
                        Name = "Inception",
                        Genre = "Sci-Fi",
                        ReleaseDate = new DateTime(2010, 7, 16),
                        ImageUrl = "https://m.media-amazon.com/images/M/MV5BMjAxMzY3NjcxNF5BMl5BanBnXkFtZTcwNTI5OTM0Mw@@._V1_FMjpg_UX1000_.jpg",
                        Coordinator = user,
                        Actors = new List<Actor> { actors[1], actors[2] }
                    },
                    new Show
                    {
                        Name = "The Crown",
                        Genre = "Drama",
                        ReleaseDate = DateTime.Now.AddMonths(-6),
                        ImageUrl = "https://upload.wikimedia.org/wikipedia/en/b/ba/The_Crown_season_2.jpeg",
                        Coordinator = user,
                        Actors = new List<Actor> { actors[1], actors[2], actors[3] }
                    },
                    new Show
                    {
                        Name = "Stranger Things",
                        Genre = "Sci-Fi",
                        ReleaseDate = DateTime.Now.AddMonths(-3),
                        ImageUrl = "https://m.media-amazon.com/images/M/MV5BMjE2N2MyMzEtNmU5NS00OTI0LTlkNTMtMWM1YWYyNmU4NmY0XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg",
                        Coordinator = user,
                        Actors = new List<Actor> { actors[5], actors[3], actors[4] }
                    }
                };

                ds.Shows.AddRange(shows);
                ds.SaveChanges();

                done = true;
            }

            return done;
        }

        public bool LoadEpisodes()
        {
            // User name
            var user = HttpContext.Current.User.Identity.Name;

            // Monitor the progress
            bool done = false;

            var shows = ds.Shows.ToList();
            if (!shows.Any()) return done;

            // *** Role claims ***
            if (ds.Episodes.Count() == 0)
            {
                foreach (var show in shows)
                {
                    for (int i = 1; i <= 3; i++)
                    {
                        ds.Episodes.Add(new Episode
                        {
                            Name = $"{show.Name} - Episode {i}",
                            SeasonNumber = 1,
                            EpisodeNumber = i,
                            Genre = show.Genre,
                            AirDate = DateTime.Now.AddDays(i * 7),
                            ImageUrl = $"https://upload.wikimedia.org/wikipedia/en/2/25/Tower-heist-movie-poster-hi-res-01-405x600.jpg",
                            Clerk = user,
                            Show = show
                        });
                    }
                }
                ds.SaveChanges();
                done = true;
            }

            return done;
        }
        #endregion


        // Genre methods
        public IEnumerable<GenreBaseViewModel> GenresGetAll()
        {
            // If no genres exist, load the default ones
            if (!ds.Genres.Any())
            {
                LoadGenres();
            }

            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreBaseViewModel>>(ds.Genres.OrderBy(g => g.Name));
        }

        // Actor methods
        public IEnumerable<ActorBaseViewModel> ActorsGetAll()
        {
            return mapper.Map<IEnumerable<Actor>, IEnumerable<ActorBaseViewModel>>(ds.Actors.OrderBy(a => a.Name));
        }

        public ActorWithShowInfoViewModel ActorGetById(int id)
        {
            var actor = ds.Actors.Include(a => a.Shows)
                .Include(a => a.ActorMediaItems)
                .SingleOrDefault(a => a.Id == id);
           
            if(actor == null) return null;

            var viewModel  = mapper.Map<Actor, ActorWithShowInfoViewModel>(actor);

            // Sort media items by Caption
            viewModel.Photos = viewModel.ActorMediaItems
                .Where(m => m.ContentType.StartsWith("image/"))
                .OrderBy(m => m.Caption)
                .ToList();

            viewModel.Documents = viewModel.ActorMediaItems
                .Where(m => m.ContentType == "application/pdf")
                .OrderBy(m => m.Caption)
                .ToList();

            viewModel.AudioClips = viewModel.ActorMediaItems
                .Where(m => m.ContentType.StartsWith("audio/"))
                .OrderBy(m => m.Caption)
                .ToList();

            viewModel.VideoClips = viewModel.ActorMediaItems
                .Where(m => m.ContentType.StartsWith("video/"))
                .OrderBy(m => m.Caption)
                .ToList();

            return viewModel;
        }

        public int ActorAdd(ActorAddViewModel newActor, string executiveUserName)
        {
            var actor = mapper.Map<ActorAddViewModel, Actor>(newActor);
            actor.Executive = executiveUserName;
            ds.Actors.Add(actor);
            ds.SaveChanges();
            return actor.Id;
        }

        public int ActorMediaItemAdd(ActorMediaItemAddViewModel actorMediaItem)
        {
            var mediaItem = mapper.Map<ActorMediaItemAddViewModel, ActorMediaItem>(actorMediaItem);

            ds.ActorMediaItems.Add(mediaItem);
            ds.SaveChanges();
            return mediaItem.Id;
        }

        // Show methods
        public IEnumerable<ShowBaseViewModel> ShowsGetAll()
        {
            return mapper.Map<IEnumerable<Show>, IEnumerable<ShowBaseViewModel>>(ds.Shows.OrderBy(s => s.Name));
        }

        public ShowWithInfoViewModel ShowGetById(int id)
        {
            var show = ds.Shows.Include(s => s.Actors)
                              .Include(s => s.Episodes)
                              .SingleOrDefault(s => s.Id == id);
            return show == null ? null : mapper.Map<Show, ShowWithInfoViewModel>(show);
        }

        public int ShowAdd(ShowAddViewModel newShow, string coordinatorUserName)
        {
            var show = mapper.Map<ShowAddViewModel, Show>(newShow);
            show.Coordinator = coordinatorUserName;

            if (newShow.SelectedActorIds != null && newShow.SelectedActorIds.Any())
            {
                show.Actors = ds.Actors.Where(a => newShow.SelectedActorIds.Contains(a.Id)).ToList();
            }

            ds.Shows.Add(show);
            ds.SaveChanges();
            return show.Id;
        }


        // Episode methods
        public IEnumerable<EpisodeWithShowNameViewModel> EpisodesGetAll()
        {
            return mapper.Map<IEnumerable<Episode>, IEnumerable<EpisodeWithShowNameViewModel>>(ds.Episodes.Include(e => e.Show)
                    .OrderBy(e => e.Show.Name)
                    .ThenBy(e => e.SeasonNumber)
                    .ThenBy(e => e.EpisodeNumber));
        }

        public EpisodeWithShowNameViewModel EpisodeGetById(int id)
        {
            var episode = ds.Episodes.Include(e => e.Show)
                .SingleOrDefault(e => e.Id == id);
            return episode == null ? null : mapper.Map<Episode, EpisodeWithShowNameViewModel>(episode);
        }

        public int EpisodeAdd(EpisodeAddViewModel newEpisode, string clerkUserName)
        {
            var episode = mapper.Map<EpisodeAddViewModel, Episode>(newEpisode);
            episode.Clerk = clerkUserName;

            var show = ds.Shows.Find(newEpisode.ShowId);
            if (show == null)
                throw new InvalidOperationException("Show not found");

            episode.Show = show;
            ds.Episodes.Add(episode);
            ds.SaveChanges();
            return episode.Id;
        }

        public EpisodeVideoViewModel EpisodeVideoGetById(int id)
        {
            var episode = ds.Episodes.Find(id);

            if (episode != null && episode.Video != null)
            {
                return new EpisodeVideoViewModel
                {
                    VideoContentType = episode.VideoContentType,
                    Video = episode.Video
                };
            }

            return null;
        }


        // ActorMediaItem method

        public ActorMediaItemWithContentViewModel ActorMediaItemGetById(int id)
        {
            var mediaItem = ds.ActorMediaItems.Find(id);

            if(mediaItem != null && mediaItem.Content != null)
            {
                return new ActorMediaItemWithContentViewModel
                {
                    Id = id,
                    Caption = mediaItem.Caption,
                    ContentType = mediaItem.ContentType,
                    Content = mediaItem.Content
                };
            }
            return null;
        }

        public bool RemoveData()
        {
            try
            {
                foreach (var e in ds.RoleClaims)
                {
                    ds.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                }

                // Remove additional entities as needed.

                ds.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveDatabase()
        {
            try
            {
                return ds.Database.Delete();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
            ds.Dispose();
        }
    }

    #endregion

    #region RequestUser Class

    // This "RequestUser" class includes many convenient members that make it
    // easier work with the authenticated user and render user account info.
    // Study the properties and methods, and think about how you could use this class.

    // How to use...
    // In the Manager class, declare a new property named User:
    //    public RequestUser User { get; private set; }

    // Then in the constructor of the Manager class, initialize its value:
    //    User = new RequestUser(HttpContext.Current.User as ClaimsPrincipal);

    public class RequestUser
    {
        // Constructor, pass in the security principal
        public RequestUser(ClaimsPrincipal user)
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                Principal = user;

                // Extract the role claims
                RoleClaims = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

                // User name
                Name = user.Identity.Name;

                // Extract the given name(s); if null or empty, then set an initial value
                string gn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.GivenName).Value;
                if (string.IsNullOrEmpty(gn)) { gn = "(empty given name)"; }
                GivenName = gn;

                // Extract the surname; if null or empty, then set an initial value
                string sn = user.Claims.SingleOrDefault(c => c.Type == ClaimTypes.Surname).Value;
                if (string.IsNullOrEmpty(sn)) { sn = "(empty surname)"; }
                Surname = sn;

                IsAuthenticated = true;
                // You can change the string value in your app to match your app domain logic
                IsAdmin = user.HasClaim(ClaimTypes.Role, "Admin") ? true : false;
            }
            else
            {
                RoleClaims = new List<string>();
                Name = "anonymous";
                GivenName = "Unauthenticated";
                Surname = "Anonymous";
                IsAuthenticated = false;
                IsAdmin = false;
            }

            // Compose the nicely-formatted full names
            NamesFirstLast = $"{GivenName} {Surname}";
            NamesLastFirst = $"{Surname}, {GivenName}";
        }

        // Public properties
        public ClaimsPrincipal Principal { get; private set; }

        public IEnumerable<string> RoleClaims { get; private set; }

        public string Name { get; set; }

        public string GivenName { get; private set; }

        public string Surname { get; private set; }

        public string NamesFirstLast { get; private set; }

        public string NamesLastFirst { get; private set; }

        public bool IsAuthenticated { get; private set; }

        public bool IsAdmin { get; private set; }

        public bool HasRoleClaim(string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(ClaimTypes.Role, value) ? true : false;
        }

        public bool HasClaim(string type, string value)
        {
            if (!IsAuthenticated) { return false; }
            return Principal.HasClaim(type, value) ? true : false;
        }
    }

    #endregion

}