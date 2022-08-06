using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Common;

namespace WebAPI.Entitys
{
    public class Book
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }

        public override string ToString()
        {
            string message = new string(
                "id          : " + ID.ToString() + "\n" + 
                "Title       : " + Title.ToString() + "\n" + 
                "Genre       : " + Genre.Name + "\n" + 
                "PageCount   : " + PageCount.ToString() + "\n" + 
                "PublishDate : " + PublishDate.ToString("gg/AA/yyyy") + "\n"
            );
            return message;
        }
    }
}