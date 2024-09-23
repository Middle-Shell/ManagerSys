using System.Security.Cryptography;
using ProjectManagementSystem.modules.BasicEntities;
using ProjectManagementSystem.modules.ControlData.Interfaces;

namespace ProjectManagementSystem.modules.authentication
{
    public class Authenticator
    {
        private IDataStorage dataStorage;

        public Authenticator(IDataStorage dataStorage)
        {
            this.dataStorage = dataStorage;
        }

        public bool Authenticate(string username, string password)
        {
            User user = dataStorage.GetUser(username);

            if (user != null)
            {
                string hashedPassword = HashPassword(password);

                if (hashedPassword == user.PasswordHash)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsManager(User user)
        {
            return user.IsManager();
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}