using MinimalAPI.Models;

namespace MinimalAPI.Repositories
{
    public class MovieRepository
    {
        public static List<Movie> Movies = new() 
        {
            new(){ Id=1, Title="Batman Begins",Description="Origin Story of the Batman", Rating=7.4},
            new(){ Id=2, Title="Batman-The Dark Knight",Description="Batman takes on the Joker in this blockbuster classic", Rating=9.5},
            new(){ Id=3, Title="Batman-The Dark Knight Rises",Description="Batman faces his worst fears and the child of Raas al Ghul", Rating=8.3},
        };
    }
}
