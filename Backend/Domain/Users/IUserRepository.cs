namespace Domain.Users;

public interface IUserRepository
{
    Task<User> GetUsers();
    Task<User> GetUserById(Guid id);
    Task Update();
    Task Delete();
    Task SoftDelete();
}
