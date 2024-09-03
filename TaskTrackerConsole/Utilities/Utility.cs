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
    }
}