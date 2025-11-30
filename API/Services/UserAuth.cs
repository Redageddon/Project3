using API.DataModels.Users;
using Microsoft.AspNetCore.Identity;

namespace API.Services;

public class UserAuth(UserRepository userRepository, PasswordHasher<UserModel> passwordHasher)
{
    public LoginResponse Register(RegisterRequest request)
    {
        if (userRepository.EmailExists(request.Email))
        {
            return new LoginResponse(false, "Email already registered");
        }

        if (userRepository.UsernameExists(request.Username))
        {
            return new LoginResponse(false, "Username already taken");
        }

        string passwordHash = passwordHasher.HashPassword(null!, request.Password);
        UserModel user = userRepository.CreateUser(request.Username, request.Email, passwordHash);

        UserDto userDto = new(user.Uid, user.Username, user.Email);

        return new LoginResponse(true, "Registration successful", userDto);
    }

    public LoginResponse Login(LoginRequest request)
    {
        UserModel? user = userRepository.GetUserByEmail(request.Email);

        if (user == null
         || passwordHasher.VerifyHashedPassword(null!, user.PasswordHash, request.Password)
         != PasswordVerificationResult.Success)
        {
            return new LoginResponse(false, "Invalid email or password");
        }

        UserDto userDto = new(user.Uid, user.Username, user.Email);

        return new LoginResponse(true, "Login successful", userDto);
    }
}