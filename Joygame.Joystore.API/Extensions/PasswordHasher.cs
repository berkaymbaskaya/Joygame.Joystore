﻿using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Joygame.Joystore.API.Extensions
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool Verify(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }

}
