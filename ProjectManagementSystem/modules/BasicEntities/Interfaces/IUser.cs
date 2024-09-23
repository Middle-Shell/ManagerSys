using ProjectManagementSystem.modules.BasicEntities;

namespace ProjectManagementSystem.modules.BasicEntities.Interfaces
{
    public interface IUser
    {
        string Username { get; }
        string PasswordHash { get; }
        Role Role { get; }
        
        bool IsManager();

    }
}