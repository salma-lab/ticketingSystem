using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace TicketAPI.Services
{
    public class PasswordService
    {
        public string HashPassword(string password)
        {
            // Generate a salt
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Hash the password with PBKDF2 algorithm
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Store the salt and hash together for later verification
            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }

        public bool VerifyPassword(string password, string storedPasswordHash)
        {
            // Split the stored password into salt and hash
            var parts = storedPasswordHash.Split(':');
            if (parts.Length != 2)
            {
                throw new FormatException("Unexpected hash format.");
            }

            byte[] salt = Convert.FromBase64String(parts[0]);
            string storedHash = parts[1];

            // Hash the provided password with the same salt
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Compare the hash of the provided password to the stored hash
            return hash == storedHash;
        }
    }
}
