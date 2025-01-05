using Newtonsoft.Json;
using System;
using Task.Interfaces;
using Task.Models;
using Task.Services.Static;

namespace Task.Services.FileReader
{
    public class JsonFileReader : IWebFileReader
    {
        public async Task<IEnumerable<Participant>> ReadParticipantsFromWebFileAsync(string url)
        {
            return await FileDownloader.DownloadJsonDataAsync(url);
        }
    }
}
