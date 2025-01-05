using System.Runtime.Serialization;
using Task.Models;
using Task.Services.FileReader;
using Task.Services.Participants;
using Task.Services.Static;

namespace Task
{
    class Program
    {
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            try
            {
                var arguments = CommandLineArgumentsParser.Parse(args);
                ArgumentValidator.ValidateArguments(arguments);
                var participants = await LoadParticipantsDataAsync(arguments.Url);
                var participantStatistics = new ParticipantAgeService(participants);
                participantStatistics.HandleCommand(arguments);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Argument error: {ex.Message}");
                Console.WriteLine("Please check the provided arguments and try again.");
                Environment.Exit(1);
            }
            catch (IOException ex)
            {
                Console.WriteLine($"I/O error: {ex.Message}");
                Console.WriteLine("There was an issue accessing the required files or network resources.");
                Environment.Exit(2);
            }
            catch (SerializationException ex)
            {
                Console.WriteLine($"Serialization error: {ex.Message}");
                Console.WriteLine("Failed to deserialize the participant data. Please check the data format.");
                Environment.Exit(3);
            }
            catch (NotImplementedException ex)
            {
                Console.WriteLine($"Feature not implemented: {ex.Message}");
                Console.WriteLine("This command or functionality is not yet available.");
                Environment.Exit(4);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Environment.Exit(5);
            }
        }

        private static async Task<IEnumerable<Participant>> LoadParticipantsDataAsync(string url)
        {
            var fileReader = FileReaderFactory.GetRequiredService(url);
            return await fileReader.ReadParticipantsFromWebFileAsync(url);
        }
    }
}