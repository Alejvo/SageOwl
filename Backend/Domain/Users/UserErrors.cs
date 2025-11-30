using Shared;

namespace Domain.Users;

public class UserErrors
{
    public static Error UserNotFoundByEmail(string email) => new("Users.NotFound",$"User with  e-mail \"{email}\" was not found",ErrorType.NotFound);
    public static Error UserNotFound() => new("Users.NotFound", $"User was not found", ErrorType.NotFound);
    public static Error LoginFailure() =>new("Users.LoginFailure",$"The login attempt has failed",ErrorType.Unauthorized);
    public static Error EmailAlreadyExists(string email) => new("Users.EmailConflict",$"Email: {email} is not available",ErrorType.Conflict);
    public static Error UsernameAlreadyExists(string username) => new("Users.UsernameConflict", $"Username: {username} is not available", ErrorType.Conflict);
}
