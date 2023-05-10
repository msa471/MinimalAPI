using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Models
{
    public class Actor
    {

        [Key]
        public int Id { get; set; } 

        public string Name { get; set; }        

        public string Bio { get; set; }

        //relationships
        public List<Actor_Movie> Actors_Movies { get; set; }    
    }
}
