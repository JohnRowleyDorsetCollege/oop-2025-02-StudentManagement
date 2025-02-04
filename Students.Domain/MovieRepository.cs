using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Students.Domain
{
    public class MovieRepository
    {
        private readonly AppDbContext _context;

        public MovieRepository(AppDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        //public void AddStudent(Student student)
        //{
        //    _context.Students.Add(student);
        //    _context.SaveChanges();
        //    Console.WriteLine("Student added successfully");
        //}

        public void AddMovie(string name, int year)
        {
            var movie = new Movie { Name = name, Year = year };
            _context.Movies.Add(movie);
            _context.SaveChanges();
            Console.WriteLine($"Added: {movie.Name}, Year: {movie.Year}");
        }

        //public Student? GetStudentById(int id)
        //{
        //    return _context.Students.FirstOrDefault(s => s.Id == id);
        //}

        ////public void UpdateStudent(Student student)
        ////{
        ////    _context.Students.Update(student);
        ////    _context.SaveChanges();
        ////    Console.WriteLine("Student updated successfully");
        ////}

        //public void UpdateStudent(int id, string newName, int newAge)
        //{
        //    var student = _context.Students.FirstOrDefault(s => s.Id == id);
        //    if (student != null)
        //    {
        //        student.Name = newName;
        //        student.Age = newAge;
        //        _context.SaveChanges();
        //        Console.WriteLine($"Updated Student ID {id}: {student.Name}, Age: {student.Age}");
        //    }
        //    else
        //    {
        //        Console.WriteLine($"Student with ID {id} not found.");
        //    }
        //}

        //public void DeleteStudent(int id)
        //{
        //    var student = _context.Students.FirstOrDefault(s => s.Id == id);
        //    if (student != null)
        //    {
        //        _context.Students.Remove(student);
        //        _context.SaveChanges();
        //        Console.WriteLine("Student deleted successfully");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Student not found");
        //    }
        //}

        //public void DeleteAllStudents()
        //{
        //    _context.Students.RemoveRange(_context.Students);
        //    _context.SaveChanges();
        //    Console.WriteLine("All students deleted successfully");
        //}

        //public IEnumerable<Student> GetAllStudents()
        //{
        //    return _context.Students.ToList();
        //}
    }
}
