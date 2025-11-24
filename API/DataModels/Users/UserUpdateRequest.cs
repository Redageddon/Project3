using System.ComponentModel.DataAnnotations;

namespace API.DataModels.Users;

public record UserUpdateRequest(
    [MinLength(3)] string? Username,
    [EmailAddress] string? Email,
    string? PasswordHash
);