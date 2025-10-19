using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PreAuthBe.Abstractions;
using PreAuthBe.Data;
using PreAuthBe.DTOs;
using PreAuthBe.Entities;
using PreAuthBe.Services;

namespace PreAuthBe.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }
    
    [HttpPost("register")] 
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var result = await _userService.RegisterAsync(registerDto);
        
        if (!result.IsSuccess)
        {
            return BadRequest(new { message = result.Error });
        }
        
        return Ok(new { message = "ลงทะเบียนผู้ใช้เรียบร้อยแล้ว" });
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _userService.LoginAsync(loginDto);

        if (!result.IsSuccess)
        {
            return Unauthorized(new { message = result.Error });
        }
        
        return Ok(new { message = "Login successful!", token = result.Value });
    }
}