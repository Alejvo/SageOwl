namespace Domain.Users;

public interface IUserRepository
{

    Task<List<User>> GetUsers();
    Task<User?> GetUserById(Guid id);
    Task<User?> GetUserByEmail(string email);
    Task<bool> CreateUser(User user);
    Task<bool> Update(User user, User userUpdated);
    Task<bool> SoftDelete(Guid userId);
    Task<bool> EmailExists(string email);
    Task<bool> UsernameExists(string username);
}
