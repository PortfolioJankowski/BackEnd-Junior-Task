using Task.Interfaces;

namespace Task.Services.FileReader
{
    public static class FileReaderFactory
    {
        public static IWebFileReader GetRequiredService(string filePath)
        {
            var fileExtenstion = Path.GetExtension(filePath).ToLower();
            if (fileExtenstion == ".json")
                return new JsonFileReader();

            if (fileExtenstion == ".csv")
                return new CsvFileReader();

            if (fileExtenstion == ".zip")
                return new ZipFileReader();

            throw new NotImplementedException($"The program found unsupported file type: {fileExtenstion}");
        }
    }
}
