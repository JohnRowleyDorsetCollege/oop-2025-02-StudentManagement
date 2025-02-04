using Microsoft.EntityFrameworkCore;
using Students.Domain;

namespace Students.Tests
{
    public class StudentRepositoryTestsInline : IDisposable
    {
        private readonly AppDbContext _context;
        private readonly StudentRepository _repository;

        public StudentRepositoryTestsInline()
        {
            // Use in-memory SQLite for testing
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new AppDbContext(options);
            _repository = new StudentRepository(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted(); // Clean up after each test
            _context.Dispose();
        }

        [Fact]
        public void AddStudent_ShouldAddStudentToDatabase()
        {
            // Act
            _repository.AddStudent("John Doe", 25);

            // Assert
            var student = _context.Students.FirstOrDefault();
            Assert.NotNull(student);
            Assert.Equal("John Doe", student.Name);
            Assert.Equal(25, student.Age);
        }
        [Fact]
        public void GetAllStudents_ShouldReturnAllStudents()
        {
            // Arrange
            _repository.AddStudent("Alice", 22);
            _repository.AddStudent("Bob", 23);

            // Act
            var students = _repository.GetAllStudents();

            // Assert
            Assert.Equal(2, students.Count());
        }

        [Fact]
        public void GetStudentById_ShouldReturnCorrectStudent()
        {
            // Arrange
            _repository.AddStudent("Charlie", 24);
            int id = _context.Students.First().Id;

            // Act
            var student = _repository.GetStudentById(id);

            // Assert
            Assert.NotNull(student);
            Assert.Equal("Charlie", student.Name);
            Assert.Equal(24, student.Age);
        }

        [Fact]
        public void UpdateStudent_ShouldModifyStudentDetails()
        {
            // Arrange
            _repository.AddStudent("David", 26);
            int id = _context.Students.First().Id;

            // Act
            _repository.UpdateStudent(id, "David Updated", 30);
            var updatedStudent = _context.Students.FirstOrDefault(s => s.Id == id);

            // Assert
            Assert.NotNull(updatedStudent);
            Assert.Equal("David Updated", updatedStudent.Name);
            Assert.Equal(30, updatedStudent.Age);
        }

        [Fact]
        public void DeleteStudent_ShouldRemoveStudentFromDatabase()
        {
            // Arrange
            _repository.AddStudent("Eve", 28);
            int id = _context.Students.First().Id;

            // Act
            _repository.DeleteStudent(id);
            var student = _context.Students.FirstOrDefault(s => s.Id == id);

            // Assert
            Assert.Null(student);
        }

        [Fact]
        public void DeleteAllStudents_ShouldRemoveAllStudents()
        {
            // Arrange
            _repository.AddStudent("Frank", 21);
            _repository.AddStudent("Grace", 23);

            // Act
            _repository.DeleteAllStudents();
            var students = _context.Students.ToList();

            // Assert
            Assert.Empty(students);
        }
    }
}