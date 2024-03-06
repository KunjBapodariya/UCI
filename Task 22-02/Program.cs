using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

public class User
{
    [Key]
    public int UserId { get; set; }

    [Required]
    public string Username { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public AppDbContext() : base("name=ConString")
    {
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        using (var context = new AppDbContext())
        {
            bool isRunning = true;
            while (isRunning)
            {
                Console.WriteLine("1. Add User");
                Console.WriteLine("2. View Users");
                Console.WriteLine("3. Update User");
                Console.WriteLine("4. Delete User");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddUser(context);
                        break;
                    case "2":
                        ViewUsers(context);
                        break;
                    case "3":
                        UpdateUser(context);
                        break;
                    case "4":
                        DeleteUser(context);
                        break;
                    case "5":
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }

    private static void AddUser(AppDbContext context)
    {
        Console.Write("Enter username: ");
        var username = Console.ReadLine();
        Console.Write("Enter email: ");
        var email = Console.ReadLine();
        Console.Write("Enter password: ");
        var password = Console.ReadLine();

        var newUser = new User { Username = username, Email = email, Password = password };
        context.Users.Add(newUser);
        context.SaveChanges();
        Console.WriteLine("User added successfully!");
    }

    private static void ViewUsers(AppDbContext context)
    {
        var users = context.Users.ToList();
        if (users.Any())
        {
            Console.WriteLine("List of Users:");
            foreach (var user in users)
            {
                Console.WriteLine($"Id: {user.UserId}, Username: {user.Username}, Email: {user.Email}");
            }
        }
        else
        {
            Console.WriteLine("No users found.");
        }
    }

    private static void UpdateUser(AppDbContext context)
    {
        Console.Write("Enter the Id of the user you want to update: ");
        var id = Convert.ToInt32(Console.ReadLine());
        var user = context.Users.Find(id);
        if (user != null)
        {
            Console.Write("Enter new username: ");
            user.Username = Console.ReadLine();
            Console.Write("Enter new email: ");
            user.Email = Console.ReadLine();
            Console.Write("Enter new password: ");
            user.Password = Console.ReadLine();

            context.SaveChanges();
            Console.WriteLine("User updated successfully!");
        }
        else
        {
            Console.WriteLine("User not found.");
        }
    }

    private static void DeleteUser(AppDbContext context)
    {
        Console.Write("Enter the Id of the user you want to delete: ");
        var id = Convert.ToInt32(Console.ReadLine());
        var user = context.Users.Find(id);
        if (user != null)
        {
            context.Users.Remove(user);
            context.SaveChanges();
            Console.WriteLine("User deleted successfully!");
        }
        else
        {
            Console.WriteLine("User not found.");
        }
    }
}