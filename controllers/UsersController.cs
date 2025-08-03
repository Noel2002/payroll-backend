using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using DotNetTest.Services;
using DotNetTest.Models;
using System.Diagnostics;
using DotNetTest.Dtos.Users;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using DotNetTest.Attributes;


namespace DotNetTest.Controllers;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly IUserServices _userServices;
    private readonly IAuthTokenService<AuthorizedUser> _authTokenService;
    public UsersController(IUserServices userServices, IAuthTokenService<AuthorizedUser> authTokenService)

    {
        _userServices = userServices;
        _authTokenService = authTokenService;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        List<User> users = _userServices.GetUsers();
        return Ok(users);
    }

    [HttpPost]
    public IActionResult CreateUser([FromBody] RegisterUserDto registerUserDto)
    {
    
        User newUser = this._userServices.CreateUser(
            registerUserDto.FirstName,
            registerUserDto.LastName,
            registerUserDto.Password,
            registerUserDto.Email
        );
        return CreatedAtAction(nameof(GetUsers), new { id = newUser.Id }, newUser);

       
    }

    [HttpGet("{id}")]
    [AuthTokenRequired]
    public IActionResult GetUser(Guid id)
    {
        User? user = _userServices.GetUser(id);
        if (user == null)
        {
            return NotFound($"User with ID {id} not found.");
        }

        return Ok(user);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(Guid id)
    {
        
        _userServices.DeleteUser(id);
       
        return Ok($"User '{id}' deleted.");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginCredentialDto loginDto)
    {

        User user = _userServices.Login(loginDto.Email, loginDto.Password);
        
        string token = _authTokenService.GenerateToken(new AuthorizedUser(user.Id, "regular"));
        return Ok(new {
            Token = token,
            UserId = user.Id,
            Role = "regular"
        });
       
    }
}