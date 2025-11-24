namespace API.DataModels.Users;

public record UserModel(
    int Uid,
    string Username,
    string Email,
    string PasswordHash
);
