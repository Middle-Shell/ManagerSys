namespace ProjectManagementSystem.modules.BasicEntities.Interfaces
{
    public interface IProject
    {
        int Id { get; }
        string Name { get; }
        string Description { get; }
    }
}