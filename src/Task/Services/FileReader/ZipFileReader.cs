using System.IO.Compression;
using System.Text;
using Task.Interfaces;
using Task.Models;
using Task.Services.Participants;

namespace Task.Services.FileReader
{
    public class ZipFileReader : ParticipantReaderBase, IWebFileReader, ILocalFileReader 
    {
        public ZipFileReader() : base() {}

        public async Task<IEnumerable<Participant>> ReadParticipantsFromWebFileAsync(string url)
        {
            var csvContent = GetZippedCsv(url);
            var tempCsvFilePath = Path.Combine(Path.GetTempPath(), "temp.csv");
            await File.WriteAllTextAsync(tempCsvFilePath, csvContent);

            return await ReadParticipantsFromLocalFileAsync(tempCsvFilePath);
        }

        public async Task<IEnumerable<Participant>> ReadParticipantsFromLocalFileAsync(string filePath)
        {
            var lines = await TryReadFileAsync(filePath);
            return ParseParticipants(lines, ",");
        }

        private static string GetZippedCsv(string url)
        {
            using var client = new HttpClient();

            try
            {
                var stream = client.GetStreamAsync(url).Result;
                using var archive = new ZipArchive(stream);
                var entry = archive.Entries.SingleOrDefault(entry => entry.FullName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase));

                if (entry == null)
                    throw new InvalidDataException("No CSV file found in the ZIP archive.");

                using var entryStream = entry.Open();
                using var entryStreamReader = new StreamReader(entryStream, Encoding.Default);
                return entryStreamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new FileLoadException("An error occurred while downloading or reading the ZIP file.", ex);
            }
        }
    }
}
