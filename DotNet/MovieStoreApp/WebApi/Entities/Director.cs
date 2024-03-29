using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
    public class Director
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<DirectorMovie> DirectorMovies { get; set; }

        public override string ToString()
        {
            string message = Name + " " + Surname.ToUpper();
            return message;
        }
    }
}