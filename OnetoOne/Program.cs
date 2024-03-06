using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    public string UserName { get; set; }

    public UserProfile Profile { get; set; }
}

public class UserProfile
{
    [Key]
    public int UserId { get; set; }

    public string FullName { get; set; }

    public DateTime DateOfBirth { get; set; }
}

public class YourDbContext : DbContext
{
    public YourDbContext() : base("name=ConString")
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }
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
                Console.WriteLine("Press 2 to Update User");
                Console.WriteLine("Press 3 to Delete User");
                Console.WriteLine("Press 4 to Read User");
                Console.WriteLine("Press 5 to Exit Program.");

                int choice;
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Enter user name:");
                            var userName = Console.ReadLine();
                            Console.WriteLine("Enter full name:");
                            var fullName = Console.ReadLine();
                            Console.WriteLine("Enter date of birth:");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime dateOfBirth))
                            {
                                var newUser = new User
                                {
                                    UserName = userName,
                                    Profile = new UserProfile { FullName = fullName, DateOfBirth = dateOfBirth }
                                };
                                dbContext.Users.Add(newUser);
                                dbContext.SaveChanges();
                                Console.WriteLine("User added");
                            }
                            else
                            {
                                Console.WriteLine("Date Format Error");
                            }
                            break;

                        case 2:
                            Console.WriteLine("Enter user ID to update:");
                            if (int.TryParse(Console.ReadLine(), out int updateUserId))
                            {
                                var userToUpdate = dbContext.Users.Include("Profile").FirstOrDefault(u => u.UserId == updateUserId);
                                if (userToUpdate != null)
                                {
                                    Console.WriteLine("Enter new user name:");
                                    userToUpdate.UserName = Console.ReadLine();
                                    Console.WriteLine("Enter new full name:");
                                    userToUpdate.Profile.FullName = Console.ReadLine();
                                    Console.WriteLine("Enter new date of birth:");
                                    if (DateTime.TryParse(Console.ReadLine(), out DateTime newDateOfBirth))
                                    {
                                        userToUpdate.Profile.DateOfBirth = newDateOfBirth;
                                        try
                                        {
                                            dbContext.SaveChanges();
                                            Console.WriteLine("User Updated");
                                        }
                                        catch (DbUpdateException ex)
                                        {
                                            var innerException = ex.InnerException;
                                            while (innerException != null)
                                            {
                                                Console.WriteLine(innerException.Message);
                                                innerException = innerException.InnerException;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("Data Format Error");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("User not found");
                                }
                            }
                            break;

                        case 3:
                            Console.WriteLine("Enter user ID to delete:");
                            if (int.TryParse(Console.ReadLine(), out int deleteUserId))
                            {
                                var userToDelete = dbContext.Users.FirstOrDefault(u => u.UserId == deleteUserId);
                                if (userToDelete != null)
                                {
                                    dbContext.Users.Remove(userToDelete);
                                    dbContext.SaveChanges();
                                    Console.WriteLine("User deleted successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("User not found");
                                }
                            }
                            break;

                        case 4:
                            var users = dbContext.Users.Include("Profile").ToList();
                            Console.WriteLine("Users:");
                            foreach (var user in users)
                            {
                                Console.WriteLine($"UserID: {user.UserId}, UserName: {user.UserName}, FullName: {user.Profile.FullName}, DateOfBirth: {user.Profile.DateOfBirth.ToShortDateString()}");
                            }
                            break;

                        case 5:
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
