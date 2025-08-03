namespace DotNetTest.Dtos;

public record CreateShiftDto(
    DateTime StartTime,
    DateTime EndTime,
    Guid JobId,
    Guid UserId,
    string Comment
);