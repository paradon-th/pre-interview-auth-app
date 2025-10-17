using Microsoft.EntityFrameworkCore;
using PreAuthBe.Entities; 

namespace PreAuthBe.Data;

public class AppDbContext :  DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



        var passwordHash = BCrypt.Net.BCrypt.HashPassword("password123!");
        
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.Parse("a1b2c3d4-e5f6-4a9b-8c7d-123456abcdef"),
                Username = "somying_admin",
                Email = "somying.j@system.io",
                FirstName = "สมหญิง",
                LastName = "ใจดี",
                PasswordHash = passwordHash,
                Role = "Admin"
            },
            new User
            {
                Id = Guid.Parse("b2c3d4e5-f6a7-4b8c-9d6e-234567abcdef"),
                Username = "somchai_user",
                Email = "somchai.r@email.com",
                FirstName = "สมชาย",
                LastName = "รักไทย",
                PasswordHash = passwordHash,
                Role = "User"
            },
            new User
            {
                Id = Guid.Parse("e5f6a7b8-c9d0-4c5b-af4f-345678abcdef"),
                Username = "ekachai_admin",
                Email = "ekachai@superuser.com",
                FirstName = "เอกชัย",
                LastName = "ราชา",
                PasswordHash = passwordHash,
                Role = "Admin"
            }
        );
    }
}