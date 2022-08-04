using System;
using System.Linq;
using LinqueDenemeleri.DbOperations;

namespace LinqueDenemeleri
{
    class Program
    {
        static void Main(string[] args)
        {
            DataGenerator.Initialize();
            LinqueDbContext _context = new LinqueDbContext();
            var students = _context.Students.ToList<Student>();

            Console.WriteLine(students);
        }
    }
}
