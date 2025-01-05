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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}