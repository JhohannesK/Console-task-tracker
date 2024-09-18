

using TaskTrackerConsole.Utilities;
using Microsoft.Extensions.DependencyInjection;
using TaskTrackerConsole.Interfaces;
using TaskTrackerConsole.TServices;
using TaskTrackerConsole.Models;

var serviceCollection = new ServiceCollection();
ConfigureServices(serviceCollection);
var serviceProvider = serviceCollection.BuildServiceProvider();
var _taskService = serviceProvider.GetService<ITaskService>();

DisplayWelcomeMessage();
List<string> commands = [];

while(true){
    Utility.PrintCommandMessage("Enter Command : ");
    string input = Console.ReadLine() ?? string.Empty;

    if (string.IsNullOrEmpty(input)){
        Utility.PrintInfoMessage("\n No input detected, Try again!");
        continue;
    }

    commands = Utility.ParseInput(input);

    string command = commands[0].ToLower();

    bool exit = false;
 
    switch (command)
    {
        case "help":
            PrintHelpCommands();
            break;
        case "add":
            AddNewTask();
            break;
        case "list":
            DisplayAllTask();
            break;
        case "exit":
           exit = true;
            break;
        default:
            break;
    }

    if (exit){
        break;
    }


}

static void DisplayWelcomeMessage(){
    Utility.PrintInfoMessage("Welcome to Domeh Task Tracker Console App.");
}

void AddNewTask(){
    if (!Utility.isUserInputValid(commands, 2))
    {
        return;
    }

    var taskAdded = _taskService?.AddNewTask(commands[1]);

    if (taskAdded != null && taskAdded.Result != 0)
    {
        Utility.PrintInfoMessage("Task added successfully");
    }
    else 
    {
        Utility.PrintInfoMessage("Task failed to be added");
    }
}

void DisplayAllTask()
{
    if  (commands.Count > 2)
    {
        Utility.PrintErrorMessage("Wrong command! Try again.");
        Utility.PrintErrorMessage("Type \"help\" to know the command it's parameter.");
        return;
    }

    List<TaskJSON> tasks = new List<TaskJSON>();
    if (commands.Count == 1)
    {
        tasks = _taskService?.GetAllTasks().Result.OrderBy(x => x.Id).ToList() ?? tasks;

    }

    TaskService.CreateTaskTable(tasks);
}

void PrintHelpCommands(){
            Console.WriteLine("executed here");
            var helpCommands = _taskService?.GetAllHelpCommands();
            int count =1;
            if (helpCommands != null){
                foreach (var command in helpCommands){
                    Utility.PrintHelpMessage(count + ". " + command);
                    count++;
                }
            }
        }

static void ConfigureServices(IServiceCollection services) {
    services.AddSingleton<ITaskService, TaskService>();
}