using Newtonsoft.Json;

namespace Task.Models
{
    public class ParticipantJsonContainer
    {
        [JsonProperty("participants")]
        public List<Participant> Participants { get; set; } = new List<Participant>();
    }
}
