using Microsoft.EntityFrameworkCore;
using PreAuthBe.Abstractions;
using PreAuthBe.Common;
using PreAuthBe.Data;
using PreAuthBe.Entities;

namespace PreAuthBe.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
    
    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }
    
    public async Task<bool> DoesUserExistAsync(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }
    
    public void Add(User user)
    {
        _context.Users.Add(user);
    }
    
    public void Remove(User user)
    {
        _context.Users.Remove(user);
    }
    
    public async Task<PaginatedResult<object>> GetAllUsersAsync(int pageIndex, int pageSize)
    {
        var totalCount = await _context.Users.CountAsync();
        
        var items = await _context.Users
            .Select(u => new { u.Id, u.Username, u.Email, u.FirstName, u.LastName, u.Role })
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .ToListAsync();
        
        return new PaginatedResult<object>(items, totalCount);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}