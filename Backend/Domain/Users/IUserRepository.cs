namespace Domain.Users;

public interface IUserRepository
{

    Task<List<User>> GetUsers();
    Task<User?> GetUserById(Guid id);
    Task<User?> GetUserByEmail(string email);
    Task<bool> CreateUser(User user);
    Task<bool> Update(User user, User userUpdated);
    Task Delete();
    Task SoftDelete();
    Task<bool> EmailExists(string email);
    Task<bool> UsernameExists(string username);
}
