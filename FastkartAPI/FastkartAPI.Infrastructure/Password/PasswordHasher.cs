﻿using Org.BouncyCastle.Crypto.Generators;

namespace FastkartAPI.Infrastructure.Password
{
    public class PasswordHasher
    {
        public string GenerateTokenSHA(string password) =>
            BCrypt.Net.BCrypt.EnhancedHashPassword(password);

        public bool Verify(string password, string hashedPassword) =>
            BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }
}
