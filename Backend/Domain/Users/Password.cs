using System.Collections.Generic;

namespace Domain.Users;

public class Password
{
    public string Value { get; private set; }

    private Password(string value)
    {
        Value = value;
    }

    public static Password Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Password cannot be empty.");

        if (value.Length < 5)
            throw new ArgumentException("Password must be at least 5 characters.");

        return new Password(value);
    }

    public static Password Create(string plainText, IPasswordHasher hasher)
    {
        var hashed = hasher.Hash(plainText);
        return new Password(hashed);
    }

    public bool Verify(string input, IPasswordHasher hasher)
    {
        return hasher.Verify(input, Value);
    }

    public override string ToString() => Value;
}

