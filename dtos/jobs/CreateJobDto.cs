namespace DotNetTest.Dtos.Jobs;

public record CreateJobDto(
    string Title,
    string Description,
    decimal Rate,
    DateTime StartDate,
    DateTime EndDate,
    Guid UserId
);
