using ProjectManagementSystem.modules.BasicEntities;
using ProjectManagementSystem.modules.ControlData.Interfaces;

namespace ProjectManagementSystem.modules.ControlData
{
    public class Logger : IObserver
    {

        public void Update(TaskEntities task, string eventName)
        {
            string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine($"[{timestamp}] - {task.Name} - {eventName}");
        }

    }
}