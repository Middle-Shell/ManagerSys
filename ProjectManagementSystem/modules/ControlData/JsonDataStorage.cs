using System.Collections.Generic;
using System.IO;
using ProjectManagementSystem.modules.BasicEntities;
using ProjectManagementSystem.modules.ControlData.Interfaces;
using System.Text.Json;

namespace ProjectManagementSystem.modules.ControlData
{
    public class JsonDataStorage : IDataStorage
    {
        private Dictionary<string, User> users = new Dictionary<string, User>();
        private Dictionary<int, Project> projects = new Dictionary<int, Project>();
        private Dictionary<int, TaskEntities> tasks = new Dictionary<int, TaskEntities>();

        private string usersFilePath = "data/users.json";
        private string projectsFilePath = "data/projects.json";
        private string tasksFilePath = "data/tasks.json";

        public void LoadData()
        {
            if (!Directory.Exists("data"))
            {
                Directory.CreateDirectory("data");
            }

            if (File.Exists(usersFilePath))
            {
                users = JsonSerializer.Deserialize<Dictionary<string, User>>(File.ReadAllText(usersFilePath));
            }

            if (File.Exists(projectsFilePath))
            {
                projects = JsonSerializer.Deserialize<Dictionary<int, Project>>(File.ReadAllText(projectsFilePath));
            }

            if (File.Exists(tasksFilePath))
            {
                tasks = JsonSerializer.Deserialize<Dictionary<int, TaskEntities>>(File.ReadAllText(tasksFilePath));
            }
        }

        public void SaveData()
        {
            if (!Directory.Exists("data"))
            {
                Directory.CreateDirectory("data");
            }

            File.WriteAllText(usersFilePath, JsonSerializer.Serialize(users));
            File.WriteAllText(projectsFilePath, JsonSerializer.Serialize(projects));
            File.WriteAllText(tasksFilePath, JsonSerializer.Serialize(tasks));
        }

        public User GetUser(string username)
        {
            if (users.ContainsKey(username))
            {
                return users[username];
            }

            return null;
        }

        public void CreateUser(string username, string password, Role role)
        {
            if (!users.ContainsKey(username))
            {
                users.Add(username, new User(username, password, role));
            }
        }

        public Project GetProject(int id)
        {
            if (projects.ContainsKey(id))
            {
                return projects[id];
            }

            return null;
        }

        public void CreateProject(string name, string description)
        {
            int nextId = projects.Count > 0 ? projects.Keys.Max() + 1 : 1;
            projects.Add(nextId, new Project(nextId, name, description));
        }

        public TaskEntities GetTask(int id)
        {
            if (tasks.ContainsKey(id))
            {
                return tasks[id];
            }

            return null;
        }

        public void CreateTask(int projectId, string name, string description, TaskStatusEntities status,
            string assignee)
        {
            int nextId = GetAllTasks().Count;
            TaskEntities taskEntities = new TaskEntities(nextId, projectId, name, description, status, assignee);
            tasks.Add(nextId, taskEntities);
            Logger logger = new Logger();
            taskEntities.AddObserver(logger); 

        }

        public void UpdateTask(int taskId, TaskStatusEntities status)
        {
            if (tasks.ContainsKey(taskId))
            {
                tasks[taskId].SetStatus(status);
            }
        }

        public List<TaskEntities> GetAllTasks()
        {
            return new List<TaskEntities>(tasks.Values);
        }

        public List<TaskEntities> GetTasksByAssignee(string assignee)
        {
            return tasks.Values.Where(task => task.Assignee == assignee).ToList();
        }

    }
}