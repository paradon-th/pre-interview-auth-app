using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PreAuthBe.Data;
using PreAuthBe.DTOs;
using PreAuthBe.Entities;
using System.Security.Claims;

namespace PreAuthBe.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly AppDbContext  _context;
    public UserController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var user = await _context.Users
            .Select(u => new { u.Id, u.Username, u.Email, u.FirstName, u.LastName, u.Role })
            .ToListAsync();
        return Ok(user);
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto updateUserDto)
    {
        var currentAdminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (id.ToString() == currentAdminId)
        {
            return BadRequest(new { message = "คุณไม่สามารถแก้ไขบัญชีของคุณเองได้" });
        }
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }
        
        if (updateUserDto.FirstName != null)
        {
            user.FirstName = updateUserDto.FirstName;
        }
        if (updateUserDto.LastName != null)
        {
            user.LastName = updateUserDto.LastName;
        }
        if (updateUserDto.Email != null)
        {
            user.Email = updateUserDto.Email;
        }
        if (updateUserDto.Role != null)
        {
            user.Role = updateUserDto.Role;
        }

        await _context.SaveChangesAsync();

        return NoContent(); 
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var currentAdminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (id.ToString() == currentAdminId)
        {
            return BadRequest(new {message = "คุณไม่สามารถลบบัญชีของคุณเองได้"});
        }
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent(); 
    }
}