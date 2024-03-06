using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    public string UserName { get; set; }

    public ICollection<UserCourse> UserCourses { get; set; }

    public User()
    {
        UserCourses = new List<UserCourse>();
    }
}
public class Course
{
    [Key]
    public int CourseId { get; set; }

    [Required]
    public string CourseName { get; set; }

    public ICollection<UserCourse> UserCourses { get; set; }
}

public class UserCourse
{
    [Key]
    public int UserCourseId { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int CourseId { get; set; }
    public Course Course { get; set; }
}

public class YourDbContext : DbContext
{
    public YourDbContext() : base("name=ConString")
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        using (var dbContext = new YourDbContext())
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Choose an operation:");
                Console.WriteLine("Press 1 to Add User");
                Console.WriteLine("Press 2 to Add Course");
                Console.WriteLine("Press 3 to Assign Course to User");
                Console.WriteLine("Press 4 to Remove Course from User");
                Console.WriteLine("Press 5 to Delete User");
                Console.WriteLine("Press 6 to Delete Course");
                Console.WriteLine("Press 7 to Read User Courses");
                Console.WriteLine("Press 8 to Exit Program.");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Enter user name:");
                            var userName = Console.ReadLine();
                            var newUser = new User { UserName = userName };
                            dbContext.Users.Add(newUser);
                            dbContext.SaveChanges();
                            Console.WriteLine("User added");
                            break;

                        case 2:
                            Console.WriteLine("Enter course name:");
                            var courseName = Console.ReadLine();
                            var newCourse = new Course { CourseName = courseName };
                            dbContext.Courses.Add(newCourse);
                            dbContext.SaveChanges();
                            Console.WriteLine("Course added");
                            break;

                        case 3:
                            Console.WriteLine("Enter user ID:");
                            if (int.TryParse(Console.ReadLine(), out int userId))
                            {
                                var user = dbContext.Users.Find(userId);
                                if (user != null)
                                {
                                    Console.WriteLine("Enter course ID:");
                                    if (int.TryParse(Console.ReadLine(), out int courseId))
                                    {
                                        var course = dbContext.Courses.Find(courseId);
                                        if (course != null)
                                        {
                                            user.UserCourses.Add(new UserCourse { UserId = userId, CourseId = courseId });
                                            dbContext.SaveChanges();
                                            Console.WriteLine("Course assigned to user");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Course not found");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid course ID");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("User not found");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid user ID");
                            }
                            break;

                        case 4:
                            Console.WriteLine("Enter user ID:");
                            if (int.TryParse(Console.ReadLine(), out int userIdToRemove))
                            {
                                var userToRemoveCourse = dbContext.Users.Find(userIdToRemove);
                                if (userToRemoveCourse != null)
                                {
                                    Console.WriteLine("Enter course ID:");
                                    if (int.TryParse(Console.ReadLine(), out int courseIdToRemove))
                                    {
                                        var courseToRemove = dbContext.Courses.Find(courseIdToRemove);
                                        if (courseToRemove != null)
                                        {
                                            var userCourseToRemove = userToRemoveCourse.UserCourses.FirstOrDefault(uc => uc.CourseId == courseIdToRemove);
                                            if (userCourseToRemove != null)
                                            {
                                                userToRemoveCourse.UserCourses.Remove(userCourseToRemove);
                                                dbContext.SaveChanges();
                                                Console.WriteLine("Course removed from user");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Course not found for user");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Course not found");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid course ID");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("User not found");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid user ID");
                            }
                            break;

                        case 5:
                            Console.WriteLine("Enter user ID:");
                            if (int.TryParse(Console.ReadLine(), out int userIdToDelete))
                            {
                                var userToDelete = dbContext.Users.Find(userIdToDelete);
                                if (userToDelete != null)
                                {
                                    dbContext.Users.Remove(userToDelete);
                                    dbContext.SaveChanges();
                                    Console.WriteLine("User deleted");
                                }
                                else
                                {
                                    Console.WriteLine("User not found");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid user ID");
                            }
                            break;

                        case 6:
                            Console.WriteLine("Enter course ID:");
                            if (int.TryParse(Console.ReadLine(), out int courseIdToDelete))
                            {
                                var courseToDelete = dbContext.Courses.Find(courseIdToDelete);
                                if (courseToDelete != null)
                                {
                                    dbContext.Courses.Remove(courseToDelete);
                                    dbContext.SaveChanges();
                                    Console.WriteLine("Course deleted");
                                }
                                else
                                {
                                    Console.WriteLine("Course not found");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid course ID");
                            }
                            break;

                        case 7:
                            var usersWithCourses = dbContext.Users.Include("UserCourses.Course").ToList();
                            Console.WriteLine("Courses for all Users:");
                            foreach (var user in usersWithCourses)
                            {
                                Console.WriteLine($"User: {user.UserName}");
                                foreach (var userCourse in user.UserCourses)
                                {
                                    Console.WriteLine($"- Course ID: {userCourse.CourseId}, Course Name: {userCourse.Course.CourseName}");
                                }
                            }
                            break;


                        case 8:
                            exit = true;
                            break;

                        default:
                            Console.WriteLine("Error");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Error");
                }
            }
        }
    }
}
