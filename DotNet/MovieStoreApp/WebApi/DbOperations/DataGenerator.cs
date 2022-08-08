using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
            {
                if(context.Movies.Any())
                    return;

                context.Genres.AddRange(
                    new Genre{
                        Name = "Action" // 1
                    },
                    new Genre{
                        Name = "Adventure" // 2
                    },
                    new Genre{
                        Name = "Sci-Fi" // 3
                    },
                    new Genre{
                        Name = "Drama" // 4
                    },
                    new Genre{
                        Name = "Crime" // 5
                    }
                );
                context.Directors.AddRange(
                    new Director{
                        Name = "Christopher",
                        Surname = "Nolan",
                        /*MovieIds = new List<Movie>(){
                            context.Movies.SingleOrDefault(x => x.Name == "Inception"),
                            context.Movies.SingleOrDefault(x => x.Name == "Interstellar")
                        }*/
                    },
                    new Director{
                        Name = "Francis Ford",
                        Surname = "Coppola",
                        /*MovieIds = new List<Movie>(){
                            context.Movies.SingleOrDefault(x => x.Name == "The Godfather")
                        }*/
                    },
                    new Director{
                        Name = "Frank",
                        Surname = "Darabont",
                        /*MovieIds = new List<Movie>(){
                            context.Movies.SingleOrDefault(x => x.Name == "The Shawshank Redemption")
                        }*/
                    }
                );
                context.Actors.AddRange(
                    new Actor{ //1
                        Name = "Leanardo",
                        Surname = "DiCaprio",
                        /*Movies = new List<Movie>(){
                            context.Movies.SingleOrDefault(x => x.Name == "Inception"),
                        }*/
                    },
                    new Actor{ //2 
                        Name = "Joseph",
                        Surname = "Gordon-Levitt",
                        /*Movies = new List<Movie>(){
                            context.Movies.SingleOrDefault(x => x.Name == "Inception"),
                        }*/
                    },
                    new Actor{ //3
                        Name = "Matthew",
                        Surname = "McConaughey",
                        /*Movies = new List<Movie>(){
                            context.Movies.SingleOrDefault(x => x.Name == "Interstellar"),
                        }*/
                    },
                    new Actor{ //4
                        Name = "Anne",
                        Surname = "Hathaway",
                        /*Movies = new List<Movie>(){
                            context.Movies.SingleOrDefault(x => x.Name == "Interstellar"),
                        }*/
                    },
                    new Actor{ //5
                        Name = "Marlon",
                        Surname = "Brando",
                        /*Movies = new List<Movie>(){
                            context.Movies.SingleOrDefault(x => x.Name == "The Godfather"),
                        }*/
                    },
                    new Actor{ //6
                        Name = "Al",
                        Surname = "Pacino",
                        /*Movies = new List<Movie>(){
                            context.Movies.SingleOrDefault(x => x.Name == "The Godfather"),
                        }*/

                    },
                    new Actor{ //7
                        Name = "Tim",
                        Surname = "Robins",
                        /*Movies = new List<Movie>(){
                            context.Movies.SingleOrDefault(x => x.Name == "The Shawshank Redemption"),
                        }*/
                    },
                    new Actor{ //8
                        Name = "Morgan",
                        Surname = "Freeman",
                        /*Movies = new List<Movie>(){
                            context.Movies.SingleOrDefault(x => x.Name == "The Shawshank Redemption"),
                        }*/

                    }
                );
                context.Movies.AddRange(
                    new Movie{
                        Name = "Inception", //1
                        /*Genres = new List<Genre>(){
                            context.Genres.SingleOrDefault(x => x.Name ==" Action"),
                            context.Genres.SingleOrDefault(x => x.Name ==" Adventure"),
                            context.Genres.SingleOrDefault(x => x.Name ==" Sci-Fi"),
                        },*/
                        DrirectorId = 1,
                        Price = 25
                    },
                    new Movie{
                        Name =  "Interstellar", //2
                        /*Genres = new List<Genre>(){
                            context.Genres.SingleOrDefault(x => x.Name ==" Drama"),
                            context.Genres.SingleOrDefault(x => x.Name ==" Adventure"),
                            context.Genres.SingleOrDefault(x => x.Name ==" Sci-Fi"),
                        },*/
                        DrirectorId = 1,
                        Price = 35

                    },
                    new Movie{
                        Name = "The Godfather", //3
                        /*Genres = new List<Genre>(){
                            context.Genres.SingleOrDefault(x => x.Name ==" Drama"),
                            context.Genres.SingleOrDefault(x => x.Name ==" Crime"),
                        },*/
                        DrirectorId = 2,
                        Price = 20
                    },
                    new Movie{
                        Name = "The Shawshank Redemption", //4
                        /*Genres = new List<Genre>(){
                            context.Genres.SingleOrDefault(x => x.Name ==" Drama"),
                        },*/
                        DrirectorId = 3,
                        Price = 20
                    }
                ); 
                context.MovieActors.AddRange(
                    new MovieActor{
                        MovieId = 1,
                        ActorId = 1
                    },
                    new MovieActor{
                        MovieId = 1,
                        ActorId = 2
                    },
                    new MovieActor{
                        MovieId = 2,
                        ActorId = 3
                    },
                    new MovieActor{
                        MovieId = 2,
                        ActorId = 4
                    },
                    new MovieActor{
                        MovieId = 3,
                        ActorId = 5
                    },
                    new MovieActor{
                        MovieId = 3,
                        ActorId = 6
                    },
                    new MovieActor{
                        MovieId = 4,
                        ActorId = 7
                    },
                    new MovieActor{
                        MovieId = 4,
                        ActorId = 8
                    }
                );
                context.MovieGenres.AddRange(
                    new MovieGenre{
                        MovieId = 1,
                        GenreId = 1
                    },
                    new MovieGenre{
                        MovieId = 1,
                        GenreId = 2
                    },
                    new MovieGenre{
                        MovieId = 1,
                        GenreId = 3
                    },
                    new MovieGenre{
                        MovieId = 2,
                        GenreId = 2
                    },
                    new MovieGenre{
                        MovieId = 2,
                        GenreId = 3
                    },
                    new MovieGenre{
                        MovieId = 2,
                        GenreId = 4
                    },
                    new MovieGenre{
                        MovieId = 3,
                        GenreId = 4
                    },
                    new MovieGenre{
                        MovieId = 3,
                        GenreId = 5
                    },
                    new MovieGenre{
                        MovieId = 4,
                        GenreId = 5
                    }
                );
                context.DirectorMovies.AddRange(
                    new DirectorMovie{
                        DirectorId = 1,
                        MovieId = 1
                    },
                    new DirectorMovie{
                        DirectorId = 1,
                        MovieId = 2
                    },
                    new DirectorMovie{
                        DirectorId = 2,
                        MovieId = 3
                    },
                    new DirectorMovie{
                        DirectorId = 3,
                        MovieId = 4,
                    }
                );
                context.SaveChanges();
            };
        }
    }
}