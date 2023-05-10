using MinimalAPI.Models;

namespace MinimalAPI.Data
{
    public class AppDbInitializer
    {

        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<UserDbContext>();

                context.Database.EnsureCreated();

                if (!context.Cinemas.Any())
                {
                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema()
                        {
                            Name = "Cinema 1",
                            Location = "Leicester"
                        },
                          new Cinema()
                        {
                            Name = "Cinema 2",
                            Location = "London"
                        },

                    });
                    context.SaveChanges();  
                }
                if (!context.Actors.Any())
                {
                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            Id = 1,
                            Name = "Ryan Reynolds",
                            Bio = "Deadpool"
                        },
                          new Actor()
                        {
                            Id = 2,
                            Name = "Robert Downey Jr",
                            Bio = "Iron Man"
                        },

                    });
                    context.SaveChanges();

                }
                if (!context.Producers.Any())
                {
                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer()
                        {
                            FullName = "Steven Spielberg",
                            Bio = "ET"
                        },
                          new Producer()
                        {
                            FullName = "Quentin Tarantino\r\n",
                            Bio = "Pulp Fiction"
                        },

                    });
                    context.SaveChanges();

                }
                if (!context.Movies.Any())
                {
                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            Id = 1,
                            Title = "Dark Knight",
                            Description = "Batman 2",
                            Rating = 9.5,
                            MovieCategory = MovieCategory.Action,
                            CinemaId = 1,
                            ProducerId = 1,
                        },
                         new Movie()
                        {
                            Id  = 2,    
                            Title = "Dark Knight Rises",
                            Description = "Batman 3",
                            Rating = 9.3,
                            MovieCategory = MovieCategory.Action,
                            CinemaId = 2,
                            ProducerId = 1,
                        }
                    });
                    context.SaveChanges();
                }
                try
                {
                    if (!context.Actors_Movies.Any())
                    {
                        context.Actors_Movies.AddRange(new List<Actor_Movie>()
                    {
                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 3
                        },
                         new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 3
                        },
                          new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 3
                        }
                    });
                        context.SaveChanges();
                    } }
                catch (Exception ex) {
                }
                }
                
            }
        }
    }

