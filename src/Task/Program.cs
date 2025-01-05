using Task.Services.Static;

namespace Task
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var arguments = CommandLineArgumentsParser.Parse(args);
                ArgumentValidator.ValidateArguments(arguments);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Argument error: {ex.Message}");
                Console.WriteLine("Please check the provided arguments and try again.");
                Environment.Exit(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}