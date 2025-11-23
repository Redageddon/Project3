using System.ComponentModel.DataAnnotations;

namespace API.DataModels;

public record LoginRequest(
    [Required][EmailAddress] string Email,
    [Required][MinLength(6)] string Password
);
