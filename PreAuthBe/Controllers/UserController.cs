using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PreAuthBe.DTOs;
using PreAuthBe.Entities;
using PreAuthBe.Abstractions;

namespace PreAuthBe.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers([FromQuery] int pageIndex = 0,[FromQuery] int pageSize = 5)
    {
        var result = await _userService.GetAllUsersAsync(pageIndex, pageSize);
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserDto updateUserDto)
    {
        var currentAdminId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _userService.UpdateUserAsync(id, currentAdminId, updateUserDto);
        if (!result.IsSuccess)
        {
            return BadRequest(new { message = result.Error });
        }
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var currentAdminId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result = await _userService.DeleteUserAsync(id, currentAdminId);

        if (!result.IsSuccess)
        {
            return BadRequest(new { message = result.Error });
        }
        return NoContent();
    }
}