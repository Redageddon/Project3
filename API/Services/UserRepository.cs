using API.DataModels.Users;
using Newtonsoft.Json;

namespace API.Services;

public class UserRepository
{
    private readonly string dataPath;
    private readonly Lock usersLock = new();

    public UserRepository(string? dataPath = null)
    {
        this.dataPath = dataPath ?? Path.Combine(AppContext.BaseDirectory, "Data", "users.json");
        this.EnsureDataFileExists();
    }

    public UsersData GetAllUsers()
    {
        lock (this.usersLock)
        {
            UsersData emptyModel = new([]);

            if (!File.Exists(this.dataPath))
            {
                this.SaveUsers(emptyModel);

                return emptyModel;
            }

            string data = File.ReadAllText(this.dataPath);

            return JsonConvert.DeserializeObject<UsersData>(data) ?? emptyModel;
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

    private void EnsureDataFileExists()
    {
        string? directory = Path.GetDirectoryName(this.dataPath);

        if (directory != null
         && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    private void SaveUsers(UsersData users)
    {
        this.EnsureDataFileExists();
        string? json = JsonConvert.SerializeObject(users, Formatting.Indented);
        File.WriteAllText(this.dataPath, json);
    }
}