﻿using Domain.Users;

namespace Infrastructure;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string plainText)
        => BCrypt.Net.BCrypt.HashPassword(plainText);
   
    public bool Verify(string plainText, string hashed)
        => BCrypt.Net.BCrypt.Verify(plainText, hashed);
}
