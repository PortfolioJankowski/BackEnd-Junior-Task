using Task.Interfaces;
using Task.Models;
using Task.Services.Participants;
using Task.Services.Static;

namespace Task.Services.FileReader
{
    public class CsvFileReader : ParticipantReaderBase, IWebFileReader, ILocalFileReader
    {
        private readonly string _csvSeparator = ",";
        
        public CsvFileReader(): base() { } 

        public async Task<IEnumerable<Participant>> ReadParticipantsFromWebFileAsync(string url)
        {
            var tempFilePath = await FileDownloader.GetLocalFilePathAsync(url);
            return await ReadParticipantsFromLocalFileAsync(tempFilePath);
        }

        public async Task<IEnumerable<Participant>> ReadParticipantsFromLocalFileAsync(string filePath)
        {
            var lines = await TryReadFileAsync(filePath);
            return ParseParticipants(lines, _csvSeparator);
        }
    }
}


