using System.Text.RegularExpressions;
using Task.Models;

namespace Task.Services.Static
{
    public static class ArgumentValidator
    {
        public static void ValidateArguments(CommandLineArguments arguments)
        {
            ValidateUrl(arguments.Url);
            ValidateCommand(arguments.Command);
        }

        private static void ValidateUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                Console.WriteLine("Please provide URL");
                throw new ArgumentException($"Url was not provided.");
            }
                    
            var pattern = @"^(https?|ftp)://[^\s/$.?#].[^\s]*$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
                
            if (!regex.IsMatch(url))
            {
                Console.WriteLine("Please provide valid URL");
                throw new ArgumentException($"Provided url {url} is not valid.");
            }
        }

        private static void ValidateCommand(string command)
        {
            var availableCommands = CommandProvider.GetCommands();
            if (!availableCommands.Contains(command))
            {
                Console.WriteLine($"Invalid command. Please specify either {string.Join(",", availableCommands)}.");
                throw new ArgumentException("Command was not recognized as valid.");
            }
        }

        private static string NormalizeCommand(string command)
        {
            return command.Replace("-", "");
        }

        
    }
}
