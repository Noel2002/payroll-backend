namespace DotNetTest.Models;

public class AuthorizedUser
{
    public Guid Id { get; set; }
    public string role { get; set; }

    public AuthorizedUser(Guid id, string role)
    {
        Id = id;
        this.role = role;
    }
}