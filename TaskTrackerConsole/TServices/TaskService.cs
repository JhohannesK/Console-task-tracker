using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TaskTrackerConsole.Enums;
using TaskTrackerConsole.Interfaces;
using TaskTrackerConsole.Models;
using TaskTrackerConsole.Utilities;

namespace TaskTrackerConsole.TServices
{
    public class TaskService : ITaskService
    {
        private readonly string FileName = "task_data.json";
        private readonly static string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "task_data.json");
        public Task<int> AddNewTask(string description)
        {
           try
           {
             var appTasks = new List<TaskJSON>();
             var task = new TaskJSON 
             {
                Id = GetTaskId(),
                Description = description,
                CreatedAt= DateTime.Now,
                UpdatedAt = DateTime.Now,
                TaskStatus = Enums.Status.todo,
             };

             var isFileCreatedSuccessfuly = CreateFileIfNotExist();

             if (isFileCreatedSuccessfuly)
             {
                string tasksFromJsonFileString = File.ReadAllText(FilePath);
                if (!string.IsNullOrEmpty(tasksFromJsonFileString))
                {
                    appTasks = JsonSerializer.Deserialize<List<TaskJSON>>(tasksFromJsonFileString);
                }
                appTasks?.Add(task);
                string newTaskJsonToString = JsonSerializer.Serialize(appTasks ?? []);
                File.WriteAllText(FilePath, newTaskJsonToString);
             }
             return Task.FromResult(0);
           }
           catch (Exception ex)
           {
            
            Console.WriteLine($"Task failed to be added. Error - {ex.Message}");
            return Task.FromResult(0);
           }
        }


        private static Task<List<TaskJSON>> GetTaskList()
        {
            string tasksFromJsonFileString = File.ReadAllText(FilePath);
            if (!string.IsNullOrEmpty(tasksFromJsonFileString))
            {
                return Task.FromResult(JsonSerializer.Deserialize<List<TaskJSON>>(tasksFromJsonFileString) ?? []);
            }

            return Task.FromResult(new List<TaskJSON>());
        }


        public Task<bool> DeleteTask(int id)
        {
            if (!File.Exists(FilePath))
            {
                return Task.FromResult(false);
            }

            var taskFromJson = GetTaskList();

            if (taskFromJson.Result.Count > 0)
            {
                var taskToBeDeleted = taskFromJson.Result.Where(x => x.Id == id).SingleOrDefault();

                if (taskToBeDeleted != null)
                {
                    taskFromJson.Result.Remove(taskToBeDeleted);
                    UpdateJsonFile(taskFromJson);
                    return Task.FromResult(true);
                }
            }

            return Task.FromResult(false);
        }

        private static void UpdateJsonFile(Task<List<TaskJSON>> taskFromJson)
        {
            string updatedAppTasks = JsonSerializer.Serialize(taskFromJson.Result);
            File.WriteAllText(FilePath, updatedAppTasks);
        }

        private int GetTaskId()
        {
            if (!File.Exists(FileName))
            {
                return 1;
            }
            else 
            {
                string tasksFromJsonFileString = File.ReadAllText(FileName);
                if (!string.IsNullOrEmpty(tasksFromJsonFileString))
                {
                    var appTasks = JsonSerializer.Deserialize<List<TaskJSON>>(tasksFromJsonFileString);
                    if (appTasks != null && appTasks.Count > 0)
                    {
                        return appTasks.OrderBy(x => x.Id).Last().Id + 1;
                    }
                }
            }
            return 1;
        }

        public List<string> GetAllHelpCommands()
        {
           return new List<string>
            {
                "add \"Task Description\" - To add a new task, type add with task description",
                "update \"Task Id\" \"Task Description\" - To update a task, type update with task id and task description",
                "delete \"Task Id\" - To delete a task, type delete with task id",
                "mark-in-progress \"Task Id\" - To mark a task to in progress, type mark-in-progress with task id",
                "mark-done \"Task Id\" - To mark a task to done, type mark-done with task id",
                "list - To list all task with its current status",
                "list done - To list all task with done status",
                "list todo  - To list all task with todo status",
                "list in-progress  - To list all task with in-progress status",
                "exit - To exit from app",
                "clear - To clear console window"
            };
        }

