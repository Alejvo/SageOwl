using Domain.Users;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public Task<bool> CreateUser(User user)
    {
        throw new NotImplementedException();
    }

    public Task Delete()
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUserById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetUsers()
    {
        throw new NotImplementedException();
    }

    public Task SoftDelete()
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(User user)
    {
        throw new NotImplementedException();
    }
}
