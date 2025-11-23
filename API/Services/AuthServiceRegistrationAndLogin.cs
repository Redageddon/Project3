using API.DataModels;

namespace API.Services;

public class AuthService(UserRepository userRepository, PasswordHasher passwordHasher)
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

        string passwordHash = passwordHasher.HashPassword(request.Password);
        UserModel user = userRepository.CreateUser(request.Username, request.Email, passwordHash);

        UserDto userDto = new(user.Uid, user.Username, user.Email);

        return new LoginResponse(true, "Registration successful", userDto);
    }

    public LoginResponse Login(LoginRequest request)
    {
        UserModel? user = userRepository.GetUserByEmail(request.Email);

        if (user == null || !passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
        {
            return new LoginResponse(false, "Invalid email or password");
        }

        UserDto userDto = new(user.Uid, user.Username, user.Email);

        return new LoginResponse(true, "Login successful", userDto);
    }
}