using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerConsole.Interfaces;

namespace TaskTrackerConsole.TServices
{
    public class TaskService : ITaskService
    {
        private readonly string FileName = "task_data.json";
        private readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "task_data.json");
        public Task<int> AddNewTask(string desciption)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteTask(int id)
        {
            throw new NotImplementedException();
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

        public Task<List<Task>> GetAllTasks()
        {
            throw new NotImplementedException();
        }

        public Task<List<Task>> GetTaskByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetStatus(string status, int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateTask(int id, string description)
        {
            throw new NotImplementedException();
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

        #endregion
    }
}