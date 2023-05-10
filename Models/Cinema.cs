using System.ComponentModel.DataAnnotations;

namespace MinimalAPI.Models
{
    public class Cinema
    {
        [Key]
        public int Id { get; set; }     
        public string Name { get; set; }    
        public string Location { get; set; } 

        //relationships
        public List<Movie> Movies { get; set; } 
    }
}
