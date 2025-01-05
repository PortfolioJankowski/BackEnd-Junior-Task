using Task.Interfaces;
using Task.Models;

namespace Task.Services.Participants
{
    public class ParticipantReaderBase
    {
        private readonly IParticipantMapper _participantMapper;
        public ParticipantReaderBase()
        {
            _participantMapper = new ParticipantMapper();
        }
        protected static async Task<string[]> TryReadFileAsync(string filePath)
        {
            try
            {
                return await File.ReadAllLinesAsync(filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: Cannot get file.");
                throw new FileLoadException("The program is not able to read provided file", ex);
            }
        }

        protected void LogErrors(List<(Participant? participant, string? Error)> result)
        {
            var errors = result
                .Select(r => r.Error)
                .Where(e => e != null)
                .ToList();

            if (errors.Any())
                Console.WriteLine($"During data reading following errors occurred: {string.Join(",", errors)}");
        }

        protected List<Participant> ParseParticipants(string[] lines, string csvSeparator)
        {
            var result = lines
            .Skip(1)
                .Select(line => _participantMapper.TryPopulateParticipantFromLine(line.Split(csvSeparator)))
                .ToList();

            LogErrors(result);

            return result
                .Select(r => r.participant)
                .Where(p => p != null)
                .ToList()!;
        }
    }
}
