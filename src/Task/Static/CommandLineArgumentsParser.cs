using CommandLine;
using Task.Models;

namespace Task.Services.Static
{
    public static class CommandLineArgumentsParser
    {
        public static CommandLineArguments Parse(string[] args)
        {
            var parsedArguments = Parser.Default.ParseArguments<CommandLineArguments>(args);
            if (parsedArguments.Tag != ParserResultType.Parsed)
            {
                Console.WriteLine("Error: Invalid command-line arguments.");
                throw new ArgumentException("Invalid command-line arguments.");
            }

            return parsedArguments.Value;
        }
    }
}