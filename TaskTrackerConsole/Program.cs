

using TaskTrackerConsole.Utilities;
using Microsoft.Extensions.DependencyInjection;
using TaskTrackerConsole.Interfaces;
using TaskTrackerConsole.TServices;

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
            // AddNewTask();
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