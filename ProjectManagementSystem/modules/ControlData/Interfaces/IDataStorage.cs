using System.Collections.Generic;
using ProjectManagementSystem.modules.BasicEntities;

namespace ProjectManagementSystem.modules.ControlData.Interfaces
{
    public interface IDataStorage
    {
        void LoadData();
        void SaveData();

        User GetUser(string username);
        void CreateUser(string username, string password, Role role);

        Project GetProject(int id);
        void CreateProject(string name, string description);

        TaskEntities GetTask(int id);
        void CreateTask(int projectId, string name, string description, TaskStatusEntities status, string assignee);
        void UpdateTask(int taskId, TaskStatusEntities status);
        List<TaskEntities> GetAllTasks();
        List<TaskEntities> GetTasksByAssignee(string assignee);
    }
}