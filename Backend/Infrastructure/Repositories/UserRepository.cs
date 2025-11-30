using Domain.Users;
using Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<bool> CreateUser(User user)
    {
        await _dbContext.Users.AddAsync(user);
        return await _dbContext.SaveChangesAsync() > 0;
    }

    public Task Delete()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> EmailExists(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email == Email.Create(email));
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var userEmail = Email.Create(email);
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == userEmail);
    }

    public async Task<User?> GetUserById(Guid id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<List<User>> GetUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public Task SoftDelete()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Update(User user,User userUpdated)
    {
        if (userUpdated is not null && user is not null)
        {
            user.Name = userUpdated.Name;
            user.Surname = userUpdated.Surname;
            user.Email = userUpdated.Email;
            user.Password = userUpdated.Password;
            user.Username = userUpdated.Username;
        }

        return await _dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UsernameExists(string username)
    {
        return await _dbContext.Users.AnyAsync(u => u.Username == username);
    }
}
