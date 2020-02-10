using LMGEDIApp.Infrastructure.ApplicationServices.Contracts;

namespace LMGEDIApp.Infrastructure.Services
{
    public class EncryptionService : IEncryption
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 11);
        }

        public bool VerifyHashedPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}