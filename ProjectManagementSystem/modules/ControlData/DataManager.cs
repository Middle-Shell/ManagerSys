using ProjectManagementSystem.modules.BasicEntities;
using ProjectManagementSystem.modules.ControlData.Interfaces;

namespace ProjectManagementSystem.modules.ControlData
{
    public class DataManager
    {
        private static DataManager instance;
        private IDataStorage dataStorage;

        public DataManager(IDataStorage dataStorage)
        {
            this.dataStorage = dataStorage;
            LoadData();
        }

        public static DataManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataManager(DependencyInjectionContainer.GetService<IDataStorage>()); 
                }
                return instance;
            }
        }

        public void SetDataStorage(IDataStorage storage)
        {
            dataStorage = storage;
        }
        public void CreateUser(string username, string password, Role role)
        {
            dataStorage.CreateUser(username, password, role);
            SaveData();
        }
        public User GetUser(string username)
        {
            return dataStorage.GetUser(username);
        }
        
        public void CreateProject(string name, string description)
        {
            dataStorage.CreateProject(name, description);
            SaveData();
        }

        public void CreateTask(int projectId, string name, string description, TaskStatusEntities status, string assignee)
        {
            dataStorage.CreateTask(projectId, name, description, status, assignee);
            SaveData();
        }
        
        public List<TaskEntities> GetAllTasks()
        {
            return dataStorage.GetAllTasks();
        }

        // Метод для получения списка задач, назначенных на пользователя
        public List<TaskEntities> GetTasksByAssignee(string assignee)
        {
            return dataStorage.GetTasksByAssignee(assignee);
        }

        public void UpdateTask(int taskId, TaskStatusEntities status)
        {
            dataStorage.UpdateTask(taskId, status);
            SaveData();
        }

        private void LoadData()
        {
            dataStorage.LoadData();
        }

        private void SaveData()
        {
            dataStorage.SaveData();
        }
    }
}