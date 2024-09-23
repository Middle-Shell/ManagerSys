using ProjectManagementSystem.modules.BasicEntities.Interfaces;

namespace ProjectManagementSystem.modules.BasicEntities
{
    public class Project : IProject
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }

        public Project(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}