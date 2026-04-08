namespace Domain.Users;

public interface IUserRepository
{
    Task<List<User>> GetUsers();
    Task<User?> GetUserById(Guid id);
    Task<User?> GetUserByEmail(string email);
    Task CreateUser(User user);
    Task Update(User user, User userUpdated);
    Task SoftDelete(Guid userId);
    Task<bool> EmailExists(string email);
    Task<bool> UsernameExists(string username);
}
