using System.Linq;

namespace LinqueDenemeleri.DbOperations
{
    public class DataGenerator 
    {
        public static void Initialize()
        {
            using(var context = new LinqueDbContext())
            {
                if(context.Students.Any())
                {
                    return;
                }
                context.Students.AddRange(
                    new Student{
                        Name = "Bahtiyar",
                        Surname = "Bayır",
                        ClassId = 1
                    },
                    new Student{
                        Name = "Musa",
                        Surname = "Örencik",
                        ClassId = 1
                    },
                    new Student{
                        Name = "Samet",
                        Surname = "Erben",
                        ClassId = 1
                    }
                );
                context.SaveChanges();
            }
        }
    }
}