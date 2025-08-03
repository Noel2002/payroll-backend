namespace DotNetTest.Dtos.Users;
   public record RegisterUserDto(
       string FirstName,
       string LastName,
       string Password,
       string Email
   );
