using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTrackerConsole.Interfaces
{
    public interface ITaskService
    {
        List<string> GetAllHelpCommands();

        Task<List<Task>> GetAllTasks();

        Task<int> AddNewTask(string desciption);
        Task<bool> UpdateTask(int id, string description);
        Task<bool> DeleteTask(int id);
        Task<bool> SetStatus(string status, int id);
        Task<List<Task>> GetTaskByStatus(string status);
    }
}