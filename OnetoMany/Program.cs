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

    public ICollection<UserProfile> Profiles { get; set; }
}

public class UserProfile
{
    [Key]
    public int UserProfileId { get; set; }
    public int UserId { get; set; }
    public string Course { get; set; }
    public int DurationInMonths { get; set; }

    public User User { get; set; }
}

public class YourDbContext : DbContext
{
    public YourDbContext() : base("name=ConString")
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<UserProfile> UserProfiles { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.Profiles)
            .WithRequired(p => p.User)
            .HasForeignKey(p => p.UserId);
    }
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
                Console.WriteLine("Press 2 to Add Profile to User");
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
                            var newUser = new User
                            {
                                UserName = userName,
                                Profiles = new List<UserProfile>()
                            };

                            Console.WriteLine("Enter the number of profiles to add:");
                            if (int.TryParse(Console.ReadLine(), out int numProfiles) && numProfiles > 0)
                            {
                                for (int i = 0; i < numProfiles; i++)
                                {
                                    Console.WriteLine($"Profile {i + 1}:");
                                    Console.WriteLine("Enter course:");
                                    var course = Console.ReadLine();
                                    Console.WriteLine("Enter duration of course in months:");
                                    if (int.TryParse(Console.ReadLine(), out int duration))
                                    {
                                        newUser.Profiles.Add(new UserProfile
                                        {
                                            Course = course,
                                            DurationInMonths = duration
                                        });
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid duration format");
                                        i--;
                                    }
                                }

                                dbContext.Users.Add(newUser);
                                dbContext.SaveChanges();
                                Console.WriteLine("User added");
                            }
                            else
                            {
                                Console.WriteLine("Invalid number of profiles");
                            }
                            break;

                        case 2:
                            Console.WriteLine("Enter user ID to add profile:");
                            if (int.TryParse(Console.ReadLine(), out int userId))
                            {
                                var user = dbContext.Users.Find(userId);
                                if (user != null)
                                {
                                    Console.WriteLine("Enter course:");
                                    var course = Console.ReadLine();
                                    Console.WriteLine("Enter duration of course in months:");
                                    if (int.TryParse(Console.ReadLine(), out int duration))
                                    {
                                        user.Profiles.Add(new UserProfile
                                        {
                                            Course = course,
                                            DurationInMonths = duration
                                        });
                                        dbContext.SaveChanges();
                                        Console.WriteLine("Profile added to user");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Invalid duration format");
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
                                var userToDelete = dbContext.Users.Find(deleteUserId);
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
                            var users = dbContext.Users.Include("Profiles").ToList();
                            Console.WriteLine("Users:");
                            foreach (var user in users)
                            {
                                Console.WriteLine($"UserID: {user.UserId}, UserName: {user.UserName}");
                                foreach (var profile in user.Profiles)
                                {
                                    Console.WriteLine($"- ProfileID: {profile.UserProfileId}, Course: {profile.Course}, Duration (months): {profile.DurationInMonths}");
                                }
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
