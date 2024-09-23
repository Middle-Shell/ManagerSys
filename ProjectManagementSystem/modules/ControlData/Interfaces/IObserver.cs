using ProjectManagementSystem.modules.BasicEntities;

namespace ProjectManagementSystem.modules.ControlData.Interfaces
{
    public interface IObserver
    {
        void Update(TaskEntities task, string eventName);
    }
}