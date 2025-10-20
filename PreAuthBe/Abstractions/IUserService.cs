using PreAuthBe.DTOs;
using PreAuthBe.Common;
using PreAuthBe.Entities;

namespace PreAuthBe.Abstractions;

public interface IUserService
{
    Task<Result<string>> LoginAsync(LoginDto loginDto);
    Task<Result> RegisterAsync(RegisterDto registerDto);
    Task<PaginatedResult<object>> GetAllUsersAsync(int pageIndex, int pageSize);
    Task<Result> DeleteUserAsync(Guid idToDelete, Guid currentAdminId);
    Task<Result> UpdateUserAsync(Guid idToUpdate, Guid currentAdminId, UpdateUserDto updateUserDto);
}