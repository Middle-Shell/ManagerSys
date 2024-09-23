using System.Security.Cryptography;
using System.Text.Json.Serialization;
using ProjectManagementSystem.modules.BasicEntities.Interfaces;

namespace ProjectManagementSystem.modules.BasicEntities
{
    public class User : IUser
    {
        [JsonPropertyName("username")]
        public string Username { get; }

        [JsonPropertyName("passwordHash")]
        public string PasswordHash { get; }

        [JsonPropertyName("role")]
        public Role Role { get; }

        public User(string username, string passwordHash, Role role) 
        {
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
        }

        public bool IsManager()
        {
            return Role == Role.Manager;
        }
    }
}