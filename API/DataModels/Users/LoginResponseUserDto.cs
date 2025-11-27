namespace API.DataModels.Users;

public record LoginResponse(
    bool Success,
    string Message,
    UserDto? User = null,
    string? SessionId = null
);

public record UserDto(
    int Uid,
    string Username,
    string Email
);
