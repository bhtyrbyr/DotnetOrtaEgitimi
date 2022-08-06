using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entitys
{
    public class Author
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }

        public Author()
        {                   }
        public Author(string name, string surname, DateTime birthDate)
        {
            Name = name;
            Surname = surname;
            BirthDate = birthDate;
        }
        
        public override string ToString()
        {
            return new string(Name + " " + Surname + " - DT: " + BirthDate.Date);
        }
    }
}