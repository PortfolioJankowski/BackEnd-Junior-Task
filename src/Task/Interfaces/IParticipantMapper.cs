using Task.Models;

namespace Task.Interfaces
{
    public interface IParticipantMapper
    {
        (Participant? participant, string? Error) TryPopulateParticipantFromLine(string[] properties);
    }
}
