using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PreAuthBe.Data;
using PreAuthBe.DTOs;
using PreAuthBe.Entities;
using PreAuthBe.Services;

namespace PreAuthBe.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly TokenService _tokenService;
    
    public AuthController(AppDbContext context , TokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }
    
    [HttpPost("register")] 
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
        {
            return BadRequest(new {message = "อีเมลนี้มีผู้ใช้งานในระบบแล้ว"});
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
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        return Ok(new { message = "ลงทะเบียนผู้ใช้เรียบร้อยแล้ว" });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
        
        if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
        {
            return Unauthorized(new {message = "อีเมลหรือรหัสผ่านไม่ถูกต้อง"});
        }

        var token = _tokenService.CreateToken(user);
        
        return Ok(new { message = "เข้าสู่ระบบสำเร็จ", token = token });
    }
}