        public Task<List<TaskJSON>> GetAllTasks()
        {
           try
           {
             if (!File.Exists(FilePath))
             {
                return Task.FromResult(new List<TaskJSON>());
             }

             string fileContentString = File.ReadAllText(FilePath);
             if(!string.IsNullOrEmpty(fileContentString))
             {
                List<TaskJSON> tasks = JsonSerializer.Deserialize<List<TaskJSON>>(fileContentString);
                return Task.FromResult(tasks ?? []);
             }
             else
             {
                return Task.FromResult(new List<TaskJSON>());
             }
           }
           catch (Exception ex)
           {
            Console.WriteLine($"Unable to load file. Error - {ex.Message}");
            return Task.FromResult(new List<TaskJSON>());
           }
        }

        public Task<List<TaskJSON>> GetTaskByStatus(string status)
        {
            try
            {
                if(!File.Exists(FilePath))
                {
                    return Task.FromResult(new List<TaskJSON>());
                }


                var tasks = GetTasksFromJson();
    
                var statusToCheck = GetStatusToDisplay(status);
                return Task.FromResult(tasks.Result.Where(task => task.TaskStatus == statusToCheck).ToList() ?? []);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        private Task<List<TaskJSON>> GetTasksFromJson()
        {

            string fileContentString = File.ReadAllText(FilePath);
                if (!string.IsNullOrEmpty(fileContentString))
                {
                    return Task.FromResult(JsonSerializer.Deserialize<List<TaskJSON>>(fileContentString) ?? []); 
                }
                else
                {
                    return Task.FromResult(new List<TaskJSON>());
                }
        }

        private Status GetStatusToDisplay(string status)
        {
            switch (status)
            {
                case "todo":
                    return Status.todo;
                case "in-progress":
                    return Status.in_progress;
                case "done":
                    return Status.done;

                default:
                    return Status.todo;
            }
        }

        public Task<bool> SetStatus(string status, int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTask(int id, string description)
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    return Task.FromResult(false);
                }

                var tasks = GetTasksFromJson();

                if (tasks.Result.Count > 0) 
                {
                    var taskToBeUpdated = tasks.Result.Where(x => x.Id == id).SingleOrDefault();

                    if (taskToBeUpdated != null) 
                    {
                        var updatedTask = new TaskJSON {
                            Id = id,
                            Description = description,
                            CreatedAt = taskToBeUpdated.CreatedAt,
                            TaskStatus = taskToBeUpdated.TaskStatus,
                            UpdatedAt = DateTime.UtcNow,
                        };
                        tasks.Result.Remove(taskToBeUpdated);
                        tasks.Result.Add(updatedTask);
                        UpdateJsonFile(tasks);
                    }

                }
                return Task.FromResult(false);
            }
            catch (System.Exception)
            {
                
                throw;
            }
        }

        #region Service Helper Methods
        private bool CreateFileIfNotExist(){
            try
            {
                if(!File.Exists(FilePath)) {
                    using (FileStream fs = File.Create(FilePath)){
                        Console.WriteLine($"File {FileName} created successfully.");
                    }
                }

                return true ;    
            }
            catch (Exception ex)
            {
                Console.WriteLine($"File {FileName} creation failed. Error - " + ex.Message);
                return false;
            }
        }

        public static void CreateTaskTable(List<TaskJSON> tasks)
{
    int colWidth1 = 15, colWidth2 = 35, colWidth3 = 15, colWidth4 = 15;
    if (tasks != null && tasks.Count > 0)
    {
        Console.WriteLine("\n{0,-" + colWidth1 + "} {1,-" + colWidth2 + "} {2,-" + colWidth3 + "} {3,-" + colWidth4 + "}",
            "Task Id", "Description", "Status", "Created Date" + "\n");

        foreach (var task in tasks)
        {
            Utility.SetConsoleTextColor(task);
            Console.WriteLine("{0,-" + colWidth1 + "} {1,-" + colWidth2 + "} {2,-" + colWidth3 + "} {3,-" + colWidth4 + "}"
                , task.Id, task.Description, task.TaskStatus, task.CreatedAt.Date.ToString("dd-MM-yyyy"));
            Console.ResetColor();
        }
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("\n No Task exists! \n");
        Console.ResetColor();

        Console.WriteLine("{0,-" + colWidth1 + "} {1,-" + colWidth2 + "} {2,-" + colWidth3 + "} {3,-" + colWidth4 + "}",
           "Task Id", "Description", "Status", "CreatedDate");
    }
}

        #endregion
    }
}