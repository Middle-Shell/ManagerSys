using ProjectManagementSystem.modules.authentication;
using ProjectManagementSystem.modules.BasicEntities;
using ProjectManagementSystem.modules.ControlData;

namespace ProjectManagementSystem.modules.UserInteraction
{
    public class CLI
    {
        private DataManager _dataManager;
        private Authenticator _authenticator;
        private User _currentUser;

        public CLI(DataManager dataManager, Authenticator authenticator)
        {
            this._dataManager = dataManager;
            this._authenticator = authenticator;
        }

        // Запускает основную логику приложения
        public void Run()
        {
            while (true)
            {
                if (_currentUser != null)
                {
                    ShowMainMenu();
                }
                else
                {
                    ShowLoginMenu();
                }
            }
        }

        // Отображает меню входа
        private void ShowLoginMenu()
        {
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Login();
                    break;
                case "2":
                    Register();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        // Процедура входа в систему
        private void Login()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            if (_authenticator.Authenticate(username, password))
            {
                _currentUser = _dataManager.GetUser(username);
                Console.WriteLine($"Welcome, {username}!");
            }
            else
            {
                Console.WriteLine("Invalid username or password.");
            }
        }

        // Процедура регистрации нового пользователя
        private void Register()
        {
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = _authenticator.HashPassword(Console.ReadLine());
            Console.Write("Enter role (Manager/Employee): ");
            string roleStr = Console.ReadLine();

            Role role;
            if (roleStr.ToLower() == "manager")
            {
                role = Role.Manager;
            }
            else if (roleStr.ToLower() == "employee")
            {
                role = Role.Employee;
            }
            else
            {
                Console.WriteLine("Invalid role.");
                return;
            }

            _dataManager.CreateUser(username, password, role);
            Console.WriteLine($"User {username} registered successfully!");
        }

        // Отображает главное меню для авторизованных пользователей
        private void ShowMainMenu()
        {
            if (_currentUser.IsManager())
            {
                Console.WriteLine("1. Create project");
                Console.WriteLine("2. Create task");
                Console.WriteLine("3. View tasks");
                Console.WriteLine("4. Update task status");
                Console.WriteLine("5. Logout");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateProject();
                        break;
                    case "2":
                        CreateTask();
                        break;
                    case "3":
                        ViewTasks();
                        break;
                    case "4":
                        UpdateTask();
                        break;
                    case "5":
                        Logout();
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("1. View tasks");
                Console.WriteLine("2. Update task status");
                Console.WriteLine("3. Logout");

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ViewTasks();
                        break;
                    case "2":
                        UpdateTask();
                        break;
                    case "3":
                        Logout();
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        // Создание нового проекта
        private void CreateProject()
        {
            Console.Write("Enter project name: ");
            string name = Console.ReadLine();
            Console.Write("Enter project description: ");
            string description = Console.ReadLine();

            _dataManager.CreateProject(name, description);
            Console.WriteLine("Project created successfully!");
        }

        // Создание новой задачи
        private void CreateTask()
        {
            Console.Write("Enter project ID: ");
            int projectId = int.Parse(Console.ReadLine());
            Console.Write("Enter task name: ");
            string name = Console.ReadLine();
            Console.Write("Enter task description: ");
            string description = Console.ReadLine();
            Console.Write("Enter task status (ToDo/InProgress/Done): ");
            TaskStatusEntities status = (TaskStatusEntities) Enum.Parse(typeof(TaskStatusEntities), Console.ReadLine());
            Console.Write("Enter assignee: ");
            string assignee = Console.ReadLine();

            _dataManager.CreateTask(projectId, name, description, status, assignee);

            Console.WriteLine("Task created successfully!");
        }

        // Просмотр задач
        private void ViewTasks()
        {
            if (_currentUser.IsManager())
            {
                List<TaskEntities> tasks = _dataManager.GetAllTasks();
                DisplayTasks(tasks);
            }
            else
            {
                List<TaskEntities> tasks = _dataManager.GetTasksByAssignee(_currentUser.Username);
                DisplayTasks(tasks);
            }
        }

        // Отображение списка задач
        private void DisplayTasks(List<TaskEntities> tasks)
        {
            if (tasks.Count > 0)
            {
                Console.WriteLine("Tasks:");
                foreach (TaskEntities task in tasks)
                {
                    Console.WriteLine($"Task ID: {task.Id}");
                    Console.WriteLine($"Project ID: {task.ProjectId}");
                    Console.WriteLine($"Name: {task.Name}");
                    Console.WriteLine($"Description: {task.Description}");
                    Console.WriteLine($"Status: {task.StatusEntities}");
                    Console.WriteLine($"Assignee: {task.Assignee}");
                    Console.WriteLine(new string('-', 20));
                }
            }
            else
            {
                Console.WriteLine("No tasks found.");
            }
        }

        private void UpdateTask()
        {
            Console.Write("Enter task ID: ");
            int taskId = int.Parse(Console.ReadLine());
            Console.Write("Enter new status (ToDo/InProgress/Done): ");
            TaskStatusEntities newStatus = (TaskStatusEntities) Enum.Parse(typeof(TaskStatusEntities), Console.ReadLine());

            _dataManager.UpdateTask(taskId, newStatus);
            Console.WriteLine("Task status updated successfully!");
        }

        private void Logout()
        {
            _currentUser = null;
            Console.WriteLine("Logged out successfully!");
        }
    }
}