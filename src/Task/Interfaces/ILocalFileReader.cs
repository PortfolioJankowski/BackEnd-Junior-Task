using Task.Models;

namespace Task.Interfaces
{
    public interface ILocalFileReader
    {
        Task<IEnumerable<Participant>> ReadParticipantsFromLocalFileAsync(string filePath);
    }
}
