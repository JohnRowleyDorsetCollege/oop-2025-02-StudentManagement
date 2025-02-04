// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Students.Domain;
using System;

Console.WriteLine("Hello, World!");

SQLitePCL.Batteries_V2.Init();


var options = new DbContextOptionsBuilder<AppDbContext>()
           .UseSqlite("Data Source=c:/sqlite/students01.db")
.Options;

using var context = new AppDbContext(options);
var repo = new StudentRepository(context);


//var repo = new StudentRepository(new AppDbContext());

//var student1 = new Student { Name = "John Doe", Age = 25 };
//var student2 = new Student { Name = "Jane Doe", Age = 22 };
//repo.Add(student1);
//repo.Add(student2);

//var students = repo.GetAll();
//Console.WriteLine("List of Students");
//foreach (var student in students)
//{
//    Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Age: {student.Age}");

//}

Console.WriteLine("SQLite Student Management System");
while (true)
{
    Console.WriteLine("\nMenu:");
    Console.WriteLine("1. Add Student");
    Console.WriteLine("2. View All Students");
    Console.WriteLine("3. Find Student by ID");
    Console.WriteLine("4. Update Student");
    Console.WriteLine("5. Delete Student");
    Console.WriteLine("6. Delete All Students");
    Console.WriteLine("7. Exit");
    Console.Write("Choose an option: ");

    string choice = Console.ReadLine()!;
    switch (choice)
    {
        case "1":
            Console.Write("Enter Name: ");
            string name = Console.ReadLine()!;
            Console.Write("Enter Age: ");
            int age = int.Parse(Console.ReadLine()!);
            Student newStudent = new Student { Name = name, Age = age };
            repo.AddStudent(name, age);

            break;

        case "2":
            var students = repo.GetAllStudents();
            Console.WriteLine("\n========================");
            Console.WriteLine("\nList of Students:");
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.Id}, Name: {student.Name}, Age: {student.Age}");
            }
            Console.WriteLine("\n========================");
            break;

        case "3":
            Console.Write("Enter Student ID: ");
            int findId = int.Parse(Console.ReadLine()!);
            var foundStudent = repo.GetStudentById(findId);
            if (foundStudent != null)
            {
                Console.WriteLine($"Found: {foundStudent.Name}, Age: {foundStudent.Age}");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
            break;

        case "4":
            Console.Write("Enter Student ID to update: ");
            int updateId = int.Parse(Console.ReadLine()!);
            Console.Write("Enter New Name: ");
            string newName = Console.ReadLine()!;
            Console.Write("Enter New Age: ");
            int newAge = int.Parse(Console.ReadLine()!);
            repo.UpdateStudent(updateId, newName, newAge);
            break;

        case "5":
            Console.Write("Enter Student ID to delete: ");
            int deleteId = int.Parse(Console.ReadLine()!);
            repo.DeleteStudent(deleteId);
            break;

        case "6":
            Console.Write("Are you sure? (yes/no): ");
            if (Console.ReadLine()?.ToLower() == "yes")
            {
                repo.DeleteAllStudents();
            }
            break;

        case "7":
            Console.WriteLine("Exiting program...");
            return;

        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }

}