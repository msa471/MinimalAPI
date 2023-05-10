using MinimalAPI.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalAPI.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; } 
        public string Title { get; set; }   
        public string Description { get; set; } 
        public double Rating { get; set; }  

        public MovieCategory MovieCategory { get; set; }

        public List<Actor_Movie> Actors_Movies { get; set; }

        public int CinemaId { get; set; }
       [ForeignKey("CinemaId")]
        public Cinema Cinema { get; set; }

        public int ProducerId { get; set; }
       [ForeignKey("ProducerId")]
        public Producer Producer { get; set; }
    }
}
