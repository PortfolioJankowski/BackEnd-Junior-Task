using Task.Enums;
using Task.Models;
using Task.Services.Static;
using static Task.Services.Static.CommandProvider;

namespace Task.Services.Participants
{
    public class ParticipantAgeService
    {
        private readonly IEnumerable<Participant> _participants;
        public ParticipantAgeService(IEnumerable<Participant> participants)
        {
            _participants = participants;
        }

        public void HandleCommand(CommandLineArguments arguments)
        {
            if (arguments.Command.ToString().ToLower() == CommandProvider.GetCommand(Command.Count)) 
                HandleCountCommand(arguments);

            if (arguments.Command.ToString().ToLower() == CommandProvider.GetCommand(Command.MaxAge))
                HandleMaxAgeCommand();
        }

        private void HandleCountCommand(CommandLineArguments arguments)
        {
            if (arguments.AgeGt.HasValue)
            {
                var count = _participants.Count(p => p.Age > arguments.AgeGt.Value);
                Console.WriteLine($"Number of participants with age greater than {arguments.AgeGt.Value}: {count}");
                return;
            }
            
            if (arguments.AgeLt.HasValue)
            {
                var count = _participants.Count(p => p.Age < arguments.AgeLt.Value);
                Console.WriteLine($"Number of participants with age less than {arguments.AgeLt.Value}: {count}");
                return;
            }
   
            Console.WriteLine($"Total number of participants: {_participants.Count()}"); 
        }

        private void HandleMaxAgeCommand()
        {
            var maxAge = _participants.Max(p => p.Age);
            Console.WriteLine($"The maximum age of a participant is: {maxAge}");
        }

    }
}
