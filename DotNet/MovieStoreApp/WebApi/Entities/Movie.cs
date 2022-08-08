using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Movie
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MovieGenre> MovieGenres { get; set; }
        public int DrirectorId { get; set; }
        public Director Drirector { get; set; }
        public List<MovieActor> MovieActors { get; set; }
        public int Price { get; set; }
    }
}