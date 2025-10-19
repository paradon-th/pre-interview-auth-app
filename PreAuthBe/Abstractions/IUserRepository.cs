using PreAuthBe.Entities;

namespace PreAuthBe.Abstractions;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(Guid id);
    Task<bool> DoesUserExistAsync(string email);
    void Add(User user);
    void Remove(User user);
    Task<IEnumerable<object>> GetAllUsersAsync();
    Task<bool> SaveChangesAsync();
}