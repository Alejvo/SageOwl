using Domain.Users;

namespace Application.Users.Common;

public static class UserExtensions
{
    public static UserResponse ToUserResponse(this User user)
    {
        return new UserResponse(
            user.Id,
            user.Name,
            user.Surname,
            user.Email.ToString(),
            user.Username,
            user.CreatedAt);
    }
}
