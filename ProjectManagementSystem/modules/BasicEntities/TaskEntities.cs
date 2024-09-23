using System.Text.Json.Serialization;
using ProjectManagementSystem.modules.BasicEntities.Interfaces;
using ProjectManagementSystem.modules.ControlData.Interfaces;

namespace ProjectManagementSystem.modules.BasicEntities
{
    public class TaskEntities : ITask
    {
        public int Id { get; }
        public int ProjectId { get; }
        public string Name { get; }
        public string Description { get; }
        public TaskStatusEntities StatusEntities { get; private set; }
        public string Assignee { get; }
        
        private List<IObserver> _observers = new List<IObserver>();

        [JsonConstructor]
        public TaskEntities(int id, int projectId, string name, string description, TaskStatusEntities statusEntities, string assignee)
        {
            Id = id;
            ProjectId = projectId;
            Name = name;
            Description = description;
            StatusEntities = statusEntities;
            Assignee = assignee;
        }
        
        public TaskEntities()
        {
            //  Инициализация  свойств  по  умолчанию
        }

        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void NotifyObservers(string @event)
        {
            foreach (IObserver observer in _observers)
            {
                observer.Update(this, @event);
            }
        }

        public void SetStatus(TaskStatusEntities status)
        {
            StatusEntities = status;
            NotifyObservers("status_changed");
        }

        public TaskStatusEntities GetStatus()
        {
            return StatusEntities;
        }
    }
}