using CommandLine;

namespace Task.Models
{
    public class CommandLineArguments
    {
        [Value(0, Required = true, HelpText = "Command to execute (e.g., count, max-age).")]
        public string Command { get; set; }

        [Option("age-gt", Required = false, HelpText = "Counts participants where age is greater than <age>, where <age> is an integer.")]
        public int? AgeGt { get; set; }

        [Option("age-lt", Required = false, HelpText = "Counts participants where age is less than <age>, where <age> is an integer.")]
        public int? AgeLt { get; set; }

        [Value(1, Required = true, HelpText = "URL to fetch data from.")]
        public string Url { get; set; }
    }
}
