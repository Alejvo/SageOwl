using Domain.Users;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task CreateUser(User user)
        => await _dbContext.Users.AddAsync(user);

    public async Task<bool> EmailExists(string email)
        => await _dbContext.Users.AnyAsync(u => u.Email == Email.Create(email));

    public async Task<User?> GetUserByEmail(string email)
    {
        var userEmail = Email.Create(email);
        return await _dbContext.Users
            .FirstOrDefaultAsync(user => user.Email == userEmail);
    }

    public async Task<User?> GetUserById(Guid id)
    {
        return await _dbContext.Users
            .Include(u => u.Subscription)
            .FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<List<User>> GetUsers()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task SoftDelete(Guid UserId)
    {
        var user = await _dbContext.Users.FindAsync(UserId);

        if(user != null)
        {
            user.IsDeleted = true;
            user.DeleteAt = DateTime.Now;
        }
    }

    public async Task Update(User user,User userUpdated)
    {
        if (userUpdated is not null && user is not null)
        {
            user.Name = userUpdated.Name;
            user.Surname = userUpdated.Surname;
            user.Email = userUpdated.Email;
            user.Password = userUpdated.Password;
            user.Username = userUpdated.Username;
        }
    }

    public async Task<bool> UsernameExists(string username)
        => await _dbContext.Users.AnyAsync(u => u.Username == username);
    
}
