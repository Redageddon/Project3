namespace API.DataModels;

public record LoginResponse(
    bool Success,
    string Message,
    UserDto? User = null
);

public record UserDto(
    int Uid,
    string Username,
    string Email
);
