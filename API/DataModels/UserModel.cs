namespace API.DataModels;

public record UserModel(
    int Uid,
    string Username,
    string Email,
    string PasswordHash
);
