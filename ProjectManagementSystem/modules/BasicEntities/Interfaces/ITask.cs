using ProjectManagementSystem.modules.BasicEntities;

namespace ProjectManagementSystem.modules.BasicEntities.Interfaces
{
    public interface ITask
    {
        int Id { get; }
        int ProjectId { get; }
        string Name { get; }
        string Description { get; }
        TaskStatusEntities  StatusEntities { get; }
        string Assignee { get; }

        void SetStatus(TaskStatusEntities statusEntities);
        TaskStatusEntities GetStatus();
    }
}