using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaskTrackerConsole.Utilities
{
    public static class Utility
    {
        public static void PrintInfoMessage(string message){
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n" + message);
            Console.ResetColor();
        }

        public static void PrintCommandMessage(string message){
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n" + message);
            Console.ResetColor();
        }

        public static void PrintHelpMessage(string message){
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\n" + message);
            Console.ResetColor();
        }

        public static void PrintErrorMessage(string message){
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n" + message);
            Console.ResetColor();
        }

        public static List<string> ParseInput(string input){
            var commandArgs = new List<string>();

            var regex = new Regex(@"[\""].+?[\""]|[^ ]+");
            var matches = regex.Matches(input);


            foreach (Match match in matches){
                string value = match.Value.Trim('"');
                commandArgs.Add(value);
            }

            return commandArgs;
        }

        public static bool isUserInputValid(List<string> commands, int parameterRequired){
            bool validInput = true;

            if (parameterRequired == 1 && commands.Count != parameterRequired){
                validInput = false;
            }

            if ((parameterRequired == 2 ) && (commands.Count != parameterRequired || string.IsNullOrEmpty(commands[1])))
            {
                validInput = false;
            }

            if ((parameterRequired == 3 ) && (commands.Count != parameterRequired || string.IsNullOrEmpty(commands[1]) && string.IsNullOrEmpty(commands[2])))
            {
                validInput = false;
            }

            if (!validInput){
                PrintErrorMessage("Wrong Command! Try again please.");
                PrintInfoMessage("Type \"help\" to know the right command and it's parameters");
                return false;
            }
            return true;
        }
    }
}