namespace Domain.Users;

public interface IPasswordHasher
{
    string Hash(string plainText);
    bool Verify(string plainText, string hashed);
}
