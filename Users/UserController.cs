using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mouse.NET.Auth.Services;
using Mouse.NET.Common;
using Mouse.NET.Users.Models;
using Mouse.NET.Users.services;
using Mouse.Stick.Controllers.Auth;

namespace Mouse.NET.Users;

//[Authorize]
[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    
    private readonly IUserService userService;
    private readonly IAuthService authService;

    public UserController(IUserService userService, IAuthService authService)
    {
        this.userService = userService;
        this.authService = authService;
    }

    [HttpGet("collect")]
    public async Task<PagedResult<User>> GetUserCollection([FromQuery] UserCollectionGetRequest getRequest)
    {
        return await this.userService.GetUserCollection(getRequest);
    }

    [HttpGet("by-one/{userId}")]
    public async Task<User> GetUser([FromRoute] int userId)
    {
        return await this.userService.GetUser(userId);
    }
    
    [HttpGet("me")]
    [Authorize]
    public async Task<User> GetCurrentUser()
    {
        return await this.userService.GetUser(this.authService.GetAuthorizedUserId());
    }

    [Authorize]
    [HttpPost("update-my-avatar")]
    public async Task<User> UpdateMyAvatar(IFormFile file)
    {
        return await this.userService.UpdateMyAvatar(file);
    }
    
    // [HttpPut]
    // public async Task<User> UpdateUser([FromBody] UserUpdateRequest updateRequest)
    // {
    //     return await this.userService.UpdateUser(updateRequest);
    // }
    //
    // [HttpPost]
    // public async Task<User> CreateUser([FromBody] UserCreateRequest createRequest)
    // {
    //     return await this.userService.CreateUser(createRequest);
    // }
    //
    // [HttpDelete("{userId}")]
    // public async Task<string> DeleteUser([FromRoute] int userId)
    // {
    //     return await this.userService.DeleteUser(userId);
    // }
}