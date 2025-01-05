using Task.Models;

namespace Task.Interfaces
{
    public interface IWebFileReader
    {
        Task<IEnumerable<Participant>> ReadParticipantsFromWebFileAsync(string filePath);
    }
}
