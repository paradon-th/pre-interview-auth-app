using PreAuthBe.Abstractions;
using PreAuthBe.DTOs;
using PreAuthBe.Entities;
using PreAuthBe.Common;

namespace PreAuthBe.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly TokenService _tokenService;

    public UserService(IUserRepository userRepository, TokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }
    
    public async Task<Result<string>> LoginAsync(LoginDto loginDto)
    {
        var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);

        if (user == null)
        {
            return Result<string>.Fail("อีเมลไม่ถูกต้อง");
        }

        if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            return Result<string>.Fail("รหัสผ่านไม่ถูกต้อง");
        }

        var token = _tokenService.CreateToken(user);
        return Result<string>.Ok(token);
    }
    
    public async Task<Result> RegisterAsync(RegisterDto registerDto)
    {
        if (await _userRepository.DoesUserExistAsync(registerDto.Email))
        {
            return Result.Fail("อีเมลนี้มีผู้ใช้งานในระบบแล้ว");
        }

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            PasswordHash = passwordHash,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Role = "User"
        };

        _userRepository.Add(user);
        await _userRepository.SaveChangesAsync();
        return Result.Ok();
    }
    
    public async Task<PaginatedResult<object>> GetAllUsersAsync(int pageIndex, int pageSize)
    {
        return await _userRepository.GetAllUsersAsync(pageIndex, pageSize);
    }
    
    public async Task<Result>  DeleteUserAsync(Guid idToDelete, Guid currentAdminId)
    {
        if (idToDelete == currentAdminId)
        {
            return Result.Fail("คุณไม่สามารถลบบัญชีตัวเองได้");
        }

        var user = await _userRepository.GetUserByIdAsync(idToDelete);
        if (user == null)
        {
            return Result.Fail("ไม่พบผู้ใช้ที่ต้องการลบ");
        }

        _userRepository.Remove(user);
        await _userRepository.SaveChangesAsync();
        return Result.Ok();
    }

    public async Task<Result> UpdateUserAsync(Guid idToUpdate, Guid currentAdminId, UpdateUserDto dto)
    {
        if (idToUpdate == currentAdminId)
        {
            return Result.Fail("คุณไม่สามารถแก้ไขบัญชีตัวเองได้");
        }

        var user = await _userRepository.GetUserByIdAsync(idToUpdate);
        if (user == null)
        {
            return Result.Fail("ไม่พบผู้ใช้ที่ต้องการแก้ไข");
        }
        user.FirstName = dto.FirstName ?? user.FirstName;
        user.LastName = dto.LastName ?? user.LastName;
        user.Email = dto.Email ?? user.Email;
        user.Role = dto.Role ?? user.Role;

        await _userRepository.SaveChangesAsync();
        return Result.Ok();
        }
    
}