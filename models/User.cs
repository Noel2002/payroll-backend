using Microsoft.EntityFrameworkCore;

namespace DotNetTest.Models;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }

    public string Password { get; set; }
    public DateTime CreatedAt { get; set; }
    
}