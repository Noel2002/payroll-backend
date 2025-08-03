using System.Collections.ObjectModel;
using DotNetTest.Models;

namespace DotNetTest.Services;

public interface IUserServices
{
    User CreateUser(string firstName, string lastName, string password, string email);
    User? UpdateUser(Guid userId, string firstName, string lastName, string password, string email);
    void DeleteUser(Guid userId);
    User? GetUser(Guid userId);
    List<User> GetUsers();
    User Login(string email, string password);
}