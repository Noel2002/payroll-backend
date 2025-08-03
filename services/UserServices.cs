using DotNetTest.Models;
using DotNetTest.Config;
using DotNetTest.Utilities;

namespace DotNetTest.Services;

public class UserServices : IUserServices
{
    private DatabaseContext _context;
    public UserServices(DatabaseContext context)
    {
        _context = context;
    }
    public User CreateUser(string firstName, string lastName, string password, string email)
    {
        User newUser = new()
        {
            FirstName = firstName,
            LastName = lastName,
            Password = password,
            Email = email
        };
        newUser.Password = AuthUtils.HashPassword(newUser.Password);
        _context.Users.Add(newUser);
        _context.SaveChanges();
        return newUser;
    }

    public User? UpdateUser(Guid userId, string firstName, string lastName, string password, string email)
    {
        return null;
    }

    public void DeleteUser(Guid userId)
    {
        User? user = _context.Users.Find(userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {userId} not found.");
        }
        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    public User? GetUser(Guid userId)
    {
        return _context.Users.Find(userId);
    }

    public List<User> GetUsers()
    {
        List<User> users = _context.Users.ToList();

        return users;
    }
    public User Login(string email, string password)
    {
        User? user = _context.Users.FirstOrDefault(u => u.Email == email);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with email {email} not found.");
        }
        if (AuthUtils.VerifyPassword(password, user.Password) == false)
        {
            throw new UnauthorizedAccessException("Invalid password"); // Bad practice, it gives away that the user exists
        }
        return user;
    }
    
}