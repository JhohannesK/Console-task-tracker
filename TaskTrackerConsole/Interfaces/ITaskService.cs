using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTrackerConsole.Models;

namespace TaskTrackerConsole.Interfaces
{
    public interface ITaskService
    {
        List<string> GetAllHelpCommands();

        Task<List<TaskJSON>> GetAllTasks();

        Task<int> AddNewTask(string desciption);
        Task<bool> UpdateTask(int id, string description);
        Task<bool> DeleteTask(int id);
        Task<bool> SetStatus(string status, int id);
        Task<List<TaskJSON>> GetTaskByStatus(string status);
    }
}