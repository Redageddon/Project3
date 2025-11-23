using API.DataModels;
using Newtonsoft.Json;

namespace API.Services;

public abstract class UserRepository
{
    private const string DataPath = "Data/users.json";
    private readonly Lock usersLock = new();

    public UserRepository()
    {
        this.EnsureDataFileExists();
    }

    private void EnsureDataFileExists()
    {
        if (!File.Exists(DataPath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(DataPath)!);
            this.SaveUsers(new UsersData([]));
        }
    }

    public UsersData GetAllUsers()
    {
        lock (this.usersLock)
        {
            string data = File.ReadAllText(DataPath);

            return JsonConvert.DeserializeObject<UsersData>(data) ?? new UsersData([]);
        }
    }

    public UserModel? GetUserByEmail(string email)
    {
        UsersData users = this.GetAllUsers();

        return users.Users.FirstOrDefault(user => user.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
    }

    public UserModel? GetUserById(int uid)
    {
        UsersData users = this.GetAllUsers();

        return users.Users.FirstOrDefault(user => user.Uid == uid);
    }

    public bool EmailExists(string email)
    {
        return this.GetUserByEmail(email) != null;
    }

    public bool UsernameExists(string username)
    {
        UsersData users = this.GetAllUsers();

        return users.Users.Any(user =>
                                   user.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
    }

    public UserModel CreateUser(string username, string email, string passwordHash)
    {
        lock (this.usersLock)
        {
            UsersData users = this.GetAllUsers();
            
            int newUid = users.Users.Count != 0 
                ? users.Users.Max(u => u.Uid) + 1 
                : 1;

            UserModel newUser = new(newUid, username, email, passwordHash);
            List<UserModel> updatedUsers = [..users.Users, newUser];

            this.SaveUsers(new UsersData(updatedUsers));

            return newUser;
        }
    }

    public UserModel? UpdateUser(int uid, string? username = null, string? email = null, string? passwordHash = null)
    {
        lock (this.usersLock)
        {
            UsersData users = this.GetAllUsers();
            UserModel? user = users.Users.FirstOrDefault(user => user.Uid == uid);

            if (user == null)
            {
                return null;
            }

            UserModel updatedUser = user with
            {
                Username = username ?? user.Username,
                Email = email ?? user.Email,
                PasswordHash = passwordHash ?? user.PasswordHash,
            };

            List<UserModel> updatedUsers = users.Users.Select(u => u.Uid == uid ? updatedUser : u).ToList();
            this.SaveUsers(new UsersData(updatedUsers));

            return updatedUser;
        }
    }

    public bool DeleteUser(int uid)
    {
        lock (this.usersLock)
        {
            UsersData users = this.GetAllUsers();
            UserModel? user = users.Users.FirstOrDefault(user => user.Uid == uid);

            if (user == null)
            {
                return false;
            }

            List<UserModel> updatedUsers = users.Users.Where(matchedUser => matchedUser.Uid != uid).ToList();
            this.SaveUsers(new UsersData(updatedUsers));

            return true;
        }
    }

    private void SaveUsers(UsersData users)
    {
        string? json = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(DataPath, json);
    }
}