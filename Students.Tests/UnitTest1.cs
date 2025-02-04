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
    }
}