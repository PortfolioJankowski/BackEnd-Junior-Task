using System.Text.Json;
using Task.Models;
using Task.Services.DateConverter;

namespace Task.Services.Static
{
    public static class FileDownloader
    {
        public static async Task<string> GetLocalFilePathAsync(string filePath)
        {
            try
            {
                var response = await GetResponseMessageAsync(filePath);
                return await GetDownloadedFilePathAsync(response!.Content, filePath);
            }
            catch (Exception ex)
            {
                throw new IOException("Failed to download file from URL.", ex);
            }
        }
        
        public static async Task<List<Participant>> DownloadJsonDataAsync(string url)
        {
            try
            {
                var response = await GetResponseMessageAsync(url);
                var jsonContent = await response!.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions 
                { 
                    PropertyNameCaseInsensitive = true,
                    Converters = { new JsonZuluDateConverter() }
                };

                var participantContainer = JsonSerializer.Deserialize<ParticipantJsonContainer>(jsonContent, options);
                
                if (participantContainer?.Participants == null)
                    throw new ArgumentException("Deserialized JSON does not contain any participants.");

                return participantContainer.Participants;
            }
            catch (Exception ex)
            {
                throw new IOException("Failed to download or deserialize the JSON file from URL.", ex);
            }
        }

        private static async Task<HttpResponseMessage?> GetResponseMessageAsync(string url)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return response;
        }

        private async static Task<string> GetDownloadedFilePathAsync(HttpContent content, string url)
        {
            var tempFilePath = Path.Combine(Path.GetTempPath(), Path.GetFileName(url));
            await using var fileStream = new FileStream(tempFilePath, FileMode.Create, FileAccess.Write);
            await content.CopyToAsync(fileStream);
            return tempFilePath;
        }
    }
}